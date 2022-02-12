using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF3ObjectWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		if (data is IList)
		{
			if (writer.UseLegacyCollection)
			{
				writer.WriteByte(9);
				writer.WriteAMF3Array(data as IList);
			}
			else
			{
				writer.WriteByte(10);
				object value = new ArrayCollection(data as IList);
				writer.WriteAMF3Object(value);
			}
		}
		else if (data is IDictionary)
		{
			writer.WriteByte(10);
			writer.WriteAMF3Object(data);
		}
		else if (data is Exception)
		{
			writer.WriteByte(10);
			writer.WriteAMF3Object(new ExceptionASO(data as Exception));
		}
		else if (data is IExternalizable)
		{
			writer.WriteByte(10);
			writer.WriteAMF3Object(data);
		}
		else
		{
			writer.WriteByte(10);
			writer.WriteAMF3Object(data);
		}
	}
}
