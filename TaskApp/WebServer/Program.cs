using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
  class Program
  {
    static void Main(string[] args)
    {
      string httpMessageTemplate = "HTTP/1.1 200 OK\r\n" +
              "Server: Microsoft-IIS/8.5\r\n" +
              "Content-Type: text/html; charset=utf-8\r\n" +
              "Connection: close\r\n" +
              "\r\n" +
              "<!DOCTYPE html><html><head><title>Test page</title></head><body>Current date is {0}</body></html>";

      TcpListener server = null;
      try
      {
        var localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, 13000);

        server.Start();

        while (true)
        {
          Console.WriteLine("Waiting for a connection... ");

          TcpClient client = server.AcceptTcpClient();
          Console.WriteLine("Connected!");

          using (NetworkStream networkStream = client.GetStream())
          {
            var receivedMessage = ReadDataFromStream(networkStream);
            if (!string.IsNullOrEmpty(receivedMessage))
              Console.WriteLine("Received: {0}", receivedMessage);

            var httpMessage = string.Format(httpMessageTemplate, DateTime.Now.ToString());
            WriteDataToStream(networkStream, httpMessage);
            Console.WriteLine("Sent: {0}", httpMessageTemplate);
          }

          client.Close();
        }
      }
      catch (SocketException e)
      {
        Console.WriteLine("SocketException: {0}", e);
      }
      finally
      {
        server.Stop();
      }

      Console.WriteLine("\nHit enter to continue...");
      Console.Read();
    }

    private static string ReadDataFromStream(NetworkStream networkStream)
    {
      if (networkStream.CanRead)
      {
        byte[] myReadBuffer = new byte[1024];
        StringBuilder responseData = new StringBuilder();
        int numberOfBytesRead = 0;
        do
        {
          numberOfBytesRead = networkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
          responseData.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
        }
        while (networkStream.DataAvailable);

        return responseData.ToString();
      }
      else
      {
        Console.WriteLine("Cannot read from this NetworkStream.");
      }

      return null;
    }

    private static void WriteDataToStream(NetworkStream networkStream, string message)
    {
      if (networkStream.CanWrite)
      {
        var dataToWrite = Encoding.ASCII.GetBytes(message);
        networkStream.Write(dataToWrite, 0, dataToWrite.Length);
        networkStream.Flush();
      }
    }
  }
}
