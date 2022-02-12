using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class Destination
{
	protected IService _service;

	protected DestinationSettings _settings;

	protected ServiceAdapter _adapter;

	private FactoryInstance _factoryInstance;

	public string Id => _settings.Id;

	public string FactoryId
	{
		get
		{
			if (_settings.Properties.Contains("factory"))
			{
				return _settings.Properties["factory"] as string;
			}
			return "dotnet";
		}
	}

	public IService Service => _service;

	public ServiceAdapter ServiceAdapter => _adapter;

	public DestinationSettings DestinationSettings => _settings;

	public string Source
	{
		get
		{
			if (_settings != null && _settings.Properties != null)
			{
				return _settings.Properties["source"] as string;
			}
			return null;
		}
	}

	public string Scope
	{
		get
		{
			if (_settings != null && _settings.Properties != null)
			{
				return _settings.Properties["scope"] as string;
			}
			return "request";
		}
	}

	private Destination()
	{
	}

	internal Destination(IService service, DestinationSettings settings)
	{
		_service = service;
		_settings = settings;
	}

	internal void Init(AdapterSettings adapterSettings)
	{
		if (adapterSettings != null)
		{
			string @class = adapterSettings.Class;
			Type type = ObjectFactory.Locate(@class);
			if (type != null)
			{
				_adapter = ObjectFactory.CreateInstance(type) as ServiceAdapter;
				_adapter.SetDestination(this);
				_adapter.SetAdapterSettings(adapterSettings);
				_adapter.SetDestinationSettings(_settings);
				_adapter.Init();
			}
		}
		MessageBroker messageBroker = Service.GetMessageBroker();
		messageBroker.RegisterDestination(this, _service);
		if (Scope == "application")
		{
			FactoryInstance factoryInstance = GetFactoryInstance();
			object obj = factoryInstance.Lookup();
		}
	}

	public FactoryInstance GetFactoryInstance()
	{
		if (_factoryInstance != null)
		{
			return _factoryInstance;
		}
		MessageBroker messageBroker = Service.GetMessageBroker();
		IFlexFactory factory = messageBroker.GetFactory(FactoryId);
		Hashtable properties = _settings.Properties;
		_factoryInstance = factory.CreateFactoryInstance(Id, properties);
		return _factoryInstance;
	}
}
