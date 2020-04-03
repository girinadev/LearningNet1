using System;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
  public class ClientConnection
  {
    private readonly TcpClient _client;

    public string ClientName { get; set; }
    public NetworkStream Stream { get; private set; }

    public ClientConnection(TcpClient tcpClient)
    {
      _client = tcpClient;
      Stream = _client?.GetStream();
    }

    public void Process()
    {
      try
      {
        string message = $"{ClientName} join chat";
        ChatServer.Instance.BroadcastMessage(message, ClientName);
        Console.WriteLine(message);

        while (true)
        {
          try
          {
            message = GetMessage();

            if (".quit".Equals(message))
            {
              ChatServer.Instance.RemoveConnection(ClientName);
              Close();

              LeftMessage(message);
              break;
            }

            message = string.Format($"{ClientName}: {message}");
            Console.WriteLine(message);

            ChatServer.Instance.BroadcastMessage(message, ClientName);
          }
          catch
          {
            LeftMessage(message);
            break;
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        ChatServer.Instance.RemoveConnection(ClientName);
        Close();
      }
    }

    public string GetMessage()
    {
      byte[] data = new byte[64];
      StringBuilder builder = new StringBuilder();
      int bytes = 0;
      do
      {
        bytes = Stream.Read(data, 0, data.Length);
        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
      }
      while (Stream.DataAvailable);

      return builder.ToString();
    }

    private void LeftMessage(string message)
    {
      message = string.Format($"{ClientName} left chat");
      Console.WriteLine(message);

      ChatServer.Instance.BroadcastMessage(message, ClientName);
    }

    public void SendMessage(string message)
    {
      byte[] data = Encoding.Unicode.GetBytes(message);
      Stream?.Write(data, 0, data.Length);
    }

    public void Close()
    {
      try
      {
        if (Stream != null)
          Stream.Close();

        if (_client != null)
          _client.Close();
      }
      catch
      {
      }
    }
  }
}