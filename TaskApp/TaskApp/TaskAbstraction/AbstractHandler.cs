using System;

namespace TaskApp.TaskAbstraction
{
  public abstract class AbstractHandler
  {
    abstract void Open();
    abstract void Create();
    abstract void Chenge();
    abstract void Save();
  }
}