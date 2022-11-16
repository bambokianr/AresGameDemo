#include <fstream>
#include <string>

using namespace std;

class LogFile {
  private:
    fstream file;
    string getNowDateTime();

  public:
    LogFile();
    void writeMessageToFile(string sourceApp, string message);
    void closeFile();
};