namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0BooleanReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadBoolean();
	}
}
