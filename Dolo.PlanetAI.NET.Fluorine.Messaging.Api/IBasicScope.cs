using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IBasicScope : ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable
{
	bool HasParent { get; }

	IScope Parent { get; }

	int Depth { get; }

	new string Name { get; }

	new string Path { get; }

	string Type { get; }

	object SyncRoot { get; }
}
