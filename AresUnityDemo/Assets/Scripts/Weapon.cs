using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  private Transform shotSpawn;

  private float rotationY = 0;
  private float rotationX = 180;

  private float angleYmin = -10;
  private float angleYmax = 60;
  
  public Transform bulletPrefab;

  static public int horizontalInput = 0;
  static public int verticalInput = 0;
  static public bool isShooting = false;

  void Start() {
    Cursor.visible = false;
    shotSpawn = transform.Find("ShotSpawn");
  }

  void Update() {
    HandleMovement();

    if(isShooting) {
      ShootRaycast();
      GameManager.UpdateShotsFiredCount();
      isShooting = false;
    }
  }

  private void HandleMovement() {
    rotationX += horizontalInput;
    rotationY += verticalInput;
    rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);

    transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
    
    horizontalInput = 0;
    verticalInput = 0;
  }

  private void ShootRaycast() {
    Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);

    Vector3 shotSpawnPosition = shotSpawn.position;
    Vector3 shotSpawnForward = shotSpawn.forward;

    RaycastHit hitInfo;
    if(Physics.Raycast(shotSpawnPosition, shotSpawnForward, out hitInfo, Mathf.Infinity)) {
      if(hitInfo.transform.tag == "Target") {
        hitInfo.transform.GetComponent<Target>().HandleHit();
      }
    }
  }
}