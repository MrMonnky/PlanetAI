namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3ASObjectWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(10);
		writer.WriteAMF3Object(data);
	}
}
