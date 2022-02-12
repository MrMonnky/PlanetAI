using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

[Serializable]
internal class SynchronizedHashtable : IDictionary, ICollection, IEnumerable, ICloneable
{
	private readonly Hashtable _table;

	public bool IsReadOnly
	{
		get
		{
			lock (SyncRoot)
			{
				return _table.IsReadOnly;
			}
		}
	}

	public bool IsFixedSize
	{
		get
		{
			lock (SyncRoot)
			{
				return _table.IsFixedSize;
			}
		}
	}

	public bool IsSynchronized => true;

	public ICollection Keys
	{
		get
		{
			lock (SyncRoot)
			{
				return _table.Keys;
			}
		}
	}

	public ICollection Values
	{
		get
		{
			lock (SyncRoot)
			{
				return _table.Values;
			}
		}
	}

	public object SyncRoot => _table.SyncRoot;

	public int Count
	{
		get
		{
			lock (SyncRoot)
			{
				return _table.Count;
			}
		}
	}

	public object this[object key]
	{
		get
		{
			lock (SyncRoot)
			{
				return _table[key];
			}
		}
		set
		{
			lock (SyncRoot)
			{
				_table[key] = value;
			}
		}
	}

	public SynchronizedHashtable()
	{
		_table = new Hashtable();
	}

	public SynchronizedHashtable(IDictionary dictionary)
	{
		ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
		_table = new Hashtable(dictionary);
	}

	public void Add(object key, object value)
	{
		lock (SyncRoot)
		{
			_table.Add(key, value);
		}
	}

	public void Clear()
	{
		lock (SyncRoot)
		{
			_table.Clear();
		}
	}

	public object Clone()
	{
		lock (SyncRoot)
		{
			return new SynchronizedHashtable(this);
		}
	}

	public bool Contains(object key)
	{
		lock (SyncRoot)
		{
			return _table.Contains(key);
		}
	}

	public bool ContainsKey(object key)
	{
		lock (SyncRoot)
		{
			return _table.ContainsKey(key);
		}
	}

	public bool ContainsValue(object value)
	{
		lock (SyncRoot)
		{
			return _table.ContainsValue(value);
		}
	}

	public void CopyTo(Array array, int index)
	{
		lock (SyncRoot)
		{
			_table.CopyTo(array, index);
		}
	}

	public IDictionaryEnumerator GetEnumerator()
	{
		lock (SyncRoot)
		{
			return new SynchronizedDictionaryEnumerator(SyncRoot, _table.GetEnumerator());
		}
	}

	public void Remove(object key)
	{
		lock (SyncRoot)
		{
			_table.Remove(key);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		lock (SyncRoot)
		{
			return new SynchronizedEnumerator(SyncRoot, ((IEnumerable)_table).GetEnumerator());
		}
	}

	public object AddIfAbsent(object key, object value)
	{
		lock (SyncRoot)
		{
			if (!_table.ContainsKey(key))
			{
				_table.Add(key, value);
				return value;
			}
			return _table[key];
		}
	}
}
