using System;

namespace TaskApp.TaskInheritance
{
  public class DocumentWorker
  {
    public virtual void OpenDocument()
    {
      Console.WriteLine("Документ открыт");
    }

    public virtual void EditDocument()
    {
      Console.WriteLine("Редактирование документа доступно в версии Про");
    }

    public virtual void SaveDocument()
    {
      Console.WriteLine("Сохранение документа доступно в версии Про");
    }
  }
}
