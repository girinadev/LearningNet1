using System;

namespace TaskApp.TaskAbstraction
{
  public class XMLHandler : AbstractHandler
  {
    private readonly string _fileName;

    public XMLHandler(string fileName) : base(fileName)
    {
      _fileName = fileName;
    }

    public override void Open()
    {
      Console.WriteLine($"Xml file '{_fileName}' opened");
    }

    public override void Create()
    {
      Console.WriteLine($"Xml file '{_fileName}' created");
    }

    public override void Change()
    {
      Console.WriteLine($"Xml file '{_fileName}' changed");
    }

    public override void Save()
    {
      Console.WriteLine($"Xml file '{_fileName}' saved");
    }
  }
}