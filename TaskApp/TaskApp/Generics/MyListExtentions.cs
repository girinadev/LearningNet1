using System.Linq;

namespace TaskApp.Generics
{
  public static class MyListExtentions
  {
    public static T[] GetArray<T>(this MyList<T> list)
    {
      return list.ToArray();
    }
  }
}
