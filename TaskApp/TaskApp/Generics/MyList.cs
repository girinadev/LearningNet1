using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaskApp.Generics
{
  public class MyList<T> : IList<T>
  {
    private T[] _collection = new T[0];

    public T this[int index] { get => _collection[index]; set => _collection[index] = value; }

    public int Count => _collection.Length;

    public bool IsReadOnly => _collection.IsReadOnly;

    public void Add(T item)
    {
      Array.Resize(ref _collection, _collection.Length + 1);
      _collection[_collection.Length - 1] = item;
    }

    public void Clear()
    {
      _collection = new T[0];
    }

    public bool Contains(T item)
    {
      return _collection.Any<T>(x => x.Equals(item));
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      Array.Copy(_collection, array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return new MyListEnumerator<T>(_collection);
    }

    public int IndexOf(T item)
    {
      throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
      throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
      try
      {
        _collection = _collection.Where(x => x.Equals(item)).ToArray();
      }
      catch
      {
        return false;
      }

      return true;
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _collection.GetEnumerator();
    }
  }
}
