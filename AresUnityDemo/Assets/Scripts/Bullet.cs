using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  private Rigidbody rb;
  private GameObject shotSpawnWeapon;

  private int forceMultiplier = 2000;

  void Start() {
    rb = GetComponent<Rigidbody>();
    shotSpawnWeapon = GameObject.Find("ShotSpawn");
    
    rb.AddForce(shotSpawnWeapon.transform.forward * forceMultiplier);
  }

  void Update() {
    Destroy(gameObject, 1f);
  }
}
