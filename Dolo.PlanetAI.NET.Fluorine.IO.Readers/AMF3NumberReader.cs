namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3NumberReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadDouble();
	}
}
