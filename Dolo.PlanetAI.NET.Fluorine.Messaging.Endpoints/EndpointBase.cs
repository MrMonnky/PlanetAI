using System;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;

internal class EndpointBase : IEndpoint
{
	protected MessageBroker _messageBroker;

	protected ChannelSettings _channelSettings;

	private string _id;

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

	public virtual bool IsSecure => false;

	public EndpointBase(MessageBroker messageBroker, ChannelSettings channelSettings)
	{
		_messageBroker = messageBroker;
		_channelSettings = channelSettings;
		_id = _channelSettings.Id;
	}

	public MessageBroker GetMessageBroker()
	{
		return _messageBroker;
	}

	public ChannelSettings GetSettings()
	{
		return _channelSettings;
	}

	public virtual void Start()
	{
	}

	public virtual void Stop()
	{
	}

	public virtual void Push(IMessage message, MessageClient messageClient)
	{
		throw new NotSupportedException();
	}

	public virtual Task Service()
	{
		return Task.FromResult<object>(null);
	}

	public virtual Task<IMessage> ServiceMessage(IMessage message)
	{
		ValidationUtils.ArgumentNotNull(message, "message");
		return _messageBroker.RouteMessage(message, this);
	}
}
