using System.Collections;
using System.Threading;
using Dolo.PlanetAI.NET.Fluorine.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class Client : AttributeStore, IClient, IAttributeStore
{
	private static object _syncLock = new object();

	private string _id;

	private int _clientLeaseTime;

	private ClientManager _clientManager;

	private CopyOnWriteArray _messageClients;

	protected CopyOnWriteDictionary _connectionToScope = new CopyOnWriteDictionary();

	private bool _polling;

	internal IList MessageClients
	{
		get
		{
			if (_messageClients == null)
			{
				lock (SyncRoot)
				{
					if (_messageClients == null)
					{
						_messageClients = new CopyOnWriteArray();
					}
				}
			}
			return _messageClients;
		}
	}

	public string Id => _id;

	public int ClientLeaseTime => _clientLeaseTime;

	public object SyncRoot => _syncLock;

	public ICollection Scopes => _connectionToScope.Values;

	public ICollection Connections => _connectionToScope.Keys;

	internal Client(ClientManager clientManager, string id)
	{
		_clientManager = clientManager;
		_id = id;
		_clientLeaseTime = 1;
		_polling = false;
	}

	public void Register(IConnection connection)
	{
		_connectionToScope.Add(connection, connection.Scope);
	}

	public void Unregister(IConnection connection)
	{
		_connectionToScope.Remove(connection);
		if (_connectionToScope.Count == 0)
		{
			Disconnect();
		}
	}

	internal void SetClientLeaseTime(int value)
	{
		_clientLeaseTime = value;
	}

	public void RegisterMessageClient(IMessageClient messageClient)
	{
		if (!MessageClients.Contains(messageClient))
		{
			MessageClients.Add(messageClient);
		}
	}

	public void UnregisterMessageClient(IMessageClient messageClient)
	{
		if (!messageClient.IsDisconnecting)
		{
			if (MessageClients.Contains(messageClient))
			{
				MessageClients.Remove(messageClient);
			}
			if (MessageClients.Count == 0)
			{
				Disconnect();
			}
		}
	}

	public void Disconnect(bool timeout)
	{
		lock (SyncRoot)
		{
			IConnection connection = null;
			if (Connections != null && Connections.Count > 0)
			{
				IEnumerator enumerator = Connections.GetEnumerator();
				enumerator.MoveNext();
				connection = enumerator.Current as IConnection;
			}
			_clientManager.RemoveSubscriber(this);
			if (_messageClients != null)
			{
				foreach (MessageClient messageClient in _messageClients)
				{
					if (timeout)
					{
						messageClient.Timeout();
					}
					else
					{
						messageClient.Disconnect();
					}
				}
				_messageClients.Clear();
			}
			foreach (IConnection connection2 in Connections)
			{
				if (timeout)
				{
					connection2.Timeout();
				}
				connection2.Close();
			}
		}
	}

	public void Disconnect()
	{
		Disconnect(timeout: false);
	}

	public void Timeout()
	{
		Disconnect(timeout: true);
	}

	public IMessage[] GetPendingMessages(int waitIntervalMillis)
	{
		ArrayList arrayList = new ArrayList();
		_polling = true;
		do
		{
			_clientManager.LookupClient(_id);
			if (waitIntervalMillis == 0)
			{
				_polling = false;
				return arrayList.ToArray(typeof(IMessage)) as IMessage[];
			}
			if (arrayList.Count > 0)
			{
				_polling = false;
				return arrayList.ToArray(typeof(IMessage)) as IMessage[];
			}
			Thread.Sleep(500);
			waitIntervalMillis -= 500;
			if (waitIntervalMillis <= 0)
			{
				_polling = false;
			}
		}
		while (_polling);
		return arrayList.ToArray(typeof(IMessage)) as IMessage[];
	}

	public void Renew()
	{
		_clientManager.LookupClient(_id);
	}

	public void Renew(int clientLeaseTime)
	{
		_clientManager.Renew(this, clientLeaseTime);
	}

	public override string ToString()
	{
		return "Client " + _id.ToString();
	}
}
