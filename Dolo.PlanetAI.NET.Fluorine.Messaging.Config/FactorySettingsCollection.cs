using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class FactorySettingsCollection : CollectionBase
{
	public FactorySettings this[int index]
	{
		get
		{
			return base.List[index] as FactorySettings;
		}
		set
		{
			base.List[index] = value;
		}
	}

	public int Add(FactorySettings value)
	{
		return base.List.Add(value);
	}

	public int IndexOf(FactorySettings value)
	{
		return base.List.IndexOf(value);
	}

	public void Insert(int index, FactorySettings value)
	{
		base.List.Insert(index, value);
	}

	public void Remove(FactorySettings value)
	{
		base.List.Remove(value);
	}

	public bool Contains(FactorySettings value)
	{
		return base.List.Contains(value);
	}
}
