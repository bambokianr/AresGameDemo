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
        if(Vehicle.verticalInput == -1)
          Vehicle.verticalInput = 0;  
        else
          Vehicle.verticalInput = 1;
        break;
      case MOVE_VEHICLE_BACKWARD:
        if(Vehicle.verticalInput == 1)
          Vehicle.verticalInput = 0;
        else 
          Vehicle.verticalInput = -1;
        break;
      case MOVE_VEHICLE_LEFT:
        if(Vehicle.horizontalInput == 1) 
          Vehicle.horizontalInput = 0; 
        else 
          Vehicle.horizontalInput = -1;
        break;
      case MOVE_VEHICLE_RIGHT:
        if(Vehicle.horizontalInput == -1) 
          Vehicle.horizontalInput = 0; 
        else 
          Vehicle.horizontalInput = 1;
        break;
      case BRAKE_VEHICLE:
        Vehicle.verticalInput = 0;
        break;

      case MOVE_WEAPON_UP:
        Weapon.verticalInput = 1;
        break;
      case MOVE_WEAPON_DOWN:
        Weapon.verticalInput = -1;
        break;
      case MOVE_WEAPON_LEFT:
        Weapon.horizontalInput = -1;
        break;
      case MOVE_WEAPON_RIGHT:
        Weapon.horizontalInput = 1;
        break;
      case WEAPON_SHOOTING:
        Weapon.isShooting = true;
        break;

      default: 
        break;
    }
  }
}