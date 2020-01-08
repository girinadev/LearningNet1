using System;

namespace TaskApp.TaskStruct
{
  public static class ConsolePrinter
  {
    public static void Print(string stroka, int color)
    {
      if (!Enum.TryParse(typeof(ConsoleColor), color.ToString(), out var c)) return;

      var defaultForegroundColor = Console.ForegroundColor;

      Console.ForegroundColor = (ConsoleColor)c;
      Console.WriteLine(stroka);

      Console.ForegroundColor = defaultForegroundColor;
    }
  }
}
