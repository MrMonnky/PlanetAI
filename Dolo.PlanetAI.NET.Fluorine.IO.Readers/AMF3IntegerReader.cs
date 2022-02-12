namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3IntegerReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3Int();
	}
}
