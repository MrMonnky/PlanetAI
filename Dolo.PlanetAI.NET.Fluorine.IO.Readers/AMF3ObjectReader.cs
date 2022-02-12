namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3ObjectReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3Object();
	}
}
