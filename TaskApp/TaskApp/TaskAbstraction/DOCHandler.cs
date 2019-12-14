using System;

namespace TaskApp.TaskAbstraction
{
  public class DOCHandler : AbstractHandler
  {
    private readonly string _fileName;

    public DOCHandler(string fileName) : base(fileName)
    {
      _fileName = fileName;
    }

    public override void Open()
    {
      Console.WriteLine($"Doc file '{_fileName}' opened");
    }

    public override void Create()
    {
      Console.WriteLine($"Doc file '{_fileName}' created");
    }

    public override void Change()
    {
      Console.WriteLine($"Doc file '{_fileName}' changed");
    }

    public override void Save()
    {
      Console.WriteLine($"Doc file '{_fileName}' saved");
    }
  }
}