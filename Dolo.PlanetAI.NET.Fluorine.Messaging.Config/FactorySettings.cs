using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class FactorySettings : Hashtable
{
	private string _id;

	private string _class;

	public string Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	public string ClassId
	{
		get
		{
			return _class;
		}
		set
		{
			_class = value;
		}
	}

	private FactorySettings()
	{
	}

	internal FactorySettings(XmlNode factoryDefinitionNode)
	{
		_id = factoryDefinitionNode.Attributes!["id"]!.Value;
		_class = factoryDefinitionNode.Attributes!["class"]!.Value;
		XmlNode xmlNode = factoryDefinitionNode.SelectSingleNode("properties");
		if (xmlNode == null)
		{
			return;
		}
		foreach (XmlNode item in xmlNode.SelectNodes("*")!)
		{
			this[item.Name] = item.InnerXml;
		}
	}
}
