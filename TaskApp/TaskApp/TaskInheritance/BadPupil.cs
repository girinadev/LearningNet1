using System;

namespace TaskApp.TaskInheritance
{
  public class BadPupil : Pupil
  {
    public override void Read()
    {
      Console.WriteLine("BadPupil.Read.");
    }

    public override void Relax()
    {
      Console.WriteLine("BadPupil.Relax.");
    }

    public override void Study()
    {
      Console.WriteLine("BadPupil.Study.");
    }

    public override void Write()
    {
      Console.WriteLine("BadPupil.Write.");
    }
  }
}
