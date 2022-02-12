using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class CopyOnWriteArraySet : ICollection, IEnumerable
{
	private CopyOnWriteArray _array;

	public int Count => _array.Count;

	public bool IsEmpty => _array.Count == 0;

	public bool IsSynchronized => false;

	public object SyncRoot => _array.SyncRoot;

	public CopyOnWriteArraySet()
	{
		_array = new CopyOnWriteArray();
	}

	public CopyOnWriteArraySet(ICollection collection)
	{
		_array = new CopyOnWriteArray();
		foreach (object item in collection)
		{
			_array.Add(item);
		}
	}

	public bool Contains(object value)
	{
		return _array.Contains(value);
	}

	public void Clear()
	{
		_array.Clear();
	}

	public void Remove(object obj)
	{
		_array.Remove(obj);
	}

	public bool Add(object value)
	{
		if (!_array.Contains(value))
		{
			_array.Add(value);
			return true;
		}
		return false;
	}

	public IEnumerator GetEnumerator()
	{
		return _array.GetEnumerator();
	}

	public void CopyTo(Array array, int index)
	{
		_array.CopyTo(array, index);
	}
}
