using System.Runtime.CompilerServices;
using Dolo.PlanetAI.Websocket.Entities;
using Dolo.PlanetAI.Websocket.Enum;
using Newtonsoft.Json;

namespace Dolo.PlanetAI.Websocket;

internal static class MspSocketBuilder
{
	public static string Build(object value, ProtocolMessage Type, Protocol Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(new SocketContent
		{
			messageContent = value,
			messageType = (int)Type
		}), Protocol);
	}

	public static string Build(object value, Protocol Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(new SocketContent
		{
			messageContent = value,
			messageType = (int)Protocol
		}), Protocol);
	}

	public static string Build(object value, ProtocolMessage Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(new SocketContent
		{
			messageContent = value,
			messageType = (int)Protocol
		}), Protocol);
	}

	public static string BuildCustom(object value, Protocol Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(value), Protocol);
	}

	public static string BuildCustom(object value, ProtocolMessage Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(value), Protocol);
	}

	public static string BuildCustom(object value, int Protocol)
	{
		return BaseUrl(JsonConvert.SerializeObject(value), Protocol);
	}

	private static string BaseUrl(string data, Protocol Protocol)
	{
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
		defaultInterpolatedStringHandler.AppendLiteral("42[\"");
		defaultInterpolatedStringHandler.AppendFormatted((int)Protocol);
		defaultInterpolatedStringHandler.AppendLiteral("\",");
		defaultInterpolatedStringHandler.AppendFormatted(data);
		defaultInterpolatedStringHandler.AppendLiteral("]");
		return defaultInterpolatedStringHandler.ToStringAndClear();
	}

	private static string BaseUrl(string data, ProtocolMessage Protocol)
	{
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
		defaultInterpolatedStringHandler.AppendLiteral("42[\"");
		defaultInterpolatedStringHandler.AppendFormatted((int)Protocol);
		defaultInterpolatedStringHandler.AppendLiteral("\",");
		defaultInterpolatedStringHandler.AppendFormatted(data);
		defaultInterpolatedStringHandler.AppendLiteral("]");
		return defaultInterpolatedStringHandler.ToStringAndClear();
	}

	private static string BaseUrl(string data, int Protocol)
	{
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
		defaultInterpolatedStringHandler.AppendLiteral("42[\"");
		defaultInterpolatedStringHandler.AppendFormatted(Protocol);
		defaultInterpolatedStringHandler.AppendLiteral("\",");
		defaultInterpolatedStringHandler.AppendFormatted(data);
		defaultInterpolatedStringHandler.AppendLiteral("]");
		return defaultInterpolatedStringHandler.ToStringAndClear();
	}
}
