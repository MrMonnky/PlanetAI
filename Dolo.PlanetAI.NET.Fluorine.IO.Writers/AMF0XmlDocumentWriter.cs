using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0XmlDocumentWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteXmlDocument(data as XmlDocument);
	}
}
