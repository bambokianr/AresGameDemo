using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour {
  private Thread serverThread;
  
  private const float GAME_TIMEOUT = 120;
  private float gameTime;
  private bool gameStarted;

  public static int shotsFiredCount;
  public static int hitsOnTargetsCount;

  public GameObject targetPrefab;
  public static int numberOfTargets = 8;

  private GameObject loadingText;
  private GameObject waitingForStartText;
  private GameObject player;

  public Camera thirdPersonCamera;

  void Start() {
    gameStarted = false;
    
    loadingText = GameObject.Find("LoadingText");
    player = GameObject.Find("Player");
    player.SetActive(false);

    StartBackgroundServerSocket(); 

    gameTime = 0;
    shotsFiredCount = 0;
    hitsOnTargetsCount = 0;
  }

  private void StartBackgroundServerSocket() {
    serverThread = new Thread(new ThreadStart(ServerSocket.ListenForIncommingMessages));
    serverThread.IsBackground = true;
    serverThread.Start();
  }

  private IEnumerator SpawnTargets() {
    int targetCount = 0;
    do {
      float posX = Random.Range(-15f, 15f);
      float posY = Random.Range(2.5f, 5f);
      float posZ = Random.Range(-15f, 15f);

      GameObject target = Instantiate(targetPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
      
      yield return null;

      targetCount++;
    } while(targetCount < numberOfTargets);
  }

  void Update() {
    if(ServerSocket.clientMessage == "GAME START") {
      ServerSocket.SendMessage("GAME STARTED");
      SetGameStart();
    }

    if(ServerSocket.clientMessage == "CANCEL GAME") {
      ServerSocket.SendMessage("GAME CANCELED");
      SetGameOver();
    }

    if(gameStarted) {
      ManageGamePlay();
    }
  }

  private void SetGameStart() {
    gameStarted = true;

    thirdPersonCamera.enabled = false;
    
    Destroy(loadingText);
    player.SetActive(true);
    StartCoroutine(SpawnTargets());
  }

  private void SetGameOver() {
    #if UNITY_STANDALONE
      Application.Quit();
    #endif
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif
  }

  private void ManageGamePlay() {
    if(gameTime < GAME_TIMEOUT) {
      gameTime += Time.deltaTime;
    } else if(gameTime >= GAME_TIMEOUT || hitsOnTargetsCount == numberOfTargets || Input.GetKeyDown(KeyCode.Escape)) {
      ServerSocket.SendMessage("GAME OVER"  
                               + "\n                                     Elapsed time: " + gameTime + " seconds  "
                               + "\n                                     Number of shots fired: " + shotsFiredCount 
                               + "\n                                     Number of hits on targets: " + hitsOnTargetsCount);
      SetGameOver();
    }
  }

  public static void UpdateShotsFiredCount() {
    shotsFiredCount++;
  }

  public static void UpdateHitsOnTargetsCount() {
    hitsOnTargetsCount++;
    ServerSocket.SendMessage("HIT TARGET"
                             + "\n                                     Number of hits on targets: " + hitsOnTargetsCount + "/" + numberOfTargets);
  }
}
