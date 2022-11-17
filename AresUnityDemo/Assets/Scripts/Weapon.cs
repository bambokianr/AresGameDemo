using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  // private const string MOUSE_X = "Mouse X";
  // private const string MOUSE_Y = "Mouse Y";

  private Transform shotSpawn;

  private float rotationY = 0;
  private float rotationX = 180;

  private float angleYmin = -10;
  private float angleYmax = 60;
  
  private float mouseSensibility = 0.5f;

  private float smoothCoef = 0.5f;

  private float smoothRotX = 0;
  private float smoothRotY = 0;

  public Transform bulletPrefab;

  static public int horizontalInput = 0;
  static public int verticalInput = 0;
  static public bool isShooting = false;

  void Start() {
    shotSpawn = transform.Find("ShotSpawn");

    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    HandleMovement();

    // if(Input.GetKeyDown(KeyCode.Mouse0)) {
    if(isShooting) {
      ShootRaycast();
      // ShootBullet();
      GameManager.UpdateShotsFiredCount();
      isShooting = false;
    }
  }

  private void HandleMovement() {
    // float horizontalDelta = Input.GetAxisRaw(MOUSE_X) * mouseSensibility;
    // float verticalDelta = Input.GetAxisRaw(MOUSE_Y) * mouseSensibility;
    float horizontalDelta = horizontalInput * mouseSensibility;
    float verticalDelta = verticalInput * mouseSensibility;

    smoothRotX = Mathf.Lerp(smoothRotX, horizontalDelta, smoothCoef);
    smoothRotY = Mathf.Lerp(smoothRotY, verticalDelta, smoothCoef);

    // rotationX += horizontalDelta;
    // rotationY += verticalDelta;
    rotationX += smoothRotX;
    rotationY += smoothRotY;
    rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);

    transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
  }

  private void ShootRaycast() {
    Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);

    Vector3 shotSpawnPosition = shotSpawn.position;
    Vector3 shotSpawnForward = shotSpawn.forward;

    RaycastHit hitInfo;
    if(Physics.Raycast(shotSpawnPosition, shotSpawnForward, out hitInfo, Mathf.Infinity)) { // 4param = LayerMask.GetMask("hittable")
      if(hitInfo.transform.tag == "Target") {
        hitInfo.transform.GetComponent<Target>().HandleHit();
      }
    }
  }
}
