#include <arpa/inet.h>
#include <string>

using namespace std;

class ClientSocket {
  private:
    char buffer[1024];
    int sock;
    int client_fd;
    struct sockaddr_in server_addr;

  public:
    ClientSocket(string serverAddr, int port);
    void sendMessage(string message);
    string readMessage();
    void closeConnectedSocket();
};