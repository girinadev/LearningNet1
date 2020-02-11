namespace TaskApp.Generics
{
  public static class MyListExtentions
  {
    public static T[] GetArray<T>(this MyList<T> list)
    {
      var array = new T[list.Count];
      for (var i = 0; i < list.Count; i++)
      {
        array[i] = list[i];
      }
      return array;
    }
  }
}
