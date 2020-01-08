using System;

namespace TaskApp.TaskStruct
{
  public struct BirthdayCounter
  {
    public static int DaysLeft(DateTime birthday)
    {
      var futureBirthday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
      return futureBirthday.Subtract(DateTime.Now).Days;
    }
  }
}
