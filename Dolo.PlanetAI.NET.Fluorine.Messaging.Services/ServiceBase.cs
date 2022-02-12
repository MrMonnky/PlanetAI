using System;
using System.Collections;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

internal class ServiceBase : IService
{
	protected MessageBroker _messageBroker;

	protected ServiceSettings _serviceSettings;

	protected Hashtable _destinations;

	private object _objLock = new object();

	protected Destination _defaultDestination;

	public ServiceSettings ServiceSettings => _serviceSettings;

	public string id => _serviceSettings.Id;

	public Destination DefaultDestination => _defaultDestination;

	internal ServiceBase()
	{
	}

	internal ServiceBase(MessageBroker messageBroker, ServiceSettings serviceSettings)
	{
		_messageBroker = messageBroker;
		_serviceSettings = serviceSettings;
		_destinations = new Hashtable();
		foreach (DestinationSettings destinationSetting in serviceSettings.DestinationSettings)
		{
			CreateDestination(destinationSetting);
		}
	}

	protected virtual Destination NewDestination(DestinationSettings destinationSettings)
	{
		return new Destination(this, destinationSettings);
	}

	public MessageBroker GetMessageBroker()
	{
		return _messageBroker;
	}

	public Destination GetDestination(IMessage message)
	{
		lock (_objLock)
		{
			return _destinations[message.destination] as Destination;
		}
	}

	public Destination[] GetDestinations()
	{
		lock (_objLock)
		{
			ArrayList arrayList = new ArrayList(_destinations.Values);
			return arrayList.ToArray(typeof(Destination)) as Destination[];
		}
	}

	public Destination GetDestinationWithSource(string source)
	{
		lock (_objLock)
		{
			foreach (Destination value in _destinations.Values)
			{
				string text = value.DestinationSettings.Properties["source"] as string;
				if (source == text)
				{
					return value;
				}
			}
			return null;
		}
	}

	public Destination GetDestination(string id)
	{
		lock (_objLock)
		{
			return _destinations[id] as Destination;
		}
	}

	public virtual Task<object> ServiceMessage(IMessage message)
	{
		if (message is CommandMessage commandMessage && (commandMessage.operation == 13 || commandMessage.operation == 5))
		{
			return Task.FromResult((object)true);
		}
		throw new NotSupportedException();
	}

	public bool IsSupportedMessage(IMessage message)
	{
		return IsSupportedMessageType(message.GetType().FullName);
	}

	public bool IsSupportedMessageType(string messageClassName)
	{
		bool flag = _serviceSettings.SupportedMessageTypes.Contains(messageClassName);
		if (!flag)
		{
			string customClass = AMFConfiguration.Instance.ClassMappings.GetCustomClass(messageClassName);
			return _serviceSettings.SupportedMessageTypes.Contains(customClass);
		}
		return flag;
	}

	public virtual void Start()
	{
	}

	public virtual void Stop()
	{
	}

	public virtual Destination CreateDestination(DestinationSettings destinationSettings)
	{
		lock (_objLock)
		{
			if (!_destinations.ContainsKey(destinationSettings.Id))
			{
				Destination destination = NewDestination(destinationSettings);
				if (destinationSettings.Adapter != null)
				{
					destination.Init(destinationSettings.Adapter);
				}
				else
				{
					destination.Init(_serviceSettings.DefaultAdapter);
				}
				_destinations[destination.Id] = destination;
				if (destination.DestinationSettings.Properties["source"] is string text && text == "*")
				{
					_defaultDestination = destination;
				}
				return destination;
			}
			return _destinations[destinationSettings.Id] as Destination;
		}
	}
}
