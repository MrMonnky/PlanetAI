namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

internal interface IEventListener
{
	void NotifyEvent(IEvent evt);
}
