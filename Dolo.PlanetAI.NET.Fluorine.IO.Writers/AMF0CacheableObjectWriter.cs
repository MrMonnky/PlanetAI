namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0CacheableObjectWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteData(ObjectEncoding.AMF0, (data as CacheableObject).Object);
	}
}
