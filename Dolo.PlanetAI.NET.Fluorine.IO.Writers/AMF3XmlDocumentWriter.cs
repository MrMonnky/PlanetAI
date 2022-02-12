using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3XmlDocumentWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteAMF3XmlDocument(data as XmlDocument);
	}
}
