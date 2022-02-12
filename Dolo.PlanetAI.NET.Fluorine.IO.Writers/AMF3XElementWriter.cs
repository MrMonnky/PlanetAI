using System.Xml.Linq;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3XElementWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteAMF3XElement(data as XElement);
	}
}
