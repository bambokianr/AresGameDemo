#include <iostream>
#include <iomanip>
#include <thread>
#include "Input/Input.h"
#include "ClientSocket/ClientSocket.h"
#include "LogFile/LogFile.h"
#include "Message/Message.h"

using namespace std;

#define PORT                       8081
#define ADDRESS             "127.0.0.1"

#define ARESGAMEINPUT   "AresGameInput"
#define ARESUNITYDEMO   "AresUnityDemo"

Input input;
ClientSocket client(ADDRESS, PORT);
LogFile logFile;
Message message;
string receivedMessage = "";

void sendAndSaveMessage(string command) {
  client.sendMessage(command);

  string messageCommand = message.translateInputToMessage(command);
  logFile.writeMessageToFile(ARESGAMEINPUT, messageCommand);
}

void getSendAndSaveMessage() {
  while(receivedMessage.substr(0, 9) != "GAME OVER") {
    char commandChar = input.setAndGetKey();
    string commandString = string(1, commandChar);

    sendAndSaveMessage(commandString);
  }
}

void receiveAndSaveMessage() {
  while(receivedMessage.substr(0, 9) != "GAME OVER") {
    receivedMessage = client.readMessage();
    logFile.writeMessageToFile(ARESUNITYDEMO, receivedMessage);
  }
  if(receivedMessage.substr(0, 9) == "GAME OVER") {
    system("stty cooked");
    cout << receivedMessage << endl;
    logFile.closeFile();
  }
}

int main() {
  cout << "Start ARES Game Demo? [y/n]";
  char isGameStarted = input.setAndGetKey();

  if (isGameStarted != 'y' && isGameStarted != 'Y') {
    cout << endl << "Ok, bye! :(" << endl;
    
    sendAndSaveMessage("CANCEL GAME");

    return 0;
  }

  cout << endl << "Initializating 'AresUnityDemo'..." << endl;

  sendAndSaveMessage("GAME START");
  
  cout << endl << "GAME START" << endl;
  cout << "                                     Press keys to command the player." << endl;

  thread clientMessageThread(getSendAndSaveMessage);
  thread serverMessageThread(receiveAndSaveMessage);
  clientMessageThread.join();
  serverMessageThread.join();

  client.closeConnectedSocket();

  return 0;
}