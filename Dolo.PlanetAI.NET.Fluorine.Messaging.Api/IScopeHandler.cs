using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Event;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IScopeHandler : IEventHandler
{
	bool Start(IScope scope);

	void Stop(IScope scope);

	bool Connect(IConnection connection, IScope scope, object[] parameters);

	void Disconnect(IConnection connection, IScope scope);

	bool AddChildScope(IBasicScope scope);

	void RemoveChildScope(IBasicScope scope);

	bool Join(IClient client, IScope scope);

	void Leave(IClient client, IScope scope);

	bool ServiceCall(IConnection connection, IServiceCall call);
}
