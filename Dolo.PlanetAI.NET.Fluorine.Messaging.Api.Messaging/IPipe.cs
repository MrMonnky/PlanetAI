namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal interface IPipe : IMessageInput, IMessageOutput
{
	void AddPipeConnectionListener(IPipeConnectionListener listener);

	void RemovePipeConnectionListener(IPipeConnectionListener listener);
}
