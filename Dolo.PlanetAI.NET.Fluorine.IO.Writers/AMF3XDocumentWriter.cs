using System.Xml.Linq;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3XDocumentWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteAMF3XDocument(data as XDocument);
	}
}
