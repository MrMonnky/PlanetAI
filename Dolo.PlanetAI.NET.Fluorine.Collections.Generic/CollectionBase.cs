using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

internal class CollectionBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
{
	private List<T> _innerList;

	public virtual T this[int index]
	{
		get
		{
			return _innerList[index];
		}
		set
		{
			T oldValue = _innerList[index];
			if (OnValidate(value) && OnSet(index, oldValue, value))
			{
				_innerList[index] = value;
				OnSetComplete(index, oldValue, value);
			}
		}
	}

	public virtual int Count => _innerList.Count;

	public virtual bool IsReadOnly => ((ICollection<T>)_innerList).IsReadOnly;

	public virtual bool IsFixedSize => ((IList)_innerList).IsFixedSize;

	object IList.this[int index]
	{
		get
		{
			return _innerList[index];
		}
		set
		{
			T oldValue = _innerList[index];
			if (OnValidate((T)value) && OnSet(index, oldValue, (T)value))
			{
				_innerList[index] = (T)value;
				OnSetComplete(index, oldValue, (T)value);
			}
		}
	}

	public virtual bool IsSynchronized => ((ICollection)_innerList).IsSynchronized;

	public virtual object SyncRoot => ((ICollection)_innerList).SyncRoot;

	public CollectionBase()
		: this(10)
	{
	}

	public CollectionBase(int initialCapacity)
	{
		_innerList = new List<T>(initialCapacity);
	}

	public virtual void Clear()
	{
		if (OnClear())
		{
			_innerList.Clear();
			OnClearComplete();
		}
	}

	protected virtual bool OnClear()
	{
		return true;
	}

	protected virtual void OnClearComplete()
	{
	}

	protected virtual bool OnInsert(int index, T value)
	{
		return true;
	}

	protected virtual void OnInsertComplete(int index, T value)
	{
	}

	protected virtual bool OnRemove(int index, T value)
	{
		return true;
	}

	protected virtual void OnRemoveComplete(int index, T value)
	{
	}

	protected virtual bool OnSet(int index, T oldValue, T value)
	{
		return true;
	}

	protected virtual void OnSetComplete(int index, T oldValue, T newValue)
	{
	}

	protected virtual bool OnValidate(T value)
	{
		return true;
	}

	public virtual int IndexOf(T item)
	{
		return _innerList.IndexOf(item);
	}

	public virtual void Insert(int index, T item)
	{
		if (OnValidate(item) && OnInsert(index, item))
		{
			_innerList.Insert(index, item);
			OnInsertComplete(index, item);
		}
	}

	public virtual void RemoveAt(int index)
	{
		T value = _innerList[index];
		if (OnValidate(value) && OnRemove(index, value))
		{
			_innerList.RemoveAt(index);
			OnRemoveComplete(index, value);
		}
	}

	public virtual void Add(T item)
	{
		if (OnValidate(item) && OnInsert(_innerList.Count, item))
		{
			_innerList.Add(item);
			OnInsertComplete(_innerList.Count - 1, item);
		}
	}

	public virtual bool Contains(T item)
	{
		return _innerList.Contains(item);
	}

	public virtual void CopyTo(T[] array, int arrayIndex)
	{
		_innerList.CopyTo(array, arrayIndex);
	}

	public virtual bool Remove(T item)
	{
		int num = _innerList.IndexOf(item);
		if (num < 0)
		{
			return false;
		}
		if (!OnValidate(item))
		{
			return false;
		}
		if (!OnRemove(num, item))
		{
			return false;
		}
		_innerList.Remove(item);
		OnRemoveComplete(num, item);
		return true;
	}

	public virtual IEnumerator<T> GetEnumerator()
	{
		return _innerList.GetEnumerator();
	}

	public virtual int Add(object value)
	{
		int count = _innerList.Count;
		if (!OnValidate((T)value))
		{
			return -1;
		}
		if (!OnInsert(count, (T)value))
		{
			return -1;
		}
		count = ((IList)_innerList).Add(value);
		OnInsertComplete(count, (T)value);
		return count;
	}

	public virtual bool Contains(object value)
	{
		return ((IList)_innerList).Contains(value);
	}

	public virtual int IndexOf(object value)
	{
		return ((IList)_innerList).IndexOf(value);
	}

	public virtual void Insert(int index, object value)
	{
		if (OnValidate((T)value) && OnInsert(index, (T)value))
		{
			((IList)_innerList).Insert(index, value);
			OnInsertComplete(index, (T)value);
		}
	}

	public virtual void Remove(object value)
	{
		int num = _innerList.IndexOf((T)value);
		if (num >= 0 && OnValidate((T)value) && OnRemove(num, (T)value))
		{
			((IList)_innerList).Remove(value);
			OnRemoveComplete(num, (T)value);
		}
	}

	public virtual void CopyTo(Array array, int index)
	{
		((ICollection)_innerList).CopyTo(array, index);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)_innerList).GetEnumerator();
	}
}
