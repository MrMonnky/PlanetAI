using System;
using System.Collections;
using System.Text;
using Dolo.PlanetAI.NET.Fluorine.Context;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services.Messaging;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal sealed class MessageClient : IMessageClient
{
	private static object _syncLock = new object();

	private string _messageClientId;

	private byte[] _binaryId;

	private string _endpoint;

	private IMessageConnection _connection;

	private IClient _client;

	private Subtopic _subtopic;

	private static Hashtable _messageClientCreatedListeners;

	private Hashtable _messageClientDestroyedListeners;

	private bool _isDisconnecting;

	public object SyncRoot => _syncLock;

	internal IMessageConnection MessageConnection => _connection;

	public string Endpoint => _endpoint;

	public string ClientId => _messageClientId;

	public bool IsDisconnecting => _isDisconnecting;

	public Subtopic Subtopic
	{
		get
		{
			return _subtopic;
		}
		set
		{
			_subtopic = value;
		}
	}

	private MessageClient()
	{
	}

	internal MessageClient(IClient client, string messageClientId, string endpoint)
	{
		_client = client;
		_messageClientId = messageClientId;
		_endpoint = endpoint;
		_connection = AMFContext.Current.Connection as IMessageConnection;
		if (_connection != null)
		{
			_connection.RegisterMessageClient(this);
		}
		if (_messageClientCreatedListeners == null)
		{
			return;
		}
		foreach (IMessageClientListener key in _messageClientCreatedListeners.Keys)
		{
			key.MessageClientCreated(this);
		}
	}

	public byte[] GetBinaryId()
	{
		if (_binaryId == null)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			_binaryId = uTF8Encoding.GetBytes(_messageClientId);
		}
		return _binaryId;
	}

	internal void SetIsDisconnecting(bool value)
	{
		_isDisconnecting = value;
	}

	public static void AddMessageClientCreatedListener(IMessageClientListener listener)
	{
		lock (typeof(MessageClient))
		{
			if (_messageClientCreatedListeners == null)
			{
				_messageClientCreatedListeners = new Hashtable(1);
			}
			_messageClientCreatedListeners[listener] = null;
		}
	}

	public static void RemoveMessageClientCreatedListener(IMessageClientListener listener)
	{
		lock (typeof(MessageClient))
		{
			if (_messageClientCreatedListeners != null && _messageClientCreatedListeners.Contains(listener))
			{
				_messageClientCreatedListeners.Remove(listener);
			}
		}
	}

	public void AddMessageClientDestroyedListener(IMessageClientListener listener)
	{
		if (_messageClientDestroyedListeners == null)
		{
			_messageClientDestroyedListeners = new Hashtable(1);
		}
		_messageClientDestroyedListeners[listener] = null;
	}

	public void RemoveMessageClientDestroyedListener(IMessageClientListener listener)
	{
		if (_messageClientDestroyedListeners != null && _messageClientDestroyedListeners.Contains(listener))
		{
			_messageClientDestroyedListeners.Remove(listener);
		}
	}

	internal void Disconnect()
	{
		SetIsDisconnecting(value: true);
		Unsubscribe(timeout: false);
	}

	internal void Unsubscribe()
	{
		if (_messageClientDestroyedListeners != null)
		{
			foreach (IMessageClientListener key in _messageClientDestroyedListeners.Keys)
			{
				key.MessageClientDestroyed(this);
			}
		}
		_client.UnregisterMessageClient(this);
	}

	internal void Timeout()
	{
		try
		{
			if (!IsDisconnecting)
			{
				CommandMessage commandMessage = new CommandMessage();
				commandMessage.destination = "AMF";
				commandMessage.clientId = ClientId;
				commandMessage.operation = 10;
				commandMessage.headers["DSId"] = _client.Id;
				object[] array = new object[1] { commandMessage.clientId };
				Unsubscribe(timeout: true);
			}
		}
		catch (Exception)
		{
		}
	}

	private void Unsubscribe(bool timeout)
	{
		CommandMessage commandMessage = new CommandMessage();
		commandMessage.destination = "AMF";
		commandMessage.operation = 1;
		commandMessage.clientId = ClientId;
		if (timeout)
		{
			commandMessage.headers[CommandMessage.SessionInvalidatedHeader] = true;
			commandMessage.headers[CommandMessage.AMFMessageClientTimeoutHeader] = true;
			commandMessage.headers["DSId"] = _client.Id;
		}
	}
}
