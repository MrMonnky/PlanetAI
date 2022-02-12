using System.Xml.Linq;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0XElementWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteXElement(data as XElement);
	}
}
