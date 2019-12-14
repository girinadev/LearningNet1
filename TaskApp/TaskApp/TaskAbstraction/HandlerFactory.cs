using System;
using System.IO;

namespace TaskApp.TaskAbstraction
{
  public class HandlerFactory : IHandlerFactory
  {
    public AbstractHandler GetHandler(string fileName)
    {
      var ext = GetFileExt(fileName);
      if (string.IsNullOrEmpty(ext))
        throw new Exception("Wrong file name format.");

      if (".doc".Equals(ext, StringComparison.OrdinalIgnoreCase))
        return new DOCHandler(fileName);

      if (".txt".Equals(ext, StringComparison.OrdinalIgnoreCase))
        return new TXTHandler(fileName);

      if (".xml".Equals(ext, StringComparison.OrdinalIgnoreCase))
        return new XMLHandler(fileName);

      throw new Exception($"Handler for '{ext}' is undefined");
    }

    private string GetFileExt(string fileName)
    {
      return Path.HasExtension(fileName) ? Path.GetExtension(fileName) : null;
    }
  }
}