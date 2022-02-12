using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0NumberWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(0);
		double value = Convert.ToDouble(data);
		writer.WriteDouble(value);
	}
}
