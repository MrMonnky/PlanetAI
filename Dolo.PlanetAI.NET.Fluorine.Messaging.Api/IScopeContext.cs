using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Persistence;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IScopeContext
{
	IClientRegistry ClientRegistry { get; }

	IPersistenceStore PersistenceStore { get; }

	IServiceInvoker ServiceInvoker { get; }

	IScopeResolver ScopeResolver { get; }

	IScope ResolveScope(string path);

	IScope ResolveScope(IScope root, string path);

	IScope GetGlobalScope();

	IScopeHandler LookupScopeHandler(string path);
}
