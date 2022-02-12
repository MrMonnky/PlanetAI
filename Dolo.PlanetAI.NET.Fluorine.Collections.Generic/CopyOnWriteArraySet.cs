using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

internal class CopyOnWriteArraySet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
{
	private CopyOnWriteArray<T> _array;

	public bool IsReadOnly => false;

	public int Count => _array.Count;

	public bool IsSynchronized => false;

	public object SyncRoot => _array.SyncRoot;

	public CopyOnWriteArraySet()
	{
		_array = new CopyOnWriteArray<T>();
	}

	public CopyOnWriteArraySet(ICollection collection)
	{
		_array = new CopyOnWriteArray<T>();
		foreach (object item in collection)
		{
			_array.Add((T)item);
		}
	}

	public void Add(T item)
	{
		if (!_array.Contains(item))
		{
			_array.Add(item);
		}
	}

	public void Clear()
	{
		_array.Clear();
	}

	public bool Contains(T item)
	{
		return _array.Contains(item);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		_array.CopyTo(array, arrayIndex);
	}

	public bool Remove(T item)
	{
		return _array.Remove(item);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return _array.GetEnumerator();
	}

	public void CopyTo(Array array, int index)
	{
		_array.CopyTo(array, index);
	}

	public IEnumerator GetEnumerator()
	{
		return _array.GetEnumerator();
	}
}
