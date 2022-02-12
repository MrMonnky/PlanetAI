using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services.Remoting;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

internal class RemotingService : ServiceBase
{
	public const string RemotingServiceId = "remoting-service";

	public RemotingService(MessageBroker broker, ServiceSettings settings)
		: base(broker, settings)
	{
	}

	protected override Destination NewDestination(DestinationSettings destinationSettings)
	{
		return new RemotingDestination(this, destinationSettings);
	}

	public override async Task<object> ServiceMessage(IMessage message)
	{
		RemotingDestination destination = GetDestination(message) as RemotingDestination;
		ServiceAdapter adapter = destination.ServiceAdapter;
		Task<object> result = adapter.Invoke(message);
		await result;
		return result.Result;
	}
}
