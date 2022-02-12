using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal abstract class SetBase : ISet, IModifiableCollection, ICollection, IEnumerable, IReversible, IComparable
{
	private IComparer _comparer;

	private bool _allowDuplicates;

	private RbTree _tree;

	private int _count = 0;

	public bool IsSynchronized => false;

	public int Count => _count;

	public object SyncRoot => this;

	public bool IsReadOnly => false;

	public IEnumerable Reversed => new ReversedTree(_tree);

	public IComparer Comparer => _comparer;

	public SetBase(IComparer comparer, bool allowDuplicates)
	{
		_comparer = comparer;
		_allowDuplicates = allowDuplicates;
		_tree = new RbTree(_comparer);
	}

	public override bool Equals(object obj)
	{
		return CollectionComparer.Default.Compare(this, obj) == 0;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public IEnumerator GetEnumerator()
	{
		return new SetEnumerator(_tree);
	}

	public void CopyTo(Array array, int index)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (array.Rank != 1)
		{
			throw new ArgumentException("Cannot copy to multidimensional array", "array");
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException("index", index, "index cannot be negative");
		}
		if (index >= array.Length)
		{
			throw new ArgumentOutOfRangeException("index", index, $"Passed array of length {array.Length}, index cannot exceed {array.Length - 1}");
		}
		int count = Count;
		if (array.Length - index < count)
		{
			throw new ArgumentOutOfRangeException("index", index, $"Not enough room in the array to copy the collection. Array length {array.Length}, start index {index}, items in collection {count}");
		}
		int num = index;
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				array.SetValue(current, num);
				num++;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	public bool Add(object key)
	{
		if (key == null)
		{
			return false;
		}
		RbTree.InsertResult insertResult = _tree.Insert(key, _allowDuplicates, replaceIfDuplicate: true);
		if (insertResult.NewNode)
		{
			_count++;
		}
		return insertResult.NewNode;
	}

	public bool AddIfNotContains(object key)
	{
		if (key == null)
		{
			return false;
		}
		RbTree.InsertResult insertResult = _tree.Insert(key, allowDuplicates: false, replaceIfDuplicate: false);
		if (insertResult.NewNode)
		{
			_count++;
		}
		return insertResult.NewNode;
	}

	public int Remove(object key)
	{
		if (key == null)
		{
			return 0;
		}
		int num = _tree.Erase(key);
		_count -= num;
		return num;
	}

	public void Clear()
	{
		_tree = new RbTree(_comparer);
		_count = 0;
	}

	public object Find(object key)
	{
		if (key == null)
		{
			return null;
		}
		RbTreeNode rbTreeNode = _tree.LowerBound(key);
		if (rbTreeNode.IsNull)
		{
			return null;
		}
		if (Comparer.Compare(rbTreeNode.Value, key) == 0)
		{
			return rbTreeNode.Value;
		}
		return null;
	}

	public ICollection FindAll(object key)
	{
		ArrayList arrayList = new ArrayList();
		if (key == null)
		{
			return arrayList;
		}
		RbTreeNode rbTreeNode = _tree.LowerBound(key);
		RbTreeNode rbTreeNode2 = _tree.UpperBound(key);
		if (rbTreeNode == rbTreeNode2)
		{
			return arrayList;
		}
		for (RbTreeNode rbTreeNode3 = rbTreeNode; rbTreeNode3 != rbTreeNode2; rbTreeNode3 = _tree.Next(rbTreeNode3))
		{
			arrayList.Add(rbTreeNode3.Value);
		}
		return arrayList;
	}

	public int CompareTo(object obj)
	{
		return CollectionComparer.Default.Compare(this, obj);
	}

	protected void CheckComparer(ISet other)
	{
		if (Comparer.GetType() != other.Comparer.GetType())
		{
			throw new ArgumentException("Sets have incompatible comparer objects", "other");
		}
	}
}
