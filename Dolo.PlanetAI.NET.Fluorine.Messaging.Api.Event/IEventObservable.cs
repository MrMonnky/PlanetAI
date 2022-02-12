using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

internal interface IEventObservable
{
	void AddEventListener(IEventListener listener);

	void RemoveEventListener(IEventListener listener);

	ICollection GetEventListeners();
}
