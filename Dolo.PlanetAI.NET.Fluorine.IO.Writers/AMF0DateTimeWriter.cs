using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0DateTimeWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(11);
		writer.WriteDateTime((DateTime)data);
	}
}
