using System;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Context;
using Dolo.PlanetAI.NET.Fluorine.DependencyInjection;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;

internal class AMFEndpoint : EndpointBase
{
	private FilterChain _filterChain;

	private const string LegacyCollectionKey = "legacy-collection";

	private AtomicInteger _waitingPollRequests;

	public AMFEndpoint(MessageBroker messageBroker, ChannelSettings channelSettings)
		: base(messageBroker, channelSettings)
	{
		_waitingPollRequests = new AtomicInteger();
	}

	public override void Start()
	{
		DeserializationFilter deserializationFilter = new DeserializationFilter();
		deserializationFilter.UseLegacyCollection = GetIsLegacyCollection();
		ServiceMapFilter serviceMapFilter = new ServiceMapFilter(this);
		ProcessFilter processFilter = new ProcessFilter(this);
		MessageFilter messageFilter = new MessageFilter(this);
		SerializationFilter serializationFilter = new SerializationFilter();
		serializationFilter.UseLegacyCollection = GetIsLegacyCollection();
		deserializationFilter.Next = serviceMapFilter;
		serviceMapFilter.Next = processFilter;
		processFilter.Next = messageFilter;
		messageFilter.Next = serializationFilter;
		_filterChain = new FilterChain(deserializationFilter);
	}

	public bool GetIsLegacyCollection()
	{
		if (!_channelSettings.Contains("legacy-collection"))
		{
			return false;
		}
		string value = _channelSettings["legacy-collection"] as string;
		return System.Convert.ToBoolean(value);
	}

	public override void Stop()
	{
		_filterChain = null;
		base.Stop();
	}

	public override async Task Service()
	{
		AMFContext amfContext = new AMFContext(HttpContextManager.HttpContext.GetInputStream(), HttpContextManager.HttpContext.GetOutputStream());
		await _filterChain.InvokeFilters(amfContext);
	}

	public override Task<IMessage> ServiceMessage(IMessage message)
	{
		if (message is CommandMessage)
		{
			CommandMessage commandMessage = message as CommandMessage;
			int operation = commandMessage.operation;
			int num = operation;
			if (num == 2)
			{
				if (Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client != null)
				{
					Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client.Renew();
				}
				IMessage[] array = null;
				_waitingPollRequests.Increment();
				int waitIntervalMillis = ((_channelSettings.WaitIntervalMillis != -1) ? _channelSettings.WaitIntervalMillis : 60000);
				if (Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client != null)
				{
					Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client.Renew();
				}
				if (commandMessage.HeaderExists(CommandMessage.AMFSuppressPollWaitHeader))
				{
					waitIntervalMillis = 0;
				}
				if (!AMFConfiguration.Instance.AMFSettings.Runtime.AsyncHandler)
				{
					waitIntervalMillis = 0;
				}
				if (_channelSettings.MaxWaitingPollRequests <= 0 || _waitingPollRequests.Value >= _channelSettings.MaxWaitingPollRequests)
				{
					waitIntervalMillis = 0;
				}
				if (message.destination != null && message.destination != string.Empty)
				{
					string text = commandMessage.clientId as string;
				}
				else if (Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client != null)
				{
					array = Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client.GetPendingMessages(waitIntervalMillis);
				}
				_waitingPollRequests.Decrement();
				if (array == null || array.Length == 0)
				{
					return Task.FromResult((IMessage)new AcknowledgeMessage());
				}
				Task<IMessage> task = Task.FromResult((IMessage)new CommandMessage());
				(task.Result as CommandMessage).operation = 4;
				(task.Result as CommandMessage).body = array;
				return task;
			}
		}
		return base.ServiceMessage(message);
	}

	public override void Push(IMessage message, MessageClient messageClient)
	{
		if (_channelSettings != null && _channelSettings.IsPollingEnabled)
		{
			IMessage message2 = message.Clone() as IMessage;
			message2.SetHeader("DSDstClientId", messageClient.ClientId);
			message2.clientId = messageClient.ClientId;
		}
	}
}
