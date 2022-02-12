using System;
using System.ComponentModel;
using System.Globalization;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal class ByteArrayConverter : TypeConverter
{
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(byte[]))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(byte[]))
		{
			return (value as ByteArray).MemoryStream.ToArray();
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
