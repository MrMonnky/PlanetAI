using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class MetadataSettings : Hashtable
{
	private ArrayList _identity = new ArrayList();

	private ArrayList _associations = new ArrayList();

	public ArrayList Identity => _identity;

	private MetadataSettings()
	{
	}

	internal MetadataSettings(XmlNode metadataDefinitionNode)
	{
		foreach (XmlNode item in metadataDefinitionNode.SelectNodes("*")!)
		{
			if (item.InnerXml != null && item.InnerXml != string.Empty)
			{
				this[item.Name] = item.InnerXml;
				continue;
			}
			if (item.Name == "identity")
			{
				foreach (XmlAttribute item2 in item.Attributes!)
				{
					_identity.Add(item2.Value);
				}
			}
			if (!(item.Name == "many-to-one"))
			{
				continue;
			}
			Hashtable hashtable = new Hashtable(3);
			foreach (XmlAttribute item3 in item.Attributes!)
			{
				hashtable[item3.Name] = item3.Value;
			}
			_associations.Add(hashtable);
		}
	}
}
