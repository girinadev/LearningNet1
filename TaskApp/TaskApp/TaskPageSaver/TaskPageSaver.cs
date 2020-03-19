using System;
using System.Net.Sockets;
using System.Text;

namespace TaskApp.TaskPageSaver
{
  public static class PageSaver
  {
    const string bodySpliter = "\r\n\r\n";

    public static string GetResponseBody(string host, string path)
    {
      var message = $"GET {path} HTTP/1.1\n" +
         $"Host: {host}\n" +        
         "\n";
      try
      {
        using (var client = new TcpClient(host, 80))
        {
          using (NetworkStream stream = client.GetStream())
          {
            WriteDataToStream(stream, message);
            Console.WriteLine("Sent {0}", message);

            var responseMessage = ReadDataFromStream(stream);
            Console.WriteLine("Received {0}", responseMessage);

            return responseMessage.Substring(responseMessage.IndexOf(bodySpliter) + bodySpliter.Length);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception {0}", e);
      }

      return null;
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
