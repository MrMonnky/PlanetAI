using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal class PipeConnectionEvent
{
	public const int PROVIDER_CONNECT_PULL = 0;

	public const int PROVIDER_CONNECT_PUSH = 1;

	public const int PROVIDER_DISCONNECT = 2;

	public const int CONSUMER_CONNECT_PULL = 3;

	public const int CONSUMER_CONNECT_PUSH = 4;

	public const int CONSUMER_DISCONNECT = 5;

	private IProvider _provider;

	private IConsumer _consumer;

	private int _type;

	private Dictionary<string, object> _parameterMap;

	private object _source;

	public IProvider Provider
	{
		get
		{
			return _provider;
		}
		set
		{
			_provider = value;
		}
	}

	public IConsumer Consumer
	{
		get
		{
			return _consumer;
		}
		set
		{
			_consumer = value;
		}
	}

	public int Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
		}
	}

	public Dictionary<string, object> ParameterMap
	{
		get
		{
			return _parameterMap;
		}
		set
		{
			_parameterMap = value;
		}
	}

	public object Source
	{
		get
		{
			return _source;
		}
		set
		{
			_source = value;
		}
	}

	public PipeConnectionEvent(object source)
	{
		_source = source;
	}
}
