using System;
using Dolo.PlanetAI.NET.Fluorine;
using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.Websocket;

internal class MspSocketConverter
{
	public static string Serialize(ASObject o)
	{
		ByteArray byteArray = new ByteArray
		{
			Position = 0
		};
		byteArray.WriteObject(o);
		return Convert.ToBase64String(byteArray.GetBuffer());
	}

	public static byte[] Serialize(object o)
	{
		ByteArray byteArray = new ByteArray
		{
			Position = 0
		};
		byteArray.WriteObject(o);
		return byteArray.GetBuffer();
	}
}
