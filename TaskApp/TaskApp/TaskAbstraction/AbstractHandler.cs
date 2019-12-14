namespace TaskApp.TaskAbstraction
{
  public abstract class AbstractHandler
  {
    protected AbstractHandler(string fileName)
    {
    }

    public abstract void Open();
    public abstract void Create();
    public abstract void Change();
    public abstract void Save();
  }
}