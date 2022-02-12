using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RtmptConnectionSettings
{
	private int _pingInterval;

	private int _maxInactivity;

	private int _maxHandshakeTimeout;

	[XmlAttribute(DataType = "int", AttributeName = "pingInterval")]
	public int PingInterval
	{
		get
		{
			return _pingInterval;
		}
		set
		{
			_pingInterval = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "maxInactivity")]
	public int MaxInactivity
	{
		get
		{
			return _maxInactivity;
		}
		set
		{
			_maxInactivity = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "maxHandshakeTimeout")]
	public int MaxHandshakeTimeout
	{
		get
		{
			return _maxHandshakeTimeout;
		}
		set
		{
			_maxHandshakeTimeout = value;
		}
	}

	public RtmptConnectionSettings()
	{
		_pingInterval = 5000;
		_maxInactivity = 60000;
		_maxHandshakeTimeout = 5000;
	}
}
