using System;

namespace TaskApp.TaskInheritance
{
  public class GoodPupil : Pupil
  {
    public override void Read()
    {
      Console.WriteLine("GoodPupil.Read.");
    }

    public override void Relax()
    {
      Console.WriteLine("GoodPupil.Relax.");
    }

    public override void Study()
    {
      Console.WriteLine("GoodPupil.Study.");
    }

    public override void Write()
    {
      Console.WriteLine("GoodPupil.Write.");
    }
  }
}
