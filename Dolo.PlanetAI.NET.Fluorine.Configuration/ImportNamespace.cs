using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class ImportNamespace
{
	private string _namespace;

	private string _assembly;

	[XmlAttribute(DataType = "string", AttributeName = "namespace")]
	public string Namespace
	{
		get
		{
			return _namespace;
		}
		set
		{
			_namespace = value;
		}
	}

	[XmlAttribute(DataType = "string", AttributeName = "assembly")]
	public string Assembly
	{
		get
		{
			return _assembly;
		}
		set
		{
			_assembly = value;
		}
	}
}
