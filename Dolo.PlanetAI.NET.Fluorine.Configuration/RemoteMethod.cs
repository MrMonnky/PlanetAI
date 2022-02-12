using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RemoteMethod
{
	private string _name;

	private string _method;

	[XmlElement(DataType = "string", ElementName = "name")]
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	[XmlElement(DataType = "string", ElementName = "method")]
	public string Method
	{
		get
		{
			return _method;
		}
		set
		{
			_method = value;
		}
	}
}
