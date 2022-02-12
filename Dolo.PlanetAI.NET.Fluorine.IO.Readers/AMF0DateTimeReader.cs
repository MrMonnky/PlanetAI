namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0DateTimeReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadDateTime();
	}
}
