using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class ClientManager : IClientRegistry
{
	private static object _objLock = new object();

	private static Hashtable _sessionCreatedListeners = new Hashtable();

	private MessageBroker _messageBroker;

	private Hashtable _clients;

	private ClientManager()
	{
	}

	internal ClientManager(MessageBroker messageBroker)
	{
		_messageBroker = messageBroker;
		_clients = new Hashtable();
	}

	internal string GetNextId()
	{
		return Guid.NewGuid().ToString("N");
	}

	public IClient GetClient(IMessage message)
	{
		if (message.HeaderExists("DSId"))
		{
			string id = message.GetHeader("DSId") as string;
			return GetClient(id);
		}
		return null;
	}

	public IClient GetClient(string id)
	{
		lock (_objLock)
		{
			if (_clients.ContainsKey(id))
			{
				return _clients[id] as Client;
			}
			if (id == null || id == "nil" || id == string.Empty)
			{
				id = Guid.NewGuid().ToString("N");
			}
			Client client = new Client(this, id);
			int clientLeaseTime = 1;
			Renew(client, clientLeaseTime);
			return client;
		}
	}

	public bool HasClient(string id)
	{
		if (id == null)
		{
			return false;
		}
		lock (_objLock)
		{
			return _clients.ContainsKey(id);
		}
	}

	public IClient LookupClient(string clientId)
	{
		if (clientId == null)
		{
			return null;
		}
		lock (_objLock)
		{
			Client result = null;
			if (_clients.Contains(clientId))
			{
				result = _clients[clientId] as Client;
			}
			return result;
		}
	}

	internal void Renew(Client client, int clientLeaseTime)
	{
		lock (_objLock)
		{
			_clients[client.Id] = client;
			if (client.ClientLeaseTime < clientLeaseTime)
			{
				client.SetClientLeaseTime(clientLeaseTime);
			}
			if (clientLeaseTime == 0)
			{
				client.SetClientLeaseTime(0);
			}
		}
	}

	internal Client RemoveSubscriber(Client client)
	{
		lock (_objLock)
		{
			RemoveSubscriber(client.Id);
			return client;
		}
	}

	internal Client RemoveSubscriber(string clientId)
	{
		lock (_objLock)
		{
			Client result = _clients[clientId] as Client;
			_clients.Remove(clientId);
			return result;
		}
	}
}
