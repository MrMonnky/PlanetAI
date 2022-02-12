namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IMessageClientListener
{
	void MessageClientCreated(IMessageClient messageClient);

	void MessageClientDestroyed(IMessageClient messageClient);
}
