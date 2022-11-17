#!/bin/sh
g++ src/main.cpp src/Input/Input.cpp src/ClientSocket/ClientSocket.cpp src/LogFile/LogFile.cpp src/Message/Message.cpp -o bin/app
./bin/app