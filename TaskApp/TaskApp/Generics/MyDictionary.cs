using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaskApp.Generics
{
  public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
  {
    private KeyValuePair<TKey, TValue>[] _collection = new KeyValuePair<TKey, TValue>[0];

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return new MyListEnumerator<KeyValuePair<TKey, TValue>>(_collection);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      if (_collection.Contains(item))
        throw new ArgumentException(nameof(item.Key));

      Array.Resize(ref _collection, _collection.Length + 1);
      _collection[_collection.Length - 1] = item;
    }

    public void Clear()
    {
      _collection = new KeyValuePair<TKey, TValue>[0];
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return _collection.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException();

      Array.Copy(_collection, array, arrayIndex);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return Remove(item.Key);
    }

    public int Count => _collection.Length;

    public bool IsReadOnly => _collection.IsReadOnly;

    public void Add(TKey key, TValue value)
    {
      Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool ContainsKey(TKey key)
    {
      foreach (var keyValuePair in _collection)
      {
        if (key.Equals(keyValuePair.Key))
          return true;
      }

      return false;
    }

    public bool Remove(TKey key)
    {
      if (_collection.IsReadOnly)
        throw new NotSupportedException();

      var destinationArray = new KeyValuePair<TKey, TValue>[_collection.Length - 1];
      var j = 0;
      bool isExist = false;
      foreach (var keyValuePair in _collection)
      {
        if (keyValuePair.Key.Equals(key))
        {
          isExist = true;
          continue;
        }

        destinationArray[j] = keyValuePair;
        j++;
      }

      _collection = destinationArray;

      return isExist;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      foreach (var keyValuePair in _collection)
      {
        if (keyValuePair.Key.Equals(key))
        {
          value = keyValuePair.Value;
          return true;
        }
      }

      value = default(TValue);
      return false;
    }

    public TValue this[TKey key]
    {
      get => _collection.FirstOrDefault(kv => kv.Key.Equals(key)).Value;
      set
      {
        for (int i = 0; i < _collection.Length; i++)
        {
          if (_collection[i].Key.Equals(key))
          {
            _collection[i] = new KeyValuePair<TKey, TValue>(key, value);
          }
        }
      }
    }

    public ICollection<TKey> Keys => _collection.Select(kv => kv.Key).ToArray();
    public ICollection<TValue> Values => _collection.Select(kv => kv.Value).ToArray();
  }
}
