namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IScopeResolver
{
	IGlobalScope GlobalScope { get; }

	IScope ResolveScope(string path);

	IScope ResolveScope(IScope root, string path);
}
