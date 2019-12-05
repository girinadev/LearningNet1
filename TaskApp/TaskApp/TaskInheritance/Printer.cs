using System;

namespace TaskApp.TaskInheritance
{
  public abstract class Printer
  {
    protected readonly ConsoleColor DefaultColor;

    protected Printer(ConsoleColor defaultColor)
    {
      DefaultColor = defaultColor;
      Color = defaultColor;
    }

    public abstract void Print(string value);

    public ConsoleColor Color { get; set; }
  }
}
