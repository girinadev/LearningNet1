using System;

namespace TaskApp.TaskInheritance
{
  public class ThreeDPrinter : Printer
  {
    public ThreeDPrinter(ConsoleColor defaultColor) : base(defaultColor)
    {
    }

    public override void Print(string value)
    {
      Console.ForegroundColor = Color;
      Console.WriteLine(value);
      Console.ForegroundColor = DefaultColor;
    }
  }
}
