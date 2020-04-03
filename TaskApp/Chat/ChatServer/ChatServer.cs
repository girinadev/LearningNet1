using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
  public class ChatServer
  {
    private static ChatServer _instance;
    private static TcpListener _tcpListener;

    private List<ClientConnection> _clients = new List<ClientConnection>();

    public static ChatServer Instance
    {
      get
      {
        if (_instance == null)
          _instance = new ChatServer();

        return _instance;
      }
    }

    public void Listening()
    {
      try
      {
        _tcpListener = new TcpListener(IPAddress.Any, 7722);
        _tcpListener.Start();
        Console.WriteLine("Server has started. Waiting connections...");

        while (true)
        {
          var tcpClient = _tcpListener.AcceptTcpClient();

          if (TryAddConnection(tcpClient, out ClientConnection clientConnection))
          {
            clientConnection.SendMessage(".ok");

            Task.Factory.StartNew(clientConnection.Process);
          }
          else
          {
            clientConnection.SendMessage(".exist");
            clientConnection.Close();

            Console.WriteLine(string.Format($"User '{clientConnection.ClientName}' exist"));
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Disconnect();
      }
    }

    private bool TryAddConnection(TcpClient tcpClient, out ClientConnection clientConnection)
    {
      clientConnection = new ClientConnection(tcpClient);
      string clientName = clientConnection.GetMessage();
      clientConnection.ClientName = clientName;

      if (_clients.Any(c => c.ClientName.Equals(clientName)) || ".quit".Equals(clientName))
        return false;

      _clients.Add(clientConnection);

      return true;
    }

    public void RemoveConnection(string clientName)
    {
      var client = _clients.FirstOrDefault(c => c.ClientName == clientName);
      if (client != null)
        _clients.Remove(client);
    }

    public void BroadcastMessage(string message, string clientName)
    {
      byte[] data = Encoding.Unicode.GetBytes(message);

      Parallel.ForEach(_clients.Where(c => !c.ClientName.Equals(clientName)),
        (c) => c.Stream?.Write(data, 0, data.Length));
    }

    public void Disconnect()
    {
      _tcpListener.Stop();

      foreach (var client in _clients)
      {
        client.Close();
      }
    }
  }
}