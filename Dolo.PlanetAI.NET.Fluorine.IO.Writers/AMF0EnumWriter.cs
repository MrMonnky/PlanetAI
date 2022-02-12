using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0EnumWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(0);
		double value = Convert.ToInt32(data);
		writer.WriteDouble(value);
	}
}
