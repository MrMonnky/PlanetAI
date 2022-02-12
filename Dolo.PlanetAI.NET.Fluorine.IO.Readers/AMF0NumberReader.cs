namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0NumberReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadDouble();
	}
}
