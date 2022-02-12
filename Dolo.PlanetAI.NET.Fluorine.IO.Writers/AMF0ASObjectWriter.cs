namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0ASObjectWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteASO(ObjectEncoding.AMF0, data as ASObject);
	}
}
