namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0AMF3TagWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(17);
		writer.WriteAMF3Data(data);
	}
}
