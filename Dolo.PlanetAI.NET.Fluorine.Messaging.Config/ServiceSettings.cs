using System.Collections;
using System.Xml;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Remoting;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ServiceSettings
{
	private Hashtable _supportedMessageTypes;

	private DestinationSettingsCollection _destinationSettings;

	private AdapterSettingsCollection _adapterSettings;

	private AdapterSettings _defaultAdapterSettings;

	private ServiceConfigSettings _serviceConfigSettings;

	private string _id;

	private string _class;

	private object _objLock = new object();

	public string Id => _id;

	public string Class => _class;

	public Hashtable SupportedMessageTypes => _supportedMessageTypes;

	public DestinationSettingsCollection DestinationSettings => _destinationSettings;

	public AdapterSettingsCollection AdapterSettings => _adapterSettings;

	public AdapterSettings DefaultAdapter
	{
		get
		{
			return _defaultAdapterSettings;
		}
		set
		{
			_defaultAdapterSettings = value;
		}
	}

	public ServiceConfigSettings ServiceConfigSettings => _serviceConfigSettings;

	internal ServiceSettings(ServiceConfigSettings serviceConfigSettings)
	{
		_serviceConfigSettings = serviceConfigSettings;
		_supportedMessageTypes = new Hashtable(1);
		_destinationSettings = new DestinationSettingsCollection();
		_adapterSettings = new AdapterSettingsCollection();
	}

	internal ServiceSettings(ServiceConfigSettings serviceConfigSettings, string id, string @class)
	{
		_serviceConfigSettings = serviceConfigSettings;
		_supportedMessageTypes = new Hashtable(1);
		_destinationSettings = new DestinationSettingsCollection();
		_adapterSettings = new AdapterSettingsCollection();
		_id = id;
		_class = @class;
	}

	internal void Init(string configPath)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(configPath);
		XmlElement documentElement = xmlDocument.DocumentElement;
		Init(documentElement);
	}

	internal void Init(XmlNode serviceElement)
	{
		_id = serviceElement.Attributes!["id"]!.Value;
		_class = serviceElement.Attributes!["class"]!.Value;
		string value = serviceElement.Attributes!["messageTypes"]!.Value;
		string[] array = value.Split(new char[1] { ',' });
		string[] array2 = array;
		foreach (string text in array2)
		{
			string type = AMFConfiguration.Instance.ClassMappings.GetType(text);
			_supportedMessageTypes[text] = type;
		}
		XmlNode xmlNode = serviceElement.SelectSingleNode("adapters");
		if (xmlNode != null)
		{
			foreach (XmlNode item in xmlNode.SelectNodes("*")!)
			{
				AdapterSettings adapterSettings = new AdapterSettings(item);
				_adapterSettings.Add(adapterSettings);
				if (adapterSettings.Default)
				{
					_defaultAdapterSettings = adapterSettings;
				}
			}
		}
		else
		{
			AdapterSettings value2 = (_defaultAdapterSettings = new AdapterSettings("dotnet", typeof(RemotingAdapter).FullName, defaultAdapter: true));
			_adapterSettings.Add(value2);
		}
		XmlNodeList xmlNodeList = serviceElement.SelectNodes("destination");
		foreach (XmlNode item2 in xmlNodeList)
		{
			DestinationSettings value3 = new DestinationSettings(this, item2);
			DestinationSettings.Add(value3);
		}
	}

	internal DestinationSettings CreateDestinationSettings(string id, string source)
	{
		lock (_objLock)
		{
			if (!DestinationSettings.ContainsKey(id))
			{
				AdapterSettings adapter = new AdapterSettings("dotnet", typeof(RemotingAdapter).FullName, defaultAdapter: false);
				DestinationSettings destinationSettings = new DestinationSettings(this, id, adapter, source);
				DestinationSettings.Add(destinationSettings);
				return destinationSettings;
			}
			return DestinationSettings[id];
		}
	}
}
