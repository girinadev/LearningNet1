using System;

namespace ChatClient
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Console.Write("Enter your name: ");
        ChatClient.Instance.UserName = Console.ReadLine();
        ChatClient.Instance.Connect();
      }
      catch
      {
        ChatClient.Instance.Disconnect();
      }
    }
  }
}