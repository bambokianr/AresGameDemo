#include <iostream>
#include <stdio.h>
#include "Message.h"

using namespace std;

#define MOVE_VEHICLE_FORWARD    "w"
#define MOVE_VEHICLE_BACKWARD   "s"
#define MOVE_VEHICLE_LEFT       "a"
#define MOVE_VEHICLE_RIGHT      "d"
#define BRAKE_VEHICLE           "x"
#define MOVE_WEAPON_UP          "W"
#define MOVE_WEAPON_DOWN        "S"
#define MOVE_WEAPON_LEFT        "A"
#define MOVE_WEAPON_RIGHT       "D"
#define WEAPON_SHOOTING         " "

string Message::translateInputToMessage(string input) {
  this->message = input;

  if(input == MOVE_VEHICLE_FORWARD)
    this->message = "[" + input + "] move vehicle forward";
  if(input == MOVE_VEHICLE_BACKWARD)
    this->message = "[" + input + "] move vehicle backward";
  if(input == MOVE_VEHICLE_LEFT)
    this->message = "[" + input + "] move vehicle left";
  if(input == MOVE_VEHICLE_RIGHT)
    this->message = "[" + input + "] move vehicle right";
  if(input == BRAKE_VEHICLE)
    this->message = "[" + input + "] brake vehicle";
  if(input == MOVE_WEAPON_UP)
    this->message = "[" + input + "] move weapon up";
  if(input == MOVE_WEAPON_DOWN)
    this->message = "[" + input + "] move weapon down";
  if(input == MOVE_WEAPON_LEFT)
    this->message = "[" + input + "] move weapon left";
  if(input == MOVE_WEAPON_RIGHT)
    this->message = "[" + input + "] move weapon right";
  if(input == WEAPON_SHOOTING)
    this->message = "[" + input + "] weapon shooting";

  return this->message;
}