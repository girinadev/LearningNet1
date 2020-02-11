using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskApp.Generics
{
  public class MyList<T> : IList<T>
  {
    private T[] _collection = new T[0];

    public T this[int index]
    {
      get => _collection[index];
      set => _collection[index] = value;
    }

    public int Count => _collection.Length;

    public bool IsReadOnly => _collection.IsReadOnly;

    public void Add(T item)
    {
      Array.Resize(ref _collection, _collection.Length + 1);
      _collection[_collection.Length - 1] = item;
    }

    public void Clear()
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      _collection = new T[0];
    }

    public bool Contains(T item)
    {
      for (var i = 0; i < _collection.Length; i++)
      {
        if (_collection[i] != null && _collection[i].Equals(item))
          return true;
      }
      return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException();

      Array.Copy(_collection, array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return new MyListEnumerator<T>(_collection);
    }

    public int IndexOf(T item)
    {
      if (item != null)
      {
        for (var i = 0; i < _collection.Length; i++)
        {
          if (item.Equals(_collection[i]))
            return i;
        }
      }

      return -1;
    }

    public void Insert(int index, T item)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      if (index < 0)
        throw new ArgumentOutOfRangeException();

      _collection[index] = item;
    }

    public bool Remove(T item)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      var index = IndexOf(item);
      if (index == -1)
        return false;

      RemoveAt(index);

      return true;
    }

    public void RemoveAt(int index)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      if (index < 0)
        throw new ArgumentOutOfRangeException();

      var destinationArray = new T[_collection.Length - 1];
      var j = 0;
      for (var i = 0; i < _collection.Length; i++)
      {
        if (i.Equals(index))
          continue;

        destinationArray[j] = _collection[i];
        j++;
      }

      _collection = destinationArray;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _collection.GetEnumerator();
    }
  }
}
