using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

[TypeConverter(typeof(ArrayCollectionConverter))]
internal class ArrayCollection : IExternalizable, IList, ICollection, IEnumerable
{
	private IList _list;

	public int Count => (_list != null) ? _list.Count : 0;

	public IList List => _list;

	public bool IsReadOnly => _list.IsReadOnly;

	public object this[int index]
	{
		get
		{
			return _list[index];
		}
		set
		{
			_list[index] = value;
		}
	}

	public bool IsFixedSize => _list.IsFixedSize;

	public bool IsSynchronized => _list.IsSynchronized;

	public object SyncRoot => _list.SyncRoot;

	public ArrayCollection()
	{
		_list = new List<object>();
	}

	public ArrayCollection(IList list)
	{
		_list = list;
	}

	public object[] ToArray()
	{
		if (_list != null)
		{
			if (_list is ArrayList)
			{
				return ((ArrayList)_list).ToArray();
			}
			if (_list is List<object>)
			{
				return ((List<object>)_list).ToArray();
			}
			object[] array = new object[_list.Count];
			for (int i = 0; i < _list.Count; i++)
			{
				array[i] = _list[i];
			}
			return array;
		}
		return null;
	}

	public void ReadExternal(IDataInput input)
	{
		_list = input.ReadObject() as IList;
	}

	public void WriteExternal(IDataOutput output)
	{
		output.WriteObject(ToArray());
	}

	public void RemoveAt(int index)
	{
		_list.RemoveAt(index);
	}

	public void Insert(int index, object value)
	{
		_list.Insert(index, value);
	}

	public void Remove(object value)
	{
		_list.Remove(value);
	}

	public bool Contains(object value)
	{
		return _list.Contains(value);
	}

	public void Clear()
	{
		_list.Clear();
	}

	public int IndexOf(object value)
	{
		return _list.IndexOf(value);
	}

	public int Add(object value)
	{
		return _list.Add(value);
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
