using System.Collections.Generic;
using System.IO;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Utils;

internal static class AMFBuilder
{
	public static AMFMessage Encode<T>(List<AMFHeader> header, string host, params object[] data)
	{
		return new AMFMessage(3).AddMany(new AMFBody(host.Split('=')[1], typeof(T).GetHashCode().ToString(), data), header);
	}

	public static object Decode(Stream data)
	{
		return new AMFDeserializer(data).ReadAMFMessage().GetBodyAt(0).Content;
	}
}
