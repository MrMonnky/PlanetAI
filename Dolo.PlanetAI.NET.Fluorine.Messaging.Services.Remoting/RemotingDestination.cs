using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services.Remoting;

internal class RemotingDestination : Destination
{
	public RemotingDestination(IService service, DestinationSettings destinationSettings)
		: base(service, destinationSettings)
	{
	}
}
