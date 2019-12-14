using System;

namespace TaskApp.TaskAbstraction
{
  public class HandlerFactory : IHandlerFactory
  {
    public AbstractHandler GetHandler(string type)
    {
      if ("doc".Equals(type))
        return new DOCHandler();

      if ("txt".Equals(type))
        return new TXTHandler();

      if ("xml".Equals(type))
        return new XMLHandler();

      throw new Exeption($"Handler for '{type}' is undefined");
    }
  }
}