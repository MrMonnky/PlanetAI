namespace Dolo.PlanetAI.NET.Fluorine.IO.Readers;

internal class AMF0XmlReader : IAMFReader
{
	public object ReadData(AMFReader reader)
	{
		return reader.ReadXmlDocument();
	}
}
