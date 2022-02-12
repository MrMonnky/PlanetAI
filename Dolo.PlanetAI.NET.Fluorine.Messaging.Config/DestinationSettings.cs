using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class DestinationSettings : Hashtable
{
	public const string AMFDestination = "AMF";

	public const string AMFServiceBrowserDestination = "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.AMFServiceBrowser";

	public const string AMFManagementDestination = "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.ManagementService";

	public const string AMFCodeGeneratorDestination = "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.CodeGeneratorService";

	private ServiceSettings _serviceSettings;

	private string _id;

	private AdapterSettings _adapter;

	private Hashtable _properties;

	private NetworkSettings _network;

	private MetadataSettings _metadata;

	private MsmqSettings _msmqSettings;

	private ServerSettings _server;

	private ChannelSettingsCollection _channels;

	private XmlNode _propertiesNode;

	public XmlNode PropertiesNode => _propertiesNode;

	public string Id => _id;

	public ServiceSettings ServiceSettings => _serviceSettings;

	public AdapterSettings Adapter => _adapter;

	public Hashtable Properties => _properties;

	public NetworkSettings NetworkSettings => _network;

	public MsmqSettings MsmqSettings => _msmqSettings;

	public MetadataSettings MetadataSettings => _metadata;

	public ServerSettings ServerSettings => _server;

	public ChannelSettingsCollection Channels => _channels;

	internal DestinationSettings(ServiceSettings serviceSettings, string id, AdapterSettings adapter, string source)
	{
		_serviceSettings = serviceSettings;
		_properties = new Hashtable();
		_channels = new ChannelSettingsCollection();
		_id = id;
		_adapter = adapter;
		_properties["source"] = source;
	}

	internal DestinationSettings(ServiceSettings serviceSettings, XmlNode destinationNode)
	{
		_serviceSettings = serviceSettings;
		_properties = new Hashtable();
		_channels = new ChannelSettingsCollection();
		_id = destinationNode.Attributes!["id"]!.Value;
		XmlNode xmlNode = destinationNode.SelectSingleNode("adapter");
		if (xmlNode != null)
		{
			string value = xmlNode.Attributes!["ref"]!.Value;
			AdapterSettings adapterSettings = (_adapter = serviceSettings.AdapterSettings[value]);
		}
		_propertiesNode = destinationNode.SelectSingleNode("properties");
		if (_propertiesNode != null)
		{
			XmlNode xmlNode2 = _propertiesNode.SelectSingleNode("source");
			if (xmlNode2 != null)
			{
				_properties["source"] = xmlNode2.InnerXml;
			}
			XmlNode xmlNode3 = _propertiesNode.SelectSingleNode("factory");
			if (xmlNode3 != null)
			{
				_properties["factory"] = xmlNode3.InnerXml;
			}
			XmlNode xmlNode4 = _propertiesNode.SelectSingleNode("attribute-id");
			if (xmlNode4 != null)
			{
				_properties["attribute-id"] = xmlNode4.InnerXml;
			}
			else
			{
				_properties["attribute-id"] = _id;
			}
			XmlNode xmlNode5 = _propertiesNode.SelectSingleNode("scope");
			if (xmlNode5 != null)
			{
				_properties["scope"] = xmlNode5.InnerXml;
			}
			XmlNode xmlNode6 = _propertiesNode.SelectSingleNode("network");
			if (xmlNode6 != null)
			{
				NetworkSettings networkSettings = (_network = new NetworkSettings(xmlNode6));
			}
			XmlNode xmlNode7 = _propertiesNode.SelectSingleNode("metadata");
			if (xmlNode7 != null)
			{
				MetadataSettings metadataSettings = (_metadata = new MetadataSettings(xmlNode7));
			}
			XmlNode xmlNode8 = _propertiesNode.SelectSingleNode("server");
			if (xmlNode8 != null)
			{
				ServerSettings serverSettings = (_server = new ServerSettings(xmlNode8));
			}
			XmlNode xmlNode9 = _propertiesNode.SelectSingleNode("msmq");
			if (xmlNode9 != null)
			{
				MsmqSettings msmqSettings = (_msmqSettings = new MsmqSettings(xmlNode9));
			}
		}
		XmlNode xmlNode10 = destinationNode.SelectSingleNode("channels");
		if (xmlNode10 == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = xmlNode10.SelectNodes("channel");
		foreach (XmlNode item in xmlNodeList)
		{
			string value2 = item.Attributes!["ref"]!.Value;
			if (value2 != null)
			{
				ChannelSettings value3 = _serviceSettings.ServiceConfigSettings.ChannelsSettings[value2];
				_channels.Add(value3);
			}
			else
			{
				ChannelSettings value4 = new ChannelSettings(item);
				_channels.Add(value4);
			}
		}
	}
}
