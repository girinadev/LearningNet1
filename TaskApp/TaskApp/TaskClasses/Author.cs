using System;

namespace TaskApp.TaskClasses
{
  public class Author
  {
    private string _inner;

    public Author(string inner)
    {
      this._inner = inner;
    }

    public void Show()
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine($"Author: {this._inner}");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
