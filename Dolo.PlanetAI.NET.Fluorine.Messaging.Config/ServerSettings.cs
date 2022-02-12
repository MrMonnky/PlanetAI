using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ServerSettings : Hashtable
{
	public bool AllowSubtopics
	{
		get
		{
			if (Contains("allow-subtopics"))
			{
				return (bool)this["allow-subtopics"];
			}
			return false;
		}
	}

	internal ServerSettings()
	{
	}

	internal ServerSettings(XmlNode severDefinitionNode)
	{
		foreach (XmlNode item in severDefinitionNode.SelectNodes("*")!)
		{
			if (item.InnerXml != null && item.InnerXml != string.Empty)
			{
				this[item.Name] = item.InnerXml;
			}
			else
			{
				if (item.Attributes == null)
				{
					continue;
				}
				foreach (XmlAttribute item2 in item.Attributes!)
				{
					this[item.Name + "_" + item2.Name] = item2.Value;
				}
			}
		}
	}
}
