using Dolo.PlanetAI.Websocket.Entities;
using Newtonsoft.Json;

namespace Dolo.PlanetAI.Websocket;

internal class MspSocketParser
{
	public static SocketResponse Parse(string raw_message)
	{
		SocketResponse socketResponse = new SocketResponse
		{
			raw_message = raw_message
		};
		if (raw_message.Length == 2)
		{
			return socketResponse;
		}
		if (raw_message.Contains("\"messageContent\":["))
		{
			string text = raw_message;
			socketResponse.ContentArray = JsonConvert.DeserializeObject<object[]>(text.Substring(13, text.Length - 13).TrimEnd(']'));
			return socketResponse;
		}
		if (raw_message.StartsWith("42[\"message\","))
		{
			string text = raw_message;
			string text2 = text.Substring(13, text.Length - 13);
			string value = text2.TrimEnd(']');
			SocketResponse socketResponse2 = JsonConvert.DeserializeObject<SocketResponse>(value);
			socketResponse2.raw_message = raw_message;
			return socketResponse2;
		}
		return socketResponse;
	}
}
