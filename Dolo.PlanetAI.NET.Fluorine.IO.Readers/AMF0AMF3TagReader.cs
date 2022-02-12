namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0AMF3TagReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3Data();
	}
}
