using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class MsmqSettings : Hashtable
{
	public const string BinaryMessageFormatter = "BinaryMessageFormatter";

	public const string XmlMessageFormatter = "XmlMessageFormatter";

	public string Name
	{
		get
		{
			if (ContainsKey("name"))
			{
				return this["name"] as string;
			}
			return null;
		}
	}

	public string Formatter
	{
		get
		{
			if (ContainsKey("formatter"))
			{
				return this["formatter"] as string;
			}
			return null;
		}
	}

	public string Label
	{
		get
		{
			if (ContainsKey("label"))
			{
				return this["label"] as string;
			}
			return null;
		}
	}

	private MsmqSettings()
	{
	}

	internal MsmqSettings(XmlNode msmqDefinitionNode)
	{
		foreach (XmlNode item in msmqDefinitionNode.SelectNodes("*")!)
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
