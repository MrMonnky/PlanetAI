namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0ReferenceReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadReference();
	}
}
