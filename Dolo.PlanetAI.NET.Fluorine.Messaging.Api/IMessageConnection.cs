using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IMessageConnection
{
	int ClientCount { get; }

	void RegisterMessageClient(IMessageClient client);

	void RemoveMessageClient(string clientId);

	void RemoveMessageClients();

	bool IsClientRegistered(string clientId);

	void Push(IMessage message, IMessageClient messageClient);
}
