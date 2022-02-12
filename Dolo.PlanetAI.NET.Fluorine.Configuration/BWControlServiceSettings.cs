using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal class BWControlServiceSettings
{
	private string _type;

	private int _interval;

	private int _defaultCapacity;

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

	[XmlAttribute(DataType = "int", AttributeName = "interval")]
	public int Interval
	{
		get
		{
			return _interval;
		}
		set
		{
			_interval = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "defaultCapacity")]
	public int DefaultCapacity
	{
		get
		{
			return _defaultCapacity;
		}
		set
		{
			_defaultCapacity = value;
		}
	}

	public BWControlServiceSettings()
	{
		_interval = 100;
		_defaultCapacity = 104857600;
	}
}
