using System;
using System.Collections;
using System.Threading;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class ReadOnlyList : IList, ICollection, IEnumerable
{
	private IList _list;

	private object _syncRoot;

	public bool IsFixedSize => true;

	public bool IsReadOnly => true;

	public object this[int index]
	{
		get
		{
			return _list[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public int Count => _list.Count;

	public bool IsSynchronized => false;

	public object SyncRoot
	{
		get
		{
			if (_syncRoot == null)
			{
				ICollection list = _list;
				if (_list != null)
				{
					_syncRoot = _list.SyncRoot;
				}
				else
				{
					Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
			}
			return _syncRoot;
		}
	}

	public ReadOnlyList(IList list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		_list = list;
	}

	public int Add(object value)
	{
		throw new NotSupportedException();
	}

	public void Clear()
	{
		throw new NotSupportedException();
	}

	public bool Contains(object value)
	{
		return _list.Contains(value);
	}

	public int IndexOf(object value)
	{
		return _list.IndexOf(value);
	}

	public void Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	public void Remove(object value)
	{
		throw new NotSupportedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	public void CopyTo(Array array, int index)
	{
		_list.CopyTo(array, index);
	}

	public IEnumerator GetEnumerator()
	{
		return _list.GetEnumerator();
	}
}
