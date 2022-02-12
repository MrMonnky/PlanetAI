using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal enum TimezoneCompensation
{
	[XmlEnum(Name = "none")]
	None,
	[XmlEnum(Name = "auto")]
	Auto
}
