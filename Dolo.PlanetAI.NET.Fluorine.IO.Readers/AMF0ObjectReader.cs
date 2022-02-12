namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0ObjectReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadObject();
	}
}
