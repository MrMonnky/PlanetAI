namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

internal interface IEventDispatcher
{
	void DispatchEvent(IEvent evt);
}
