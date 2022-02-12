namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

internal interface IEvent
{
	EventType EventType { get; }

	object Object { get; }

	bool HasSource { get; }

	IEventListener Source { get; set; }
}
