using System;
using System.Collections;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Context;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class MessageBroker
{
	public static string DefaultMessageBrokerId = "default";

	private static Hashtable _messageBrokers = new Hashtable(1);

	private static object _syncLock = new object();

	private string _messageBrokerId;

	private Hashtable _services;

	private Hashtable _endpoints;

	private Hashtable _destinationServiceMap;

	private Hashtable _destinations;

	private MessageServer _messageServer;

	private Hashtable _factories;

	private ClientManager _clientManager;

	private GlobalScope _globalScope;

	public string Id => _messageBrokerId;

	public object SyncRoot => _syncLock;

	public IGlobalScope GlobalScope => _globalScope;

	internal IClientRegistry ClientRegistry => _clientManager;

	internal FlexClientSettings FlexClientSettings => _messageServer.ServiceConfigSettings.FlexClientSettings;

	internal MessageServer MessageServer => _messageServer;

	public MessageBroker(MessageServer messageServer)
	{
		_messageServer = messageServer;
		_services = new Hashtable();
		_endpoints = new Hashtable();
		_factories = new Hashtable();
		_destinationServiceMap = new Hashtable();
		_destinations = new Hashtable();
		_clientManager = new ClientManager(this);
	}

	protected void RegisterMessageBroker()
	{
		if (_messageBrokerId == null)
		{
			_messageBrokerId = DefaultMessageBrokerId;
		}
		lock (_syncLock)
		{
			if (_messageBrokers.ContainsKey(_messageBrokerId))
			{
				throw new AMFException(__Res.GetString("MessageBroker_RegisterError", _messageBrokerId));
			}
			_messageBrokers[_messageBrokerId] = this;
		}
	}

	protected void UnregisterMessageBroker()
	{
		if (_messageBrokerId == null)
		{
			_messageBrokerId = DefaultMessageBrokerId;
		}
		lock (_syncLock)
		{
			_messageBrokers.Remove(_messageBrokerId);
		}
	}

	internal void RegisterDestination(Destination destination, Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service)
	{
		_destinationServiceMap[destination.Id] = service.id;
		_destinations[destination.Id] = destination;
	}

	public static MessageBroker GetMessageBroker(string messageBrokerId)
	{
		lock (_syncLock)
		{
			if (messageBrokerId == null)
			{
				messageBrokerId = DefaultMessageBrokerId;
			}
			return _messageBrokers[messageBrokerId] as MessageBroker;
		}
	}

	internal void AddService(Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service)
	{
		_services[service.id] = service;
	}

	internal Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService GetService(string id)
	{
		return _services[id] as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
	}

	internal void AddEndpoint(IEndpoint endpoint)
	{
		_endpoints[endpoint.Id] = endpoint;
	}

	internal void AddFactory(string id, IFlexFactory factory)
	{
		_factories.Add(id, factory);
	}

	public IFlexFactory GetFactory(string id)
	{
		return _factories[id] as IFlexFactory;
	}

	internal void StartServices()
	{
		foreach (DictionaryEntry service2 in _services)
		{
			Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = service2.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
			service.Start();
		}
	}

	internal void StopServices()
	{
		foreach (DictionaryEntry service2 in _services)
		{
			Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = service2.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
			service.Stop();
		}
	}

	internal void StartEndpoints()
	{
		foreach (DictionaryEntry endpoint2 in _endpoints)
		{
			IEndpoint endpoint = endpoint2.Value as IEndpoint;
			endpoint.Start();
		}
	}

	internal void StopEndpoints()
	{
		foreach (DictionaryEntry endpoint2 in _endpoints)
		{
			IEndpoint endpoint = endpoint2.Value as IEndpoint;
			endpoint.Stop();
		}
	}

	public void Start()
	{
		RegisterMessageBroker();
		_globalScope = new GlobalScope();
		_globalScope.Name = "default";
		_globalScope.Register();
		StartServices();
		StartEndpoints();
	}

	public void Stop()
	{
		StopServices();
		StopEndpoints();
		if (_globalScope != null)
		{
			_globalScope.Stop();
			_globalScope.Dispose();
			_globalScope = null;
		}
		UnregisterMessageBroker();
	}

	internal IEndpoint GetEndpoint(string endpointId)
	{
		foreach (DictionaryEntry endpoint2 in _endpoints)
		{
			IEndpoint endpoint = endpoint2.Value as IEndpoint;
			if (endpoint.Id == endpointId)
			{
				return endpoint;
			}
		}
		return null;
	}

	internal IEndpoint GetEndpoint(string path, string contextPath, bool secure)
	{
		foreach (DictionaryEntry endpoint2 in _endpoints)
		{
			IEndpoint endpoint = endpoint2.Value as IEndpoint;
			ChannelSettings settings = endpoint.GetSettings();
			if (settings != null && settings.Bind(path, contextPath))
			{
				return endpoint;
			}
		}
		return null;
	}

	internal void TraceChannelSettings()
	{
		foreach (DictionaryEntry endpoint2 in _endpoints)
		{
			IEndpoint endpoint = endpoint2.Value as IEndpoint;
			ChannelSettings settings = endpoint.GetSettings();
		}
	}

	public Task<IMessage> RouteMessage(IMessage message)
	{
		return RouteMessage(message, null);
	}

	internal async Task<IMessage> RouteMessage(IMessage message, IEndpoint endpoint)
	{
		Task<IMessage> responseMessage;
		if (message is CommandMessage commandMessage && (commandMessage.operation == 13 || commandMessage.operation == 5))
		{
			responseMessage = Task.FromResult((IMessage)new AcknowledgeMessage());
			responseMessage.Result.body = true;
		}
		else
		{
			Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = GetService(message);
			object result;
			if (service != null)
			{
				try
				{
					Task<object> task = service.ServiceMessage(message);
					await task;
					result = task.Result;
				}
				catch (Exception ex)
				{
					Exception exception = ex;
					result = ErrorMessage.GetErrorMessage(message, exception);
				}
			}
			else
			{
				string msg = __Res.GetString("Destination_NotFound", message.destination);
				result = ErrorMessage.GetErrorMessage(message, new AMFException(msg));
			}
			if (!(result is IMessage))
			{
				responseMessage = Task.FromResult((IMessage)new AcknowledgeMessage());
				responseMessage.Result.body = result;
			}
			else
			{
				responseMessage = result as Task<IMessage>;
			}
		}
		if (responseMessage.Result is AsyncMessage)
		{
			((AsyncMessage)responseMessage.Result).correlationId = message.messageId;
		}
		responseMessage.Result.destination = message.destination;
		responseMessage.Result.clientId = message.clientId;
		if (Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current != null && Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client != null)
		{
			responseMessage.Result.SetHeader("DSId", Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client.Id);
		}
		return responseMessage.Result;
	}

	internal Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService GetService(IMessage message)
	{
		Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = GetServiceByDestinationId(message.destination);
		if (service == null && message is CommandMessage commandMessage && commandMessage.messageRefType != null)
		{
			foreach (DictionaryEntry service3 in _services)
			{
				Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service2 = service3.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
				if (service2.IsSupportedMessageType(commandMessage.messageRefType))
				{
					service = service2;
					break;
				}
			}
		}
		return service;
	}

	internal Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService GetServiceByDestinationId(string destinationId)
	{
		if (destinationId == null)
		{
			return null;
		}
		if (_destinationServiceMap[destinationId] is string key)
		{
			return _services[key] as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
		}
		return null;
	}

	internal Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService GetServiceByMessageType(string messageRef)
	{
		if (messageRef == null)
		{
			return null;
		}
		foreach (DictionaryEntry service2 in _services)
		{
			Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = service2.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
			if (service.IsSupportedMessageType(messageRef))
			{
				return service;
			}
		}
		return null;
	}

	public string GetDestinationBySource(string source)
	{
		foreach (DictionaryEntry destination2 in _destinations)
		{
			Destination destination = destination2.Value as Destination;
			if (destination.Source == source)
			{
				return destination.Id;
			}
		}
		return null;
	}

	public Destination GetDestination(string destinationId)
	{
		foreach (DictionaryEntry destination2 in _destinations)
		{
			Destination destination = destination2.Value as Destination;
			if (destination.Id == destinationId)
			{
				return destination;
			}
		}
		return null;
	}

	public string GetDestinationId(IMessage message)
	{
		if (message.destination != null)
		{
			return message.destination;
		}
		if (message is RemotingMessage)
		{
			RemotingMessage remotingMessage = message as RemotingMessage;
			string destinationBySource = GetDestinationBySource(remotingMessage.source);
			if (destinationBySource != null)
			{
				return destinationBySource;
			}
			Destination destination = null;
			foreach (DictionaryEntry service2 in _services)
			{
				Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = service2.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
				if (!service.IsSupportedMessage(message))
				{
					continue;
				}
				Destination[] destinations = service.GetDestinations();
				Destination[] array = destinations;
				foreach (Destination destination2 in array)
				{
					if (destination2.Source == remotingMessage.source)
					{
						return destination2.Id;
					}
					if (destination2.Source == "*")
					{
						destination = destination2;
					}
				}
			}
			if (destination != null)
			{
				return destination.Id;
			}
		}
		return null;
	}

	public string GetDestinationId(string source)
	{
		ValidationUtils.ArgumentNotNullOrEmpty(source, "source");
		string destinationBySource = GetDestinationBySource(source);
		if (destinationBySource != null)
		{
			return destinationBySource;
		}
		Destination destination = null;
		foreach (DictionaryEntry service2 in _services)
		{
			Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService service = service2.Value as Dolo.PlanetAI.NET.Fluorine.Messaging.Services.IService;
			if (!service.IsSupportedMessageType("flex.messaging.messages.RemotingMessage"))
			{
				continue;
			}
			Destination[] destinations = service.GetDestinations();
			Destination[] array = destinations;
			foreach (Destination destination2 in array)
			{
				if (destination2.Source == source)
				{
					return destination2.Id;
				}
				if (destination2.Source == "*")
				{
					destination = destination2;
				}
			}
		}
		return destination?.Id;
	}

	internal IClient GetClient(string id)
	{
		return _clientManager.LookupClient(id);
	}
}
