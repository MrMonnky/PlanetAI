using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ChannelSettingsCollection : CollectionBase
{
	private Hashtable _channelDictionary;

	public ChannelSettings this[int index]
	{
		get
		{
			return base.List[index] as ChannelSettings;
		}
		set
		{
			base.List[index] = value;
		}
	}

	public ChannelSettings this[string key]
	{
		get
		{
			return _channelDictionary[key] as ChannelSettings;
		}
		set
		{
			_channelDictionary[key] = value;
		}
	}

	public ChannelSettingsCollection()
	{
		_channelDictionary = new Hashtable();
	}

	public int Add(ChannelSettings value)
	{
		_channelDictionary[value.Id] = value;
		return base.List.Add(value);
	}

	public int IndexOf(ChannelSettings value)
	{
		return base.List.IndexOf(value);
	}

	public void Insert(int index, ChannelSettings value)
	{
		_channelDictionary[value.Id] = value;
		base.List.Insert(index, value);
	}

	public void Remove(ChannelSettings value)
	{
		_channelDictionary.Remove(value.Id);
		base.List.Remove(value);
	}

	public bool Contains(ChannelSettings value)
	{
		return base.List.Contains(value);
	}
}
