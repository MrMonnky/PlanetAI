using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal class SchedulingServiceSettings
{
	private string _type;

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
}
