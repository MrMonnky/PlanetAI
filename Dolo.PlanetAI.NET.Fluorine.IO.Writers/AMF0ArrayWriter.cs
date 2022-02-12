using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0ArrayWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteArray(ObjectEncoding.AMF0, data as Array);
	}
}
