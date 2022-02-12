using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3ByteArrayWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		if (data is byte[])
		{
			data = new ByteArray(data as byte[]);
		}
		if (data is ByteArray)
		{
			writer.WriteByteArray(data as ByteArray);
		}
	}
}
