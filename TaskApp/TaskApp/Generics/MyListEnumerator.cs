using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskApp.Generics
{
  public class MyListEnumerator<T> : IEnumerator<T>
  {
    private T[] _collection;
    private int currentIndex;
    private T current;

    public MyListEnumerator(T[] collection)
    {
      _collection = collection;
      currentIndex = -1;
      current = default(T);
    }

    public bool MoveNext()
    {
      //Avoids going beyond the end of the collection.
      if (++currentIndex >= _collection.Length)
      {
        return false;
      }

      // Set current box to next item in collection.
      current = _collection[currentIndex];
      return true;
    }

    public void Reset() { currentIndex = -1; }

    void IDisposable.Dispose() { }

    public T Current => current;

    object IEnumerator.Current => Current;
  }
}
