namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0LongStringReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		int length = reader.ReadInt32();
		return reader.ReadUTF(length);
	}
}
