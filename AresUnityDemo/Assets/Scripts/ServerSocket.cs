using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ServerSocket {
  private const string ADDRESS = "127.0.0.1";
  private const int PORT = 8081;

  public static TcpListener server;
  public static TcpClient client;
  
  public static string clientMessage = null;

  public static void ListenForIncommingMessages() {
    try {
      server = new TcpListener(IPAddress.Parse(ADDRESS), PORT);
      server.Start();

      Byte[] bytes = new Byte[1024];
      while(true) {
        using (client = server.AcceptTcpClient()) {
          using (NetworkStream stream = client.GetStream()) {
            int length;
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
              var incommingData = new byte[length];
              Array.Copy(bytes, 0, incommingData, 0, length);
              
              clientMessage = System.Text.Encoding.ASCII.GetString(incommingData);

              if(clientMessage != "GAME START") {
                Player.ControlCommand(clientMessage);
              }
            }
          }
        }
      }
    } catch(SocketException socketException) {
      Debug.Log("SocketException " + socketException.ToString());
    }
  }

  public static void SendMessage(string serverMessage) {
    if(client == null) {
      Debug.Log("client == null");
      return;
    }

    try {
      NetworkStream stream = client.GetStream();
      if(stream.CanWrite) {
        byte[] serverMessageAsByteArray = System.Text.Encoding.ASCII.GetBytes(serverMessage);
        stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
        clientMessage = null;
      }
    } catch(SocketException socketException) {
      Debug.Log("Socket exception: " + socketException);
    }
  }
}