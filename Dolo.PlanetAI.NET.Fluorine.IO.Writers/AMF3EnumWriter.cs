using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3EnumWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		int value = Convert.ToInt32(data);
		writer.WriteAMF3Int(value);
	}
}
