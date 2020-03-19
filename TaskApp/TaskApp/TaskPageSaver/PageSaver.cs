using System;
using System.IO;

namespace TaskApp.TaskPageSaver
{
  public static class PageSaver
  {
    public static void LoadAndSave(string ulr)
    {
      var uri = new Uri(ulr);

      var content = PageLoader.GetContent(uri.Host, uri.PathAndQuery);
      var ext = Path.GetExtension(ulr);
      var name = Path.GetFileName(ulr);

      var path = Path.Combine("D:\\", $"output_{name}{ext}");
      File.WriteAllText(path, content);
    }
  }
}
