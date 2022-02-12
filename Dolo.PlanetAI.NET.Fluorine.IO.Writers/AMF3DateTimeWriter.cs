using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3DateTimeWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(8);
		writer.WriteAMF3DateTime((DateTime)data);
	}
}
