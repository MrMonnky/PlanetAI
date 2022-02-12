namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0NullWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteNull();
	}
}
