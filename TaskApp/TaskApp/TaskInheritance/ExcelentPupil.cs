using System;

namespace TaskApp.TaskInheritance
{
  public class ExcelentPupil : Pupil
  {
    public override void Read()
    {
      Console.WriteLine("ExcelentPupil.Read.");
    }

    public override void Relax()
    {
      Console.WriteLine("ExcelentPupil.Relax.");
    }

    public override void Study()
    {
      Console.WriteLine("ExcelentPupil.Study.");
    }

    public override void Write()
    {
      Console.WriteLine("ExcelentPupil.Write.");
    }
  }
}
