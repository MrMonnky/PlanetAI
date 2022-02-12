using System;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class MimeTypeEntryCollection : CollectionBase<MimeTypeEntry>
{
	private Dictionary<string, MimeTypeEntry> _excludedTypes = new Dictionary<string, MimeTypeEntry>(StringComparer.OrdinalIgnoreCase);

	public override void Add(MimeTypeEntry value)
	{
		_excludedTypes.Add(value.Type, value);
		base.Add(value);
	}

	public override void Insert(int index, MimeTypeEntry value)
	{
		_excludedTypes.Add(value.Type, value);
		base.Insert(index, value);
	}

	public override bool Remove(MimeTypeEntry value)
	{
		_excludedTypes.Remove(value.Type);
		return base.Remove(value);
	}

	public bool Contains(string type)
	{
		return _excludedTypes.ContainsKey(type);
	}
}
