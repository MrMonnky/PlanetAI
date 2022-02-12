using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

internal class CopyOnWriteArray<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
{
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private T[] list;

		private int index;

		private T current;

		public T Current => current;

		object IEnumerator.Current
		{
			get
			{
				if (index == 0 || index == list.Length + 1)
				{
					throw new InvalidOperationException();
				}
				return Current;
			}
		}

		internal Enumerator(T[] list)
		{
			this.list = list;
			index = 0;
			current = default(T);
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			if (index < list.Length)
			{
				current = list[index];
				index++;
				return true;
			}
			index = list.Length + 1;
			current = default(T);
			return false;
		}

		void IEnumerator.Reset()
		{
			index = 0;
			current = default(T);
		}
	}

	private T[] _array;

	private static object _objLock = new object();

	public T this[int index]
	{
		get
		{
			return _array[index];
		}
		set
		{
			lock (SyncRoot)
			{
				T val = _array[index];
				if (value == null || !value.Equals(val))
				{
					T[] array = new T[_array.Length];
					Array.Copy(_array, 0, array, 0, _array.Length);
					array[index] = value;
					_array = array;
				}
			}
		}
	}

	public int Count => _array.Length;

	public bool IsReadOnly => false;

	public bool IsFixedSize => false;

	object IList.this[int index]
	{
		get
		{
			return _array[index];
		}
		set
		{
			lock (SyncRoot)
			{
				T val = _array[index];
				if (!value.Equals(val))
				{
					T[] array = new T[_array.Length];
					Array.Copy(_array, 0, array, 0, _array.Length);
					array[index] = (T)value;
					_array = array;
				}
			}
		}
	}

	public bool IsSynchronized => true;

	public object SyncRoot => _objLock;

	public CopyOnWriteArray()
	{
		_array = new T[0];
	}

	public CopyOnWriteArray(IList<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		_array = new T[list.Count];
		int num = 0;
		foreach (T item in list)
		{
			_array[num++] = item;
		}
	}

	private void Copy(T[] src, int offset, int count)
	{
		lock (SyncRoot)
		{
			_array = new T[count];
			Array.Copy(src, offset, _array, 0, count);
		}
	}

	private static int IndexOf(T elem, T[] elementData, int length)
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

	public int IndexOf(T item)
	{
		T[] array = _array;
		int length = array.Length;
		return IndexOf(item, array, length);
	}

	public void Insert(int index, T item)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			T[] array = new T[num + 1];
			Array.Copy(_array, 0, array, 0, index);
			array[index] = item;
			Array.Copy(_array, index, array, index + 1, num - index);
			_array = array;
		}
	}

	public void RemoveAt(int index)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			T val = _array[index];
			T[] array = new T[num - 1];
			Array.Copy(_array, 0, array, 0, index);
			int num2 = num - index - 1;
			if (num2 > 0)
			{
				Array.Copy(_array, index + 1, array, index, num2);
			}
			_array = array;
		}
	}

	public void Add(T item)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			T[] array = new T[num + 1];
			Array.Copy(_array, 0, array, 0, num);
			array[num] = item;
			_array = array;
		}
	}

	public void Clear()
	{
		lock (SyncRoot)
		{
			_array = new T[0];
		}
	}

	public bool Contains(T item)
	{
		return IndexOf(item) > -1;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(_array, 0, array, arrayIndex, array.Length - arrayIndex);
	}

	public bool Remove(T item)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			if (num == 0)
			{
				return false;
			}
			int num2 = num - 1;
			T[] array = new T[num2];
			for (int i = 0; i < num2; i++)
			{
				if (item.Equals(_array[i]))
				{
					for (int j = i + 1; j < num; j++)
					{
						array[j - 1] = _array[j];
					}
					_array = array;
					return true;
				}
				array[i] = _array[i];
			}
			if (item.Equals(_array[num2]))
			{
				_array = array;
			}
			return true;
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new Enumerator(_array);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _array.GetEnumerator();
	}

	public int Add(object value)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			T[] array = new T[num + 1];
			Array.Copy(_array, 0, array, 0, num);
			array[num] = (T)value;
			_array = array;
			return num;
		}
	}

	public bool Contains(object value)
	{
		return IndexOf(value) > -1;
	}

	public int IndexOf(object value)
	{
		T[] array = _array;
		int length = array.Length;
		return IndexOf((T)value, array, length);
	}

	public void Insert(int index, object value)
	{
		lock (SyncRoot)
		{
			int num = _array.Length;
			T[] array = new T[num + 1];
			Array.Copy(_array, 0, array, 0, index);
			array[index] = (T)value;
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
			T[] array = new T[num2];
			for (int i = 0; i < num2; i++)
			{
				if (value.Equals(_array[i]))
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
			if (value.Equals(_array[num2]))
			{
				_array = array;
			}
		}
	}

	public void CopyTo(Array array, int index)
	{
		Array.Copy(_array, 0, array, index, array.Length - index);
	}
}
