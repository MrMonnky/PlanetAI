using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;
using Dolo.PlanetAI.NET.Fluorine.Remoting;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ServiceConfigSettings
{
	private ChannelSettingsCollection _channelSettingsCollection;

	private FactorySettingsCollection _factorySettingsCollection;

	private ServiceSettingsCollection _serviceSettingsCollection;

	private FlexClientSettings _flexClientSettings;

	public FlexClientSettings FlexClientSettings => _flexClientSettings;

	public ChannelSettingsCollection ChannelsSettings => _channelSettingsCollection;

	public FactorySettingsCollection FactoriesSettings => _factorySettingsCollection;

	public ServiceSettingsCollection ServiceSettings => _serviceSettingsCollection;

	internal ServiceConfigSettings()
	{
		_channelSettingsCollection = new ChannelSettingsCollection();
		_factorySettingsCollection = new FactorySettingsCollection();
		_serviceSettingsCollection = new ServiceSettingsCollection();
	}

	public static ServiceConfigSettings Load()
	{
		ServiceConfigSettings serviceConfigSettings = new ServiceConfigSettings();
		ChannelSettings value = new ChannelSettings("my-amf", "flex.messaging.endpoints.AMFEndpoint", "http://{server.name}:{server.port}/{context.root}/gateway");
		serviceConfigSettings.ChannelsSettings.Add(value);
		ServiceSettings serviceSettings = new ServiceSettings(serviceConfigSettings, "remoting-service", typeof(RemotingService).FullName);
		string text = "flex.messaging.messages.RemotingMessage";
		string type = AMFConfiguration.Instance.ClassMappings.GetType(text);
		serviceSettings.SupportedMessageTypes[text] = type;
		serviceConfigSettings.ServiceSettings.Add(serviceSettings);
		AdapterSettings adapter = (serviceSettings.DefaultAdapter = new AdapterSettings("dotnet", typeof(RemotingAdapter).FullName, defaultAdapter: true));
		DestinationSettings value2 = new DestinationSettings(serviceSettings, "AMF", adapter, "*");
		serviceSettings.DestinationSettings.Add(value2);
		serviceConfigSettings._flexClientSettings = new FlexClientSettings();
		return serviceConfigSettings;
	}
}
