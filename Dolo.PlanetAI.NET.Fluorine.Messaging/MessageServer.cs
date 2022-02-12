using System;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.DependencyInjection;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal sealed class MessageServer : DisposableBase
{
	private ServiceConfigSettings _serviceConfigSettings;

	private MessageBroker _messageBroker;

	internal ServiceConfigSettings ServiceConfigSettings => _serviceConfigSettings;

	public MessageBroker MessageBroker => _messageBroker;

	public void Init()
	{
		_messageBroker = new MessageBroker(this);
		_serviceConfigSettings = ServiceConfigSettings.Load();
		foreach (ChannelSettings channelsSetting in _serviceConfigSettings.ChannelsSettings)
		{
			Type type = ObjectFactory.Locate(channelsSetting.Class);
			if (type != null && ObjectFactory.CreateInstance(type, new object[2] { _messageBroker, channelsSetting }) is IEndpoint endpoint)
			{
				_messageBroker.AddEndpoint(endpoint);
			}
		}
		foreach (FactorySettings factoriesSetting in _serviceConfigSettings.FactoriesSettings)
		{
			Type type2 = ObjectFactory.Locate(factoriesSetting.ClassId);
			if (type2 != null && ObjectFactory.CreateInstance(type2, new object[0]) is IFlexFactory factory)
			{
				_messageBroker.AddFactory(factoriesSetting.Id, factory);
			}
		}
		_messageBroker.AddFactory("dotnet", new DotNetFactory());
		foreach (ServiceSettings serviceSetting in _serviceConfigSettings.ServiceSettings)
		{
			Type type3 = ObjectFactory.Locate(serviceSetting.Class);
			if (type3 != null && ObjectFactory.CreateInstance(type3, new object[2] { _messageBroker, serviceSetting }) is IService service)
			{
				_messageBroker.AddService(service);
			}
		}
	}

	private void InstallServiceBrowserDestinations(ServiceSettings serviceSettings, AdapterSettings adapterSettings)
	{
		DestinationSettings value = new DestinationSettings(serviceSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.AMFServiceBrowser", adapterSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.AMFServiceBrowser");
		serviceSettings.DestinationSettings.Add(value);
		value = new DestinationSettings(serviceSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.ManagementService", adapterSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.ManagementService");
		serviceSettings.DestinationSettings.Add(value);
		value = new DestinationSettings(serviceSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.CodeGeneratorService", adapterSettings, "Dolo.PlanetAI.NET.Fluorine.ServiceBrowser.CodeGeneratorService");
		serviceSettings.DestinationSettings.Add(value);
	}

	public void Start()
	{
		if (_messageBroker != null)
		{
			_messageBroker.Start();
		}
	}

	public void Stop()
	{
		if (_messageBroker != null)
		{
			_messageBroker.Stop();
			_messageBroker = null;
		}
	}

	protected override void Free()
	{
		if (_messageBroker != null)
		{
			Stop();
		}
	}

	protected override void FreeUnmanaged()
	{
		if (_messageBroker != null)
		{
			Stop();
		}
	}

	public async Task Service()
	{
		if (_messageBroker == null)
		{
			string msg2 = __Res.GetString("MessageBroker_NotAvailable");
			throw new AMFException(msg2);
		}
		string contextPath = HttpContextManager.ContextPath;
		string endpointPath = contextPath + "/Gateway";
		bool isSecure = HttpContextManager.IsSecure;
		IEndpoint endpoint = _messageBroker.GetEndpoint(endpointPath, contextPath, isSecure);
		if (endpoint != null)
		{
			await endpoint.Service();
			return;
		}
		string msg = __Res.GetString("Endpoint_BindFail", endpointPath, contextPath);
		_messageBroker.TraceChannelSettings();
		throw new AMFException(msg);
	}
}
