using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3DoubleWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		double value = Convert.ToDouble(data);
		writer.WriteAMF3Double(value);
	}
}
