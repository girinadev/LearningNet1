using System;

namespace TaskApp.TaskAbstraction
{
  public interface IHandlerFactory
  {
    AbstractHandler GetHandler(string type);
  }
}