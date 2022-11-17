#include <iostream>
#include <ctime>
#include <fstream>
#include <iomanip>
#include "LogFile.h"

using namespace std;

string LogFile::getNowDateTime() {
  time_t rawTime;
  struct tm *timeInfo;
  char buffer[80];

  time(&rawTime);
  timeInfo = localtime(&rawTime);

  strftime(buffer, sizeof(buffer), "%d-%m-%y_%Hh%Mmin%Ss", timeInfo);
  string nowDateTime(buffer);

  return nowDateTime;
}

LogFile::LogFile() {
  string filename = LogFile::getNowDateTime();
  this->file.open("logs/" + filename + ".txt", ios::out);
  this->file << setw(20) << "timestamp" << setw(15) << "source app" << "  " << "message" << endl;
}

void LogFile::writeMessageToFile(string sourceApp, string message) {
  string timestamp = LogFile::getNowDateTime();
  this->file << timestamp << "  " << sourceApp << "  " << message << endl;
}

void LogFile::closeFile() {
  this->file.close();
}