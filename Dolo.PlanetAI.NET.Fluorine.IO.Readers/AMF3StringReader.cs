namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3StringReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3String();
	}
}
