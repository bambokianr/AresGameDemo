using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
  private Rigidbody rb;

  private Vector3 initialPosition;

  private enum MovementPatterns { Horizontal, Circular, SinusWave };
  private int movementPattern;
  private bool oppositeMovement;

  private float frequency;
  private float amplitude;

  void Start() {
    rb = GetComponent<Rigidbody>();

    initialPosition = transform.position;

    movementPattern = Random.Range(0, 3);
    oppositeMovement = (Random.Range(0, 2) * 2 - 1) == -1;

    switch(movementPattern) {
      case (int)MovementPatterns.Horizontal: 
        InitializeHorizontalMovement();
        break;
      case (int)MovementPatterns.Circular: 
        InitializeCircularMovement();
        break;
      case (int)MovementPatterns.SinusWave: 
        InitializeSinusWaveMovement();
        break;
      default: break;
    }
  }

  private void InitializeHorizontalMovement() {
    frequency = Random.Range(5f, 15f);
  }

  private void InitializeCircularMovement() {
    frequency = Random.Range(1f, 3f);
    amplitude = Random.Range(0.5f, 2f);
  }

  private void InitializeSinusWaveMovement() {
    frequency = Random.Range(1f, 4f);
    amplitude = Random.Range(0.5f, 2f);
  }

  void Update() {
    switch(movementPattern) {
      case (int)MovementPatterns.Horizontal: 
        HandleHorizontalMovement();
        break;
      case (int)MovementPatterns.Circular: 
        HandleCircularMovement();
        break;
      case (int)MovementPatterns.SinusWave: 
        HandleSinusWaveMovement();
        break;
      default: break;
    }
  }

  private void HandleHorizontalMovement() {
    Vector3 horizontalDirection = oppositeMovement ? Vector3.left : Vector3.right;

    rb.velocity = frequency * horizontalDirection;
    
    float x = transform.position.x;
    float y = initialPosition.y;
    float z = transform.position.z;

    transform.position = new Vector3(x, y, z);
  }

  private void HandleCircularMovement() {
    float x = initialPosition.x + amplitude * Mathf.Cos(frequency * Time.time);
    float y = initialPosition.y + amplitude * Mathf.Sin(frequency * Time.time);
    float z = initialPosition.z;

    transform.position = new Vector3(x, y, z);
  }

  private void HandleSinusWaveMovement() {
    Vector3 horizontalDirection = oppositeMovement ? Vector3.left : Vector3.right;
    
    rb.velocity = frequency * horizontalDirection;

    float x = transform.position.x;
    float y = initialPosition.y + amplitude * Mathf.Sin(frequency * Time.time);
    float z = transform.position.z;

    transform.position = new Vector3(x, y, z);
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Wall") 
    || collision.gameObject.CompareTag("Target") 
    || collision.gameObject.CompareTag("Player")) {
      oppositeMovement = !oppositeMovement;
    }
  }

  public void HandleHit() {
    Destroy(gameObject);
    GameManager.UpdateHitsOnTargetsCount();
  }
}
