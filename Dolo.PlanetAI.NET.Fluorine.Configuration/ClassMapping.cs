using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

[XmlType(TypeName = "classMapping")]
internal sealed class ClassMapping
{
	private string _type;

	private string _customClass;

	[XmlElement(DataType = "string", ElementName = "type")]
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

	[XmlElement(DataType = "string", ElementName = "customClass")]
	public string CustomClass
	{
		get
		{
			return _customClass;
		}
		set
		{
			_customClass = value;
		}
	}
}
