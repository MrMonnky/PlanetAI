using System;
using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

[Serializable]
internal class TreeSet : ArrayList, ISortedSet, ICollection, IEnumerable, IList
{
	private readonly IComparer _comparer = System.Collections.Comparer.Default;

	public IComparer Comparer => _comparer;

	public TreeSet()
	{
	}

	public TreeSet(ICollection c)
	{
		AddAll(c);
	}

	public TreeSet(IComparer c)
	{
		_comparer = c;
	}

	public static TreeSet UnmodifiableTreeSet(ICollection collection)
	{
		ArrayList list = new ArrayList(collection);
		list = ArrayList.ReadOnly(list);
		return new TreeSet(list);
	}

	private bool AddWithoutSorting(object obj)
	{
		bool flag;
		if (!(flag = Contains(obj)))
		{
			base.Add(obj);
		}
		return !flag;
	}

	public new bool Add(object obj)
	{
		bool result = AddWithoutSorting(obj);
		Sort(_comparer);
		return result;
	}

	public bool AddAll(ICollection c)
	{
		IEnumerator enumerator = new ArrayList(c).GetEnumerator();
		bool result = false;
		while (enumerator.MoveNext())
		{
			if (AddWithoutSorting(enumerator.Current))
			{
				result = true;
			}
		}
		Sort(_comparer);
		return result;
	}

	public object First()
	{
		return this[0];
	}

	public override bool Contains(object item)
	{
		IEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (_comparer.Compare(enumerator.Current, item) == 0)
			{
				return true;
			}
		}
		return false;
	}

	public ISortedSet TailSet(object limit)
	{
		ISortedSet sortedSet = new TreeSet();
		int i;
		for (i = 0; i < Count && _comparer.Compare(this[i], limit) < 0; i++)
		{
		}
		for (; i < Count; i++)
		{
			sortedSet.Add(this[i]);
		}
		return sortedSet;
	}
}
