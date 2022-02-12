using System;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class NullableTypeCollection : CollectionBase<NullableType>
{
	private Dictionary<string, NullableType> _nullableDictionary = new Dictionary<string, NullableType>(5);

	public object this[Type type]
	{
		get
		{
			if (_nullableDictionary.ContainsKey(type.FullName))
			{
				return _nullableDictionary[type.FullName].NullValue;
			}
			return null;
		}
	}

	public override void Add(NullableType value)
	{
		_nullableDictionary[value.TypeName] = value;
		base.Add(value);
	}

	public override void Insert(int index, NullableType value)
	{
		_nullableDictionary[value.TypeName] = value;
		base.Insert(index, value);
	}

	public override bool Remove(NullableType value)
	{
		_nullableDictionary.Remove(value.TypeName);
		return base.Remove(value);
	}

	public bool ContainsKey(Type type)
	{
		return _nullableDictionary.ContainsKey(type.FullName);
	}
}
