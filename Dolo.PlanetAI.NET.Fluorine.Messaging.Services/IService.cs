using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

internal interface IService
{
	string id { get; }

	MessageBroker GetMessageBroker();

	Destination GetDestination(IMessage message);

	Task<object> ServiceMessage(IMessage message);

	bool IsSupportedMessage(IMessage message);

	bool IsSupportedMessageType(string messageClassName);

	void Start();

	void Stop();

	Destination GetDestination(string id);

	Destination[] GetDestinations();
}
