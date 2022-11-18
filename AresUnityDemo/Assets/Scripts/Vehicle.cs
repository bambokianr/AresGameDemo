using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
  public WheelCollider wheelFrontLeft, wheelFrontRight, wheelBackLeft, wheelBackRight;
  
  private int steering;
  private int throttle;

  private int throttleForce = 2000;
  private int steeringForce = 35;
  private int brakeForce = 1000;

  static public int horizontalInput = 0;
  static public int verticalInput = 0;

  void Update() {
    SetSteering();
    SetThrottle();

    HandleSteerAngle();
    HandleMotorTorque();
    HandleBrakeTorque();
  }

  private void SetSteering() {
    steering = horizontalInput * steeringForce;
  }

  private void SetThrottle() {
    throttle = verticalInput * throttleForce;
  }

  private void HandleSteerAngle() {
    wheelFrontLeft.steerAngle = steering;
    wheelFrontRight.steerAngle = steering;
  }

  private void HandleMotorTorque() {
    wheelBackLeft.motorTorque = throttle;
    wheelBackRight.motorTorque = throttle;
  }

  private void HandleBrakeTorque() {
    int brakeTorque = (verticalInput == 0) ? brakeForce : 0;

    wheelBackLeft.brakeTorque = brakeTorque;
    wheelBackRight.brakeTorque = brakeTorque;
  }
}