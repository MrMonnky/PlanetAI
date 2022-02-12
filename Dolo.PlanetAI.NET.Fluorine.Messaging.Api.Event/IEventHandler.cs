namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

internal interface IEventHandler
{
	bool HandleEvent(IEvent evt);
}
