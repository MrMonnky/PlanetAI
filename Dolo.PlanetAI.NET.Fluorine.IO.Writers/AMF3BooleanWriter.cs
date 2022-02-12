namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3BooleanWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteAMF3Bool((bool)data);
	}
}
