namespace TaskApp.Generics
{
  public class Factory<T> where T : new()
  {
    public static T FactoryMethod()
    {
      return new T();
    }
  }
}
