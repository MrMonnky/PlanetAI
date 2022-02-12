namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

internal interface IPendingServiceCall : IServiceCall
{
	object Result { get; set; }

	void RegisterCallback(IPendingServiceCallback callback);

	void UnregisterCallback(IPendingServiceCallback callback);

	IPendingServiceCallback[] GetCallbacks();
}
