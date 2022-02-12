using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class DestinationSettingsCollection : CollectionBase
{
	private Hashtable _destinationDictionary;

	public DestinationSettings this[int index]
	{
		get
		{
			return base.List[index] as DestinationSettings;
		}
		set
		{
			base.List[index] = value;
		}
	}

	public DestinationSettings this[string key]
	{
		get
		{
			return _destinationDictionary[key] as DestinationSettings;
		}
		set
		{
			_destinationDictionary[key] = value;
		}
	}

	public DestinationSettingsCollection()
	{
		_destinationDictionary = new Hashtable();
	}

	public int Add(DestinationSettings value)
	{
		_destinationDictionary[value.Id] = value;
		return base.List.Add(value);
	}

	public int IndexOf(DestinationSettings value)
	{
		return base.List.IndexOf(value);
	}

	public void Insert(int index, DestinationSettings value)
	{
		_destinationDictionary[value.Id] = value;
		base.List.Insert(index, value);
	}

	public void Remove(DestinationSettings value)
	{
		_destinationDictionary.Remove(value.Id);
		base.List.Remove(value);
	}

	public bool Contains(DestinationSettings value)
	{
		return base.List.Contains(value);
	}

	public bool ContainsKey(string key)
	{
		return _destinationDictionary.ContainsKey(key);
	}
}
