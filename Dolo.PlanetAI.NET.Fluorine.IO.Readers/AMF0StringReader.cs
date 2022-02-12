namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0StringReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadString();
	}
}
