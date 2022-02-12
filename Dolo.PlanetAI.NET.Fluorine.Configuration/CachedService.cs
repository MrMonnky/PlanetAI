using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

[XmlType(TypeName = "cachedService")]
internal sealed class CachedService
{
	private int _timeout;

	private bool _slidingExpiration;

	private string _type;

	[XmlAttribute(DataType = "int", AttributeName = "timeout")]
	public int Timeout
	{
		get
		{
			return _timeout;
		}
		set
		{
			_timeout = value;
		}
	}

	[XmlAttribute(DataType = "boolean", AttributeName = "slidingExpiration")]
	public bool SlidingExpiration
	{
		get
		{
			return _slidingExpiration;
		}
		set
		{
			_slidingExpiration = value;
		}
	}

	[XmlAttribute(DataType = "string", AttributeName = "type")]
	public string Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
		}
	}

	public CachedService()
	{
		_timeout = 30;
		_slidingExpiration = false;
	}
}
