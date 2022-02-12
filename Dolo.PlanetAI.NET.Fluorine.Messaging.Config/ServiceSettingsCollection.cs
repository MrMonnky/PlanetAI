using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ServiceSettingsCollection : CollectionBase
{
	private Hashtable _serviceDictionary;

	public ServiceSettings this[int index]
	{
		get
		{
			return base.List[index] as ServiceSettings;
		}
		set
		{
			base.List[index] = value;
		}
	}

	public ServiceSettings this[string key]
	{
		get
		{
			return _serviceDictionary[key] as ServiceSettings;
		}
		set
		{
			_serviceDictionary[key] = value;
		}
	}

	public ServiceSettingsCollection()
	{
		_serviceDictionary = new Hashtable();
	}

	public int Add(ServiceSettings value)
	{
		_serviceDictionary[value.Id] = value;
		return base.List.Add(value);
	}

	public int IndexOf(ServiceSettings value)
	{
		return base.List.IndexOf(value);
	}

	public void Insert(int index, ServiceSettings value)
	{
		_serviceDictionary[value.Id] = value;
		base.List.Insert(index, value);
	}

	public void Remove(ServiceSettings value)
	{
		_serviceDictionary.Remove(value.Id);
		base.List.Remove(value);
	}

	public bool Contains(ServiceSettings value)
	{
		return base.List.Contains(value);
	}
}
