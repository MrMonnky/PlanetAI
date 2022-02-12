using System;
using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class MessageBase : IMessage, ICloneable
{
	protected Dictionary<string, object> _headers = new Dictionary<string, object>();

	protected long _timestamp;

	protected object _clientId;

	protected string _destination;

	protected string _messageId;

	protected long _timeToLive;

	protected object _body;

	public const string DestinationClientIdHeader = "DSDstClientId";

	public const string EndpointHeader = "DSEndpoint";

	public const string RemoteCredentialsHeader = "DSRemoteCredentials";

	public const string RequestTimeoutHeader = "DSRequestTimeout";

	public const string FlexClientIdHeader = "DSId";

	public object clientId
	{
		get
		{
			return _clientId;
		}
		set
		{
			_clientId = value;
		}
	}

	public string destination
	{
		get
		{
			return _destination;
		}
		set
		{
			_destination = value;
		}
	}

	public string messageId
	{
		get
		{
			return _messageId;
		}
		set
		{
			_messageId = value;
		}
	}

	public long timestamp
	{
		get
		{
			return _timestamp;
		}
		set
		{
			_timestamp = value;
		}
	}

	public long timeToLive
	{
		get
		{
			return _timeToLive;
		}
		set
		{
			_timeToLive = value;
		}
	}

	public object body
	{
		get
		{
			return _body;
		}
		set
		{
			_body = value;
		}
	}

	public Dictionary<string, object> headers
	{
		get
		{
			return _headers;
		}
		set
		{
			_headers = value;
		}
	}

	public object GetHeader(string name)
	{
		if (_headers != null)
		{
			return _headers[name];
		}
		return null;
	}

	public void SetHeader(string name, object value)
	{
		if (_headers == null)
		{
			_headers = new ASObject();
		}
		_headers[name] = value;
	}

	public bool HeaderExists(string name)
	{
		if (_headers != null)
		{
			return _headers.ContainsKey(name);
		}
		return false;
	}

	public virtual object Clone()
	{
		MessageBase messageBase = MemberwiseClone() as MessageBase;
		if (_headers != null)
		{
			messageBase.headers = new Dictionary<string, object>(_headers);
		}
		return messageBase;
	}
}
