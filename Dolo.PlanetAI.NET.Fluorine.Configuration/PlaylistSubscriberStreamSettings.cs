using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class PlaylistSubscriberStreamSettings
{
	private int _underrunTrigger;

	private int _bufferCheckInterval;

	[XmlAttribute(DataType = "int", AttributeName = "underrunTrigger")]
	public int UnderrunTrigger
	{
		get
		{
			return _underrunTrigger;
		}
		set
		{
			_underrunTrigger = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "bufferCheckInterval")]
	public int BufferCheckInterval
	{
		get
		{
			return _bufferCheckInterval;
		}
		set
		{
			_bufferCheckInterval = value;
		}
	}

	public PlaylistSubscriberStreamSettings()
	{
		_underrunTrigger = 5000;
		_bufferCheckInterval = 60000;
	}
}
