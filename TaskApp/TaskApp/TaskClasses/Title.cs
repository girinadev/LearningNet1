using System;

namespace TaskApp.TaskClasses
{
  public class Title
  {
    private string _inner;

    public Title(string inner)
    {
      this._inner = inner;
    }

    public void Show()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"Title: {this._inner}");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
