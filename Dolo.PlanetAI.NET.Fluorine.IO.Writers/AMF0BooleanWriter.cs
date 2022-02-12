namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0BooleanWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteByte(1);
		writer.WriteBoolean((bool)data);
	}
}
