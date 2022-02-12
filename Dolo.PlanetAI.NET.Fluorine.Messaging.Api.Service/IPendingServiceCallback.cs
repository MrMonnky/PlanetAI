namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

internal interface IPendingServiceCallback
{
	void ResultReceived(IPendingServiceCall call);
}
