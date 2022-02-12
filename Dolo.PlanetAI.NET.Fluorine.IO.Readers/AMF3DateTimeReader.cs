namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF3DateTimeReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadAMF3Date();
	}
}
