using System;

namespace TaskApp.TaskClasses
{
  public class Content
  {
    private string _inner;

    public Content(string inner)
    {
      this._inner = inner;
    }

    public void Show()
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Content: {this._inner}");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
