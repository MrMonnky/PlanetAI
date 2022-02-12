using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal enum RemotingServiceAttributeConstraint
{
	[XmlEnum(Name = "browse")]
	Browse = 1,
	[XmlEnum(Name = "access")]
	Access
}
