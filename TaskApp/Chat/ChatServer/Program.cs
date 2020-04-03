using System;

namespace ChatServer
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        ChatServer.Instance.Listening();
      }
      catch (Exception ex)
      {
        ChatServer.Instance.Disconnect();
        Console.WriteLine(ex.Message);
      }
    }
  }
}