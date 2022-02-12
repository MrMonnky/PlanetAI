using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3ArrayWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(9);
		writer.WriteAMF3Array(data as Array);
	}
}
