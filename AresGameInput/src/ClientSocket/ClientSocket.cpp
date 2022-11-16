#include <arpa/inet.h>
#include <iostream>
#include <stdlib.h>
#include <unistd.h>
#include "ClientSocket.h"

using namespace std;

ClientSocket::ClientSocket(string serverAddr, int port) {
  if((this->sock = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
    cout << endl << "socket creation error" << endl;
    exit(-1);
  }

  this->server_addr.sin_family = AF_INET;
  this->server_addr.sin_port = htons(port);

  if(inet_pton(AF_INET, serverAddr.c_str(), &this->server_addr.sin_addr) <= 0) {
    cout << endl << "invalid address / address not suported" << endl;
    exit(-1);
  }

  if((this->client_fd = connect(this->sock, (struct sockaddr *)&this->server_addr, sizeof(this->server_addr))) < 0) {
    cout << endl << "connection failed" << endl;
    exit(-1);
  }
}

void ClientSocket::sendMessage(string message) {
  send(this->sock, message.c_str(), strlen(message.c_str()), 0);
}

string ClientSocket::readMessage() {
  read(this->sock, this->buffer, 1024);
  return this->buffer;
}

void ClientSocket::closeConnectedSocket() {
  close(this->client_fd);
}