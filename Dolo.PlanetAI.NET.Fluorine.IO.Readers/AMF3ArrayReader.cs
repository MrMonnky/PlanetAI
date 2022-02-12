namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3ArrayReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3Array();
	}
}
