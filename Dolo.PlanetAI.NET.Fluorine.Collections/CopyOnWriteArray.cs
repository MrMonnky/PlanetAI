using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class CopyOnWriteArray : IList, ICollection, IEnumerable
{
	private object[] _array;

	private static object _objLock = new object();

	public bool IsFixedSize => false;

	public bool IsReadOnly => false;

	public object this[int index]
	{
		get
		{
			return _array[index];
		}
		set
		{
			lock (SyncRoot)
			{
				object obj = _array[index];
				if (obj != value && (value == null || !value.Equals(obj)))
				{
					object[] array = new object[_array.Length];
					Array.Copy(_array, 0, array, 0, _array.Length);
					array[index] = value;
					_array = array;
				}
			}
		}
	}

	public int Count => _array.Length;

	public bool IsSynchronized => true;

	public object SyncRoot => _objLock;

	public CopyOnWriteArray()
	{
		_array = new object[0];
	}

	public CopyOnWriteArray(IList list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		_array = new object[list.Count];
		int num = 0;
		foreach (object item in list)
		{
			_array[num++] = item;
		}
	}

	private void Copy(object[] src, int offset, int count)
	{
		lock (SyncRoot)
		{
			_array = new object[count];
			Array.Copy(src, offset, _array, 0, count);
		}
	}

	private static int IndexOf(object elem, object[] elementData, int length)
	{
		if (elem == null)
		{
			for (int i = 0; i < length; i++)
			{
				if (elementData[i] == null)
				{
					return i;
				}
			}
		}
		else
		{
			for (int j = 0; j < length; j++)
			{
				if (elem.Equals(elementData[j]))
				{
					return j;
				}
			}
		}
		return -1;
	}

	public int Add(object value)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			object[] array = new object[num + 1];
			Array.Copy(_array, 0, array, 0, num);
			array[num] = value;
			_array = array;
			return num;
		}
	}

	public void Clear()
	{
		lock (SyncRoot)
		{
			_array = new object[0];
		}
	}

	public bool Contains(object value)
	{
		return IndexOf(value) > -1;
	}

	public int IndexOf(object value)
	{
		object[] array = _array;
		int length = array.Length;
		return IndexOf(value, array, length);
	}

	public void Insert(int index, object value)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			object[] array = new object[num + 1];
			Array.Copy(_array, 0, array, 0, index);
			array[index] = value;
			Array.Copy(_array, index, array, index + 1, num - index);
			_array = array;
		}
	}

	public void Remove(object value)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			if (num == 0)
			{
				return;
			}
			int num2 = num - 1;
			object[] array = new object[num2];
			for (int i = 0; i < num2; i++)
			{
				if (value == _array[i] || (value != null && value.Equals(_array[i])))
				{
					for (int j = i + 1; j < num; j++)
					{
						array[j - 1] = _array[j];
					}
					_array = array;
					return;
				}
				array[i] = _array[i];
			}
			if (value == _array[num2] || (value != null && value.Equals(_array[num2])))
			{
				_array = array;
			}
		}
	}

	public void RemoveAt(int index)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			object obj = _array[index];
			object[] array = new object[num - 1];
			Array.Copy(_array, 0, array, 0, index);
			int num2 = num - index - 1;
			if (num2 > 0)
			{
				Array.Copy(_array, index + 1, array, index, num2);
			}
			_array = array;
		}
	}

	public void CopyTo(Array array, int index)
	{
		Array.Copy(_array, 0, array, index, array.Length - index);
	}

	public IEnumerator GetEnumerator()
	{
		return _array.GetEnumerator();
	}
}
