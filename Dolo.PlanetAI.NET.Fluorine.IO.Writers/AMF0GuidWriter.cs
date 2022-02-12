using System;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0GuidWriter : IAMFWriter
{
	public bool IsPrimitive => true;

	public void WriteData(AMFWriter writer, object data)
	{
		writer.WriteString(((Guid)data).ToString());
	}
}
