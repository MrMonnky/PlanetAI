namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0ArrayReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadArray();
	}
}
