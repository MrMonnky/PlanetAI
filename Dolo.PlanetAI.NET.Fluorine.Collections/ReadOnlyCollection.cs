using System;
using System.Collections;
using System.Threading;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class ReadOnlyCollection : ICollection, IEnumerable
{
	private ICollection _collection;

	private object _syncRoot;

	public int Count => _collection.Count;

	public bool IsSynchronized => false;

	public object SyncRoot
	{
		get
		{
			if (_syncRoot == null)
			{
				if (_collection != null)
				{
					_syncRoot = _collection.SyncRoot;
				}
				else
				{
					Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
			}
			return _syncRoot;
		}
	}

	public ReadOnlyCollection(ICollection collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("list");
		}
		_collection = collection;
	}

	public void CopyTo(Array array, int index)
	{
		_collection.CopyTo(array, index);
	}

	public IEnumerator GetEnumerator()
	{
		return _collection.GetEnumerator();
	}
}
