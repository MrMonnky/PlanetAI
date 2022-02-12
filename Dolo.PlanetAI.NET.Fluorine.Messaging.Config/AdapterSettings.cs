using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class AdapterSettings : Hashtable
{
	private string _id;

	private string _class;

	private bool _defaultAdapter;

	public string Id => _id;

	public string Class => _class;

	public bool Default => _defaultAdapter;

	internal AdapterSettings(string id, string adapterClass, bool defaultAdapter)
	{
		_id = id;
		_class = adapterClass;
		_defaultAdapter = defaultAdapter;
	}

	internal AdapterSettings(XmlNode adapterNode)
	{
		_id = adapterNode.Attributes!["id"]!.Value;
		_class = adapterNode.Attributes!["class"]!.Value;
		if (adapterNode.Attributes!["default"] != null && adapterNode.Attributes!["default"]!.Value == "true")
		{
			_defaultAdapter = true;
		}
	}
}
