using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IClient : IAttributeStore
{
	string Id { get; }

	ICollection Scopes { get; }

	ICollection Connections { get; }

	object SyncRoot { get; }

	int ClientLeaseTime { get; }

	void Disconnect();

	void Timeout();

	IMessage[] GetPendingMessages(int waitIntervalMillis);

	void RegisterMessageClient(IMessageClient messageClient);

	void UnregisterMessageClient(IMessageClient messageClient);

	void Renew();

	void Renew(int clientLeaseTime);

	void Register(IConnection connection);

	void Unregister(IConnection connection);
}
