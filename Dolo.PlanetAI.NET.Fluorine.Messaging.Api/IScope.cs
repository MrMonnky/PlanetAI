using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IScope : IBasicScope, ICoreObject, IAttributeStore, IEventDispatcher, IEventHandler, IEventListener, IEventObservable, IPersistable, IEnumerable, IServiceContainer, IServiceProvider
{
	IScopeContext Context { get; }

	bool HasHandler { get; }

	IScopeHandler Handler { get; }

	string ContextPath { get; }

	bool Connect(IConnection connection);

	bool Connect(IConnection connection, object[] parameters);

	void Disconnect(IConnection conn);

	bool HasChildScope(string name);

	bool HasChildScope(string type, string name);

	bool CreateChildScope(string name);

	bool AddChildScope(IBasicScope scope);

	void RemoveChildScope(IBasicScope scope);

	ICollection GetScopeNames();

	IEnumerator GetBasicScopeNames(string type);

	IBasicScope GetBasicScope(string type, string name);

	IScope GetScope(string name);

	ICollection GetClients();

	IEnumerator GetConnections();

	ICollection LookupConnections(IClient client);
}
