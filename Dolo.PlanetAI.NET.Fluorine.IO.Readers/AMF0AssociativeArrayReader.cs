namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0AssociativeArrayReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAssociativeArray();
	}
}
