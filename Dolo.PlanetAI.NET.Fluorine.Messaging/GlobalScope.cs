using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class GlobalScope : Scope, IGlobalScope, IScope, IBasicScope, ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable, IServiceContainer, IServiceProvider
{
	public IServiceProvider ServiceProvider => _serviceContainer;

	internal GlobalScope()
	{
	}

	public void Register()
	{
		Init();
	}
}
