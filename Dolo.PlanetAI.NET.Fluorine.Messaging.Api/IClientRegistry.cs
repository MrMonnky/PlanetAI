using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IClientRegistry
{
	bool HasClient(string id);

	IClient LookupClient(string id);

	IClient GetClient(string id);

	IClient GetClient(IMessage message);
}
