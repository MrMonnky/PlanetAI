using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface ICoreObject : IAttributeStore, IEventDispatcher, IEventHandler, IEventListener
{
}
