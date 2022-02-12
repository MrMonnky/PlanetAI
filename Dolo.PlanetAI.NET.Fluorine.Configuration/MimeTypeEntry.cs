using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class MimeTypeEntry
{
	private string _type;

	[XmlAttribute(AttributeName = "type")]
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
