using System.Collections;
using System.Net;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IConnection : ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener
{
	bool IsConnected { get; }

	IDictionary Parameters { get; }

	IClient Client { get; }

	IScope Scope { get; }

	IEnumerator BasicScopes { get; }

	string ConnectionId { get; }

	string SessionId { get; }

	ObjectEncoding ObjectEncoding { get; }

	object SyncRoot { get; }

	long ReadBytes { get; }

	long WrittenBytes { get; }

	long ReadMessages { get; }

	long WrittenMessages { get; }

	long DroppedMessages { get; }

	long PendingMessages { get; }

	long ClientBytesRead { get; }

	int LastPingTime { get; }

	int ClientLeaseTime { get; }

	bool IsFlexClient { get; }

	IPEndPoint RemoteEndPoint { get; }

	string Path { get; }

	void Initialize(IClient client);

	bool Connect(IScope scope);

	bool Connect(IScope scope, object[] args);

	void Timeout();

	void Close();

	new string ToString();

	void Ping();
}
