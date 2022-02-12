namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0StringWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteString(data as string);
	}
}
