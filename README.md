<h1 align="center">ARES – Software Project | <b>Game Demo</b></h1>

## :gear: **Project**

The project consists of two applications that communicate with each other:

:one: `AresUnityDemo` _folder_
> _Unity 3D application that implements the game scene_

- The player has the ability to move around the scene with a vehicle-like behavior and contain a weapon system with the ability to rotate 360º, control elevation and fire at targets.
- Targets are at random locations on the stage and move in a loop according to predefined movement patterns.
- Scene ends after 2 min timeout or when all targets are dead.

:two: `AresGameInput` _folder_
> _C++ application to interface the game and control a player inside the scene_

- The game is controlled from user input, restricted to the following commands - move player forward/backward/left/right, rotate weapon left/right, raise/lower weapon and shoot at targets.
- All data exchanged between the two applications is recorded and saved in a new text log file each round of the game.

## :space_invader: **Technologies**

Project developed using C++ and the Unity 3D framework, based on C#.

## :round_pushpin: **Getting started**

//TODO

## :page_facing_up: **Documentation**

See the [documentation](doc.md) for more implementation details, classes, messages and d structures.

## :label: **License**

This project is under the MIT license. See the [LICENSE](https://choosealicense.com/licenses/mit/) for details.
