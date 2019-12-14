using System;

namespace TaskApp.TaskAbstraction
{
  public class Player : IPlayable, IRecodable
  {
    void IPlayable.Play()
    {
      Console.WriteLine("Playing...");
    }

    void IPlayable.Pause()
    {
      Console.WriteLine("Playing is paused.");
    }

    void IPlayable.Stop()
    {
      Console.WriteLine("Playing is stopped.");
    }

    void IRecodable.Record()
    {
      Console.WriteLine("Recording...");
    }

    void IRecodable.Pause()
    {
      Console.WriteLine("Recording is paused.");
    }

    void IRecodable.Stop()
    {
      Console.WriteLine("Recording is stopped.");
    }
  }
}
