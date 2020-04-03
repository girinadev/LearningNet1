using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
  public class ChatClient
  {
    private static ChatClient _instance;
    private TcpClient _client;
    private NetworkStream _stream;

    public string UserName { get; set; }

    public static ChatClient Instance
    {
      get
      {
        if (_instance == null)
          _instance = new ChatClient();

        return _instance;
      }
    }

    public void Connect()
    {
      _client = new TcpClient();
      try
      {
        _client.Connect("127.0.0.1", 7722);
        _stream = _client.GetStream();

        string message = UserName;
        byte[] data = Encoding.Unicode.GetBytes(message);
        _stream.Write(data, 0, data.Length);
        
        Task.Factory.StartNew(ReceiveMessage);

        SendMessage();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        Disconnect();
      }
    }

    public void Disconnect()
    {
      if (_stream != null)
        _stream.Close();

      if (_client != null)
        _client.Close();      

      Environment.Exit(0);
    }

    private void SendMessage()
    {
      while (true)
      {
        string message = Console.ReadLine();        

        byte[] data = Encoding.Unicode.GetBytes(message);
        _stream.Write(data, 0, data.Length);

        if (".quit".Equals(message))
        {
          Disconnect();
          return;
        }
      }
    }

    private void ReceiveMessage()
    {
      while (true)
      {
        try
        {
          byte[] data = new byte[64];
          StringBuilder builder = new StringBuilder();
          int bytes = 0;
          do
          {
            bytes = _stream.Read(data, 0, data.Length);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
          }
          while (_stream.DataAvailable);

          string message = builder.ToString();

          if (".exist".Equals(message))
          {
            Console.WriteLine("User name already exist");            
            Console.WriteLine("Disconected");
            Console.ReadLine();
            Disconnect();
            return;
          }
          else if (".ok".Equals(message))
          {
            Console.WriteLine("Hi {0}", UserName);
            Console.WriteLine("Input message: ");
          }
          else if (!string.IsNullOrEmpty(message))
          {
            Console.WriteLine(message);
          }
        }
        catch
        {
          Console.WriteLine("Disconected");
          Disconnect();
        }
      }
    }
  }
}
