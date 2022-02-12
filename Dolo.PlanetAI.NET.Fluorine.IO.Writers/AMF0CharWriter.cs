namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0CharWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(2);
		writer.WriteUTF(new string((char)data, 1));
	}
}
