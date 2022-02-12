using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

[XmlType(TypeName = "type")]
internal sealed class NullableType
{
	private string _typeName;

	private string _value;

	private object _nullValue;

	[XmlAttribute(DataType = "string", AttributeName = "name")]
	public string TypeName
	{
		get
		{
			return _typeName;
		}
		set
		{
			_typeName = value;
			Init();
		}
	}

	[XmlAttribute(DataType = "string", AttributeName = "value")]
	public string Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
			Init();
		}
	}

	[XmlIgnore]
	public object NullValue => _nullValue;

	private void Init()
	{
		if (_typeName != null && _value != null)
		{
			Type type = Type.GetType(_typeName);
			FieldInfo field = type.GetField(_value, BindingFlags.Static | BindingFlags.Public);
			if (field != null)
			{
				_nullValue = field.GetValue(null);
			}
			else
			{
				_nullValue = Convert.ChangeType(_value, type, null);
			}
		}
	}
}
