using System;
using System.Collections;

namespace TaskApp.Generics
{
  public class MyArrayList : IEnumerable, IList, ICollection, ICloneable
  {
    private object[] _collection = new object[0];

    public IEnumerator GetEnumerator()
    {
      return _collection.GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
      Array.Copy(_collection, array, index);
    }

    public int Count => _collection.Length;

    public bool IsSynchronized => _collection.IsSynchronized;

    public object SyncRoot => _collection.SyncRoot;

    public int Add(object value)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      Array.Resize(ref _collection, _collection.Length + 1);
      int newIndex = _collection.Length - 1;
      _collection[newIndex] = value;

      return newIndex;
    }

    public void Clear()
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      _collection = new object[0];
    }

    public bool Contains(object value)
    {
      for (var i = 0; i < _collection.Length; i++)
      {
        if (_collection[i] != null && _collection[i].Equals(value))
          return true;
      }
      return false;
    }

    public int IndexOf(object value)
    {
      if (value != null)
      {
        for (var i = 0; i < _collection.Length; i++)
        {
          if (value.Equals(_collection[i]))
            return i;
        }
      }

      return -1;
    }

    public void Insert(int index, object value)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      if (index < 0)
        throw new ArgumentOutOfRangeException();

      _collection[index] = value;
    }

    public void Remove(object value)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      var index = IndexOf(value);
      if (index == -1)
        return;

      RemoveAt(index);
    }

    public void RemoveAt(int index)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      if (index < 0)
        throw new ArgumentOutOfRangeException();

      var destinationArray = new object[_collection.Length - 1];
      var j = 0;
      for (var i = 0; i < _collection.Length; i++)
      {
        if (i.Equals(index))
          continue;

        destinationArray[j] = _collection[i];
        j++;
      }
    }

    public bool IsFixedSize => _collection.IsFixedSize;

    public bool IsReadOnly => _collection.IsReadOnly;

    public object this[int index]
    {
      get => _collection[index];
      set => _collection[index] = value;
    }

    public object Clone()
    {
      var newCollection = new object[_collection.Length];

      for (var i = 0; i < _collection.Length; i++)
      {
        newCollection[i] = ((ICloneable)_collection[i]).Clone();
      }

      return newCollection;
    }
  }
}
