using System;
using System.Collections.Specialized;
using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.IO.Writers;

internal class AMF0NameObjectCollectionWriter : IAMFWriter
{
	public bool IsPrimitive => false;

	public void WriteData(AMFWriter writer, object data)
	{
		NameObjectCollectionBase nameObjectCollectionBase = data as NameObjectCollectionBase;
		object[] customAttributes = nameObjectCollectionBase.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), inherit: false);
		if (customAttributes != null && customAttributes.Length != 0)
		{
			DefaultMemberAttribute defaultMemberAttribute = customAttributes[0] as DefaultMemberAttribute;
			PropertyInfo property = nameObjectCollectionBase.GetType().GetProperty(defaultMemberAttribute.MemberName, new Type[1] { typeof(string) });
			if (property != null)
			{
				ASObject aSObject = new ASObject();
				for (int i = 0; i < nameObjectCollectionBase.Keys.Count; i++)
				{
					string text = nameObjectCollectionBase.Keys[i];
					object value = property.GetValue(nameObjectCollectionBase, new object[1] { text });
					aSObject.Add(text, value);
				}
				writer.WriteASO(ObjectEncoding.AMF0, aSObject);
				return;
			}
		}
		writer.WriteObject(ObjectEncoding.AMF0, data);
	}
}
