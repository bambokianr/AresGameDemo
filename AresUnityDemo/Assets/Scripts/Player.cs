using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  private const string MOVE_VEHICLE_FORWARD = "w";
  private const string MOVE_VEHICLE_BACKWARD = "s";
  private const string MOVE_VEHICLE_LEFT = "a";
  private const string MOVE_VEHICLE_RIGHT = "d";
  private const string BRAKE_VEHICLE = "x";

  private const string MOVE_WEAPON_UP = "W";
  private const string MOVE_WEAPON_DOWN = "S";
  private const string MOVE_WEAPON_LEFT = "A";
  private const string MOVE_WEAPON_RIGHT = "D";
  private const string WEAPON_SHOOTING = " ";
  
  public static void ControlCommand(string command) {
    switch(command) {
      case MOVE_VEHICLE_FORWARD:
        Vehicle.verticalInput = 1;
        Vehicle.isBraking = false;
        Debug.Log("move vehicle forward");
        break;
      case MOVE_VEHICLE_BACKWARD:
        Vehicle.verticalInput = -1;
        Vehicle.isBraking = false;
        Debug.Log("move vehicle backward");
        break;
      case MOVE_VEHICLE_LEFT:
        Vehicle.horizontalInput = -1;
        Debug.Log("move vehicle left");
        break;
      case MOVE_VEHICLE_RIGHT:
        Vehicle.horizontalInput = 1;
        Debug.Log("move vehicle right");
        break;
      case BRAKE_VEHICLE:
        Vehicle.isBraking = true;
        Vehicle.verticalInput = 0;
        Debug.Log("brake vehicle");
        break;
      case MOVE_WEAPON_UP:
        Weapon.verticalInput = 1;
        Debug.Log("move weapon up");
        break;
      case MOVE_WEAPON_DOWN:
        Weapon.verticalInput = -1;
        Debug.Log("move weapon down");
        break;
      case MOVE_WEAPON_LEFT:
        Weapon.horizontalInput = -1;
        Debug.Log("move weapon left");
        break;
      case MOVE_WEAPON_RIGHT:
        Weapon.horizontalInput = 1;
        Debug.Log("move weapon right");
        break;
      case WEAPON_SHOOTING:
        Weapon.isShooting = true;
        Debug.Log("weapon shooting");
        break;
      default: 
        break;
    }
  }
}