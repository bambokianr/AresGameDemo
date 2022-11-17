<div align="center">
  <p>ARES – Software Project | Game Demo</p>
  <h2>:page_facing_up: Documentation</h2>
</div>

### :one: Implemented project description

#### &ensp;&ensp;&ensp;&ensp; :open_file_folder: AresUnityDemo

---

> The game scene is composed of a square plane limited by invisible walls that restrict the movement of targets and the player.  
> The vehicle and the weapon compose the player, who controls both movements.  
> Targets are spawned at random positions and move in a loop according to predefined movement patterns.

Below, all the implemented C# scripts are mentioned, with each related class description.

#### - [_GameManager.cs_](AresUnityDemo/Assets/Scripts/GameManager.cs)

Controls the main game variables - _gameTime_, _shotsFiredCount_ and _hitsOnTargetsCount_, in addition to running the server in background thread for socket communication with the **AresGameInput** application client.

:black_small_square: `private void StartBackgroundServerSocket()`  
Starts the server in the background from a new thread, keeping the `ServerSocket.ListenForIncommingMessages()` method running which waits for messages from the connected client.  

:black_small_square: `private void SetGameStart()`
Starts the game, removing the initial text from the scene and inserting the player and targets.

:black_small_square: `private IEnumerator SpawnTargets()`  
The method, called by `StartCoroutine()`, spawns targets from _prefabs_ at random starting positions in the scene.  

:black_small_square: `private void ManageGamePlay()`  
Controls game execution, ending the application if the timeout is reached, if all targets are hitted or if the user presses 'ESC' key. Also sends the **AresGameInput** application a game ending message with the number of shots fired and hits on targets.  

:black_small_square: `private void SetGameOver()`  
Invoked by `ManageGamePlay()`. Ends the application execution.

:black_small_square: `public static void UpdateShotsFiredCount()`  
Method called by the _Weapon_ class to update the shots fired value.

:black_small_square: `public static void UpdateHitsOnTargetsCount()`  
Method called by the _Target_ class to update the value of hits on target. In addition, it sends a message to the application's client.

#### - [_ServerSocket.cs_](AresUnityDemo/Assets/Scripts/ServerSocket.cs)

:black_small_square: `public static void ListenForIncommingMessages()`  
Initializes the server as a TCP listener and waits for the client to connect.  
When establishing socket communication, it waits for incoming messages, reads the input stream into the byte array, and converts the byte array into a string message to control the game.

:black_small_square: `public static new void SendMessage(string serverMessage)`  
Sends message to client using socket connection.  
Gets a client stream object for writing, converts string message to byte array and write byte array to socket connection stream.

#### - [_Target.cs_](AresUnityDemo/Assets/Scripts/Target.cs)

Initially, a movement between the horizontal, circular and sinus wave is randomly defined - `movementPattern`, as well as the movement direction (to the left, right, clockwise or counterclockwise) - `oppositeMovement`.  

- `int movementPattern` - _0_ for horizontal movement | _1_ for circular movement | _2_ for sinus wave movement  
- `bool oppositeMovement` - _false_ for right/clockwise movement direction | _true_ for left/counterclockwise movement direction  

At every update method call, the function related to the movement itself is invoked, depending on which movement was randomly chosen.  

:black_small_square: `private void InitializeHorizontalMovement()`
Sets the horizontal frequency movement given a defined interval.  

:black_small_square: `private void InitializeCircularMovement()`
Sets frequency and amplitude of the circular movement given a defined interval.  

:black_small_square: `private void InitializeSinusWaveMovement()`
Sets frequency and amplitude of the sinus wave movement given a defined interval.  

:black_small_square: `private void HandleHorizontalMovement()`
Defines `transform.position` based on horizontal movement (_x_ and _z_ axes).  

:black_small_square: `private void HandleCircularMovement()`
Defines `transform.position` based on circular movement (_x_ and _y_ axes).  

:black_small_square: `private void HandleSinusWaveMovement()`
Defines `transform.position` based on sinus wave movement (_x_, _y_ and _z_ axes).  

:black_small_square: `private void OnCollisionEnter(Collision collision)`
Controla o valor de `oppositeMovement`. Se há colisão com as paredes invisíveis, com o player ou com outro alvo, inverte-se o valor da variavel.  
Controls the `oppositeMovement` value. If there is a collision with invisible walls, with the player or with another target, the value of the variable is inverted.  

:black_small_square: `private void HandleHit()`
Method called when the bullet hits a target. Removes the object from the scene and updates the value of the `hitsOnTargetsCount` variable, from the `UpdateHitsOnTargetsCount` method of _GameManager_ class.  

#### - [_Player.cs_](AresUnityDemo/Assets/Scripts/Player.cs)

:black_small_square: `public static void ControlCommand(string command)`
Translates the received command into player actions - both vehicle and weapon, changing the variables related to each input.  

#### - [_Vehicle.cs_](AresUnityDemo/Assets/Scripts/Vehicle.cs)
<!-- TODO -->

#### - [_Weapon.cs_](AresUnityDemo/Assets/Scripts/Weapon.cs)
<!-- TODO -->

#### - [_Bullet.cs_](AresUnityDemo/Assets/Scripts/Bullet.cs)

Responsible for generating the bullet prefab for each shot given by the weapon, using the normal vector referring to the weapon output to create the movement direction. In order not to overload the scene, the object is destroyed after 1 frame.  
It's only illustrative, as the collision is implemented with the raycast method.

#### &ensp;&ensp;&ensp;&ensp; :open_file_folder: AresGameInput

---
<!-- TODO - descrever aplicação em geral e mencionar `main.cpp` -->

#### - [_LogFile.cpp_](AresGameInput/src/LogFile/LogFile.cpp)

Class responsible for creating and modifying a new text file at each application execution. This log file saves all messages and data exchanged between `AresGameInput` and `AresUnityDemo`.  
<!-- To understand all the messages and data exchanged between `AresGameInput` and `AresUnityDemo`, see Log file structure. -->

:black_small_square: `LogFile::LogFile()`  
Constructor that creates the log file inside the [logs](AresGameInput/logs/), naming it with the creation timestamp, and enable write permission.  
Also creates a header indicating the _timestamp_, _source app_ and _message_ columns.  

:black_small_square: `void LogFile::writeMessageToFile(string sourceApp, string message)`  
Fills a line of the text file with the _timestamp_ in addition to the method input arguments - _sourceApp_ and _message_.  

:black_small_square: `void LogFile::closeFile()`  
Method that closes the text file. Called as soon as the application finishes exchanging messages with `AresUnityDemo`.  

#### - [_Message.cpp_](AresGameInput/src/Message/Message.cpp)

:black_small_square: `string Message::translateInputToMessage(string input)`

Converts user input commands to log file messages.

| **input command** |      **message**      |
|:-----------------:|:---------------------:|
|        'w'        | move vehicle forward  |
|        's'        | move vehicle backward |
|        'a'        | move vehicle left     |
|        'd'        | move vehicle right    |
|        'x'        | brake vehicle         |
|        'W'        | move weapon up        |
|        'S'        | move weapon down      |
|        'A'        | move weapon left      |
|        'D'        | move weapon right     |
|        ' '        | weapon shooting       |

#### - [_Input.cpp_](AresGameInput/src/Input/Input.cpp)

Catches user input from terminal.  

:black_small_square: `char Input::setAndGetKey()`  
Sets terminal to "raw" mode, captures the command sent by terminal, resets terminal to normal "cooked" mode, and finally returns the user input.

#### - [_ClientSocket.cpp_](AresGameInput/src/ClientSocket/ClientSocket.cpp)

Class that manages the client related to socket communication.  

:black_small_square: `ClientSocket::ClientSocket(string serverAddr, int port)`  
Constructor that creates a socket file descriptor, defines the server from the `sockaddr_in struct` (information about address family, network address and port), and then connects the client to allow communication.

:black_small_square: `void ClientSocket::sendMessage(string message)`  
Allows the client to send a message from the established socket communication.  

:black_small_square: `string ClientSocket::readMessage()`  
Reads and returns the message received by the server.

:black_small_square: `void ClientSocket::closeConnectedSocket()`  
Ends client-server communication via socket communication.