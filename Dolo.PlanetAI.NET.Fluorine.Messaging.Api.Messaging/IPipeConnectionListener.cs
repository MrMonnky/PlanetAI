namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal interface IPipeConnectionListener
{
	void OnPipeConnectionEvent(PipeConnectionEvent evt);
}
