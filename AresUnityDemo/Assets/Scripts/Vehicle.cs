using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
  public float throttleForce, steeringForce, brakeForce;
  public WheelCollider wheelFrontLeft, wheelFrontRight, wheelBackLeft, wheelBackRight;

  static public int horizontalInput = 0;
  static public int verticalInput = 0;
  static public bool isBraking = false;

  void Start() {
    throttleForce = 2000;
    steeringForce = 35;
    // maxSteerAngle = 35;
    brakeForce = 1000;
  }

  void Update() {
    float steering = horizontalInput * steeringForce;
    float throttle = verticalInput * throttleForce;

    //^ as rodas da frente controlam a direção
    wheelFrontLeft.steerAngle = steering;
    wheelFrontRight.steerAngle = steering;

    //^ as rodas de trás controlam o acelerador
    wheelBackLeft.motorTorque = throttle;
    wheelBackRight.motorTorque = throttle;

    //^ freio é acionado
    // if(Input.GetKey(KeyCode.Space)) {
    if(isBraking) {
      wheelBackLeft.brakeTorque = brakeForce;
      wheelBackRight.brakeTorque = brakeForce;
    }

    //^ freio para de ser acionado
    // if(Input.GetKeyDown(KeyCode.Space)) {
    if(!isBraking) {
      wheelBackLeft.brakeTorque = 0;
      wheelBackRight.brakeTorque = 0;
    }

    //^ acelerador não é acionado (teclas p/ frente e p/ trás)
    // if(Input.GetAxis("Vertical") == 0) {
    if(verticalInput == 0) {
      wheelBackLeft.brakeTorque = brakeForce;
      wheelBackRight.brakeTorque = brakeForce;
    } else {
      wheelBackLeft.brakeTorque = 0;
      wheelBackRight.brakeTorque = 0;
    }
  }
}