namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IMessageClient
{
	object SyncRoot { get; }

	string ClientId { get; }

	bool IsDisconnecting { get; }

	byte[] GetBinaryId();
}
