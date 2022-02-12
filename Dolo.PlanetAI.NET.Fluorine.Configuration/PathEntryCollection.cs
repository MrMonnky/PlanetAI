using System;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class PathEntryCollection : CollectionBase<PathEntry>
{
	private Dictionary<string, PathEntry> _excludedPaths = new Dictionary<string, PathEntry>(StringComparer.OrdinalIgnoreCase);

	public override void Add(PathEntry value)
	{
		_excludedPaths.Add(value.Path, value);
		base.Add(value);
	}

	public override void Insert(int index, PathEntry value)
	{
		_excludedPaths.Add(value.Path, value);
		base.Insert(index, value);
	}

	public override bool Remove(PathEntry value)
	{
		_excludedPaths.Remove(value.Path);
		return base.Remove(value);
	}

	public bool Contains(string path)
	{
		return _excludedPaths.ContainsKey(path);
	}
}
