using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RtmpServerSettings
{
	private ThreadPoolSettings _threadPoolSettings;

	private RtmpConnectionSettings _rtmpConnectionSettings;

	private RtmptConnectionSettings _rtmptConnectionSettings;

	private RtmpTransportSettings _rtmpTransportSettings;

	[XmlElement(ElementName = "threadpool")]
	public ThreadPoolSettings ThreadPoolSettings
	{
		get
		{
			return _threadPoolSettings;
		}
		set
		{
			_threadPoolSettings = value;
		}
	}

	[XmlElement(ElementName = "rtmpConnection")]
	public RtmpConnectionSettings RtmpConnectionSettings
	{
		get
		{
			return _rtmpConnectionSettings;
		}
		set
		{
			_rtmpConnectionSettings = value;
		}
	}

	[XmlElement(ElementName = "rtmptConnection")]
	public RtmptConnectionSettings RtmptConnectionSettings
	{
		get
		{
			return _rtmptConnectionSettings;
		}
		set
		{
			_rtmptConnectionSettings = value;
		}
	}

	[XmlElement(ElementName = "rtmpTransport")]
	public RtmpTransportSettings RtmpTransportSettings
	{
		get
		{
			return _rtmpTransportSettings;
		}
		set
		{
			_rtmpTransportSettings = value;
		}
	}

	public RtmpServerSettings()
	{
		_threadPoolSettings = new ThreadPoolSettings();
		_rtmpConnectionSettings = new RtmpConnectionSettings();
		_rtmptConnectionSettings = new RtmptConnectionSettings();
		_rtmpTransportSettings = new RtmpTransportSettings();
	}
}
