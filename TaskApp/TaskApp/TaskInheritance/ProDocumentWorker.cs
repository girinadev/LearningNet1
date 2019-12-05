using System;

namespace TaskApp.TaskInheritance
{
  public class ProDocumentWorker : DocumentWorker
  {
    public override void OpenDocument()
    {
      Console.WriteLine("Документ отредактирован");
    }

    public override void EditDocument()
    {
      Console.WriteLine("Документ сохранен в старом формате, сохранение в остальных форматах доступно в версии Эксперт");
    }
  }
}
