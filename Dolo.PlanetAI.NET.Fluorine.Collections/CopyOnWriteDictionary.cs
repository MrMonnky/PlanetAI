using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class CopyOnWriteDictionary : IDictionary, ICollection, IEnumerable
{
	private Hashtable _dictionary;

	public bool IsFixedSize => _dictionary.IsFixedSize;

	public bool IsReadOnly => _dictionary.IsReadOnly;

	public ICollection Keys => _dictionary.Keys;

	public ICollection Values => _dictionary.Values;

	public object this[object key]
	{
		get
		{
			return _dictionary[key];
		}
		set
		{
			lock (SyncRoot)
			{
				Hashtable hashtable = new Hashtable(_dictionary);
				hashtable[key] = value;
				_dictionary = hashtable;
			}
		}
	}

	public int Count => _dictionary.Count;

	public bool IsSynchronized => true;

	public object SyncRoot => _dictionary.SyncRoot;

	public CopyOnWriteDictionary()
	{
		_dictionary = new Hashtable();
	}

	public CopyOnWriteDictionary(int capacity)
	{
		_dictionary = new Hashtable(capacity);
	}

	public CopyOnWriteDictionary(IDictionary d)
	{
		_dictionary = new Hashtable(d);
	}

	public void Add(object key, object value)
	{
		lock (SyncRoot)
		{
			Hashtable hashtable = new Hashtable(_dictionary);
			hashtable.Add(key, value);
			_dictionary = hashtable;
		}
	}

	public void Clear()
	{
		lock (SyncRoot)
		{
			_dictionary = new Hashtable();
		}
	}

	public bool Contains(object key)
	{
		return _dictionary.ContainsKey(key);
	}

	public IDictionaryEnumerator GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}

	public void Remove(object key)
	{
		lock (SyncRoot)
		{
			if (Contains(key))
			{
				Hashtable hashtable = new Hashtable(_dictionary);
				hashtable.Remove(key);
				_dictionary = hashtable;
			}
		}
	}

	public void CopyTo(Array array, int index)
	{
		_dictionary.CopyTo(array, index);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}
}
