using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Config;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;

internal interface IEndpoint
{
	string Id { get; set; }

	bool IsSecure { get; }

	MessageBroker GetMessageBroker();

	ChannelSettings GetSettings();

	void Start();

	void Stop();

	void Push(IMessage message, MessageClient messageclient);

	Task Service();

	Task<IMessage> ServiceMessage(IMessage message);
}
