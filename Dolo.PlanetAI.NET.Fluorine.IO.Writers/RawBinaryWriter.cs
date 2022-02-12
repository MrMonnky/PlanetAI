namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class RawBinaryWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteBytes((data as RawBinary).Buffer);
	}
}
