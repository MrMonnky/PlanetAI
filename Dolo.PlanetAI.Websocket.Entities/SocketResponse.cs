using Newtonsoft.Json;

namespace Dolo.PlanetAI.Websocket.Entities;

internal class SocketResponse
{
	[JsonProperty("messageType", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public long Type { get; set; }

	[JsonProperty("messageContent", DefaultValueHandling = DefaultValueHandling.Ignore)]
	public object Content { get; set; }

	public object[] ContentArray { get; set; }

	public string raw_message { get; set; }
}
