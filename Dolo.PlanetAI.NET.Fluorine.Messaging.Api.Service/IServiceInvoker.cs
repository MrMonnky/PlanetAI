namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

internal interface IServiceInvoker
{
	bool Invoke(IServiceCall call, IScope scope);

	bool Invoke(IServiceCall call, object service);
}
