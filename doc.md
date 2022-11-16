<div align="center">
  <p>ARES – Software Project | Game Demo</p>
  <h2>:page_facing_up: Documentation</h2>
</div>

### :one: Implemented project description

#### &ensp;&ensp;&ensp;&ensp; :open_file_folder: AresGameInput

---
<!-- TODO - descrever aplicação em geral e mencionar `main.cpp` -->

#### - [_LogFile.cpp_](AresGameInput/src/LogFile/LogFile.cpp)

Class responsible for creating and modifying a new text file at each application execution. This log file saves all messages and data exchanged between `AresGameInput` and `AresUnityDemo`.  
<!-- To understand all the messages and data exchanged between `AresGameInput` and `AresUnityDemo`, see Log file structure. -->  <!-- alterar esse texto -->

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