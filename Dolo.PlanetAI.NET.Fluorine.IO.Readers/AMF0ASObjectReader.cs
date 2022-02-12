namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0ASObjectReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadASObject();
	}
}
