using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IGlobalScope : IScope, IBasicScope, ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable, IServiceContainer, IServiceProvider
{
	IServiceProvider ServiceProvider { get; }

	void Register();
}
