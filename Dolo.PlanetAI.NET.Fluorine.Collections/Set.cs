using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Collections;

internal class Set : SetBase
{
	public Set()
		: base(System.Collections.Comparer.Default, allowDuplicates: false)
	{
	}

	public Set(ICollection collection)
		: this()
	{
		foreach (object item in collection)
		{
			Add(item);
		}
	}

	public Set(IComparer comparer)
		: base(comparer, allowDuplicates: false)
	{
	}

	public Set(IComparer comparer, ICollection collection)
		: this(comparer)
	{
		foreach (object item in collection)
		{
			Add(item);
		}
	}

	public Set Union(Set other)
	{
		return Union(this, other);
	}

	public static Set Union(Set a, Set b)
	{
		a.CheckComparer(b);
		Set set = new Set(a.Comparer);
		SetOp.Union(a, b, a.Comparer, new Inserter(set));
		return set;
	}

	public Set Intersection(Set other)
	{
		return Intersection(this, other);
	}

	public static Set Intersection(Set a, Set b)
	{
		a.CheckComparer(b);
		Set set = new Set(a.Comparer);
		SetOp.Inersection(a, b, a.Comparer, new Inserter(set));
		return set;
	}

	public Set Difference(Set other)
	{
		return Difference(this, other);
	}

	public static Set Difference(Set a, Set b)
	{
		a.CheckComparer(b);
		Set set = new Set(a.Comparer);
		SetOp.Difference(a, b, a.Comparer, new Inserter(set));
		return set;
	}

	public Set SymmetricDifference(Set other)
	{
		return SymmetricDifference(this, other);
	}

	public static Set SymmetricDifference(Set a, Set b)
	{
		a.CheckComparer(b);
		Set set = new Set(a.Comparer);
		SetOp.SymmetricDifference(a, b, a.Comparer, new Inserter(set));
		return set;
	}
}
