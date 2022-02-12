using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class PathEntry
{
	private string _path;

	[XmlAttribute(AttributeName = "path")]
	public string Path
	{
		get
		{
			return _path;
		}
		set
		{
			_path = value;
		}
	}
}
