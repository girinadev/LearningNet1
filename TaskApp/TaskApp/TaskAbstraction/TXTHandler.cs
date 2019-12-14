using System;

namespace TaskApp.TaskAbstraction
{
  public class TXTHandler : AbstractHandler
  {
    private readonly string _fileName;

    public TXTHandler(string fileName) : base(fileName)
    {
      _fileName = fileName;
    }

    public override void Open()
    {
      Console.WriteLine($"Txt file '{_fileName}' opened");
    }

    public override void Create()
    {
      Console.WriteLine($"Txt file '{_fileName}' created");
    }

    public override void Change()
    {
      Console.WriteLine($"Txt file '{_fileName}' changed");
    }

    public override void Save()
    {
      Console.WriteLine($"Txt file '{_fileName}' saved");
    }
  }
}