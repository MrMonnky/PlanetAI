using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal class ArrayCollectionConverter : ArrayConverter
{
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == null)
		{
			throw new ArgumentNullException("destinationType");
		}
		if (destinationType == typeof(ArrayCollection))
		{
			return true;
		}
		if (destinationType.IsArray)
		{
			return true;
		}
		if (destinationType == typeof(ArrayList))
		{
			return true;
		}
		if (destinationType == typeof(IList))
		{
			return true;
		}
		Type @interface = destinationType.GetInterface("System.Collections.IList", ignoreCase: false);
		if (@interface != null)
		{
			return true;
		}
		Type interface2 = destinationType.GetInterface("System.Collections.Generic.ICollection`1", ignoreCase: false);
		if (interface2 != null)
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		ArrayCollection arrayCollection = value as ArrayCollection;
		ValidationUtils.ArgumentNotNull(arrayCollection, "value");
		if (destinationType == null)
		{
			throw new ArgumentNullException("destinationType");
		}
		if (destinationType == typeof(ArrayCollection))
		{
			return value;
		}
		if (destinationType.IsArray)
		{
			return arrayCollection.ToArray();
		}
		if (destinationType == typeof(ArrayList))
		{
			if (arrayCollection.List is ArrayList)
			{
				return arrayCollection.List;
			}
			return ArrayList.Adapter(arrayCollection.List);
		}
		if (destinationType == typeof(IList))
		{
			return arrayCollection.List;
		}
		Type @interface = destinationType.GetInterface("System.Collections.Generic.ICollection`1", ignoreCase: false);
		if (@interface != null)
		{
			object obj = TypeHelper.CreateInstance(destinationType);
			MethodInfo method = destinationType.GetMethod("Add");
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters != null && parameters.Length == 1)
				{
					Type parameterType = parameters[0].ParameterType;
					IList list = (IList)value;
					for (int i = 0; i < list.Count; i++)
					{
						method.Invoke(obj, new object[1] { TypeHelper.ChangeType(list[i], parameterType) });
					}
					return obj;
				}
			}
		}
		Type interface2 = destinationType.GetInterface("System.Collections.IList", ignoreCase: false);
		if (interface2 != null)
		{
			object obj2 = TypeHelper.CreateInstance(destinationType);
			IList list2 = obj2 as IList;
			for (int j = 0; j < arrayCollection.List.Count; j++)
			{
				list2.Add(arrayCollection.List[j]);
			}
			return obj2;
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
