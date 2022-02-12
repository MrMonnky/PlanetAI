using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class AdapterSettingsCollection : CollectionBase
{
	private Hashtable _adapterDictionary;

	public AdapterSettings this[int index]
	{
		get
		{
			return base.List[index] as AdapterSettings;
		}
		set
		{
			base.List[index] = value;
		}
	}

	public AdapterSettings this[string key]
	{
		get
		{
			return _adapterDictionary[key] as AdapterSettings;
		}
		set
		{
			_adapterDictionary[key] = value;
		}
	}

	public AdapterSettingsCollection()
	{
		_adapterDictionary = new Hashtable();
	}

	public int Add(AdapterSettings value)
	{
		_adapterDictionary[value.Id] = value;
		return base.List.Add(value);
	}

	public int IndexOf(AdapterSettings value)
	{
		return base.List.IndexOf(value);
	}

	public void Insert(int index, AdapterSettings value)
	{
		_adapterDictionary[value.Id] = value;
		base.List.Insert(index, value);
	}

	public void Remove(AdapterSettings value)
	{
		_adapterDictionary.Remove(value.Id);
		base.List.Remove(value);
	}

	public bool Contains(AdapterSettings value)
	{
		return base.List.Contains(value);
	}
}
