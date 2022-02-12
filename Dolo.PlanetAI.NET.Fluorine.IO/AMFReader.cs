using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.IO.Readers;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFReader : BinaryReader
{
	private bool _useLegacyCollection = true;

	private bool _faultTolerancy = false;

	private Exception _lastError;

	private List<object> _amf0ObjectReferences;

	private List<object> _objectReferences;

	private List<object> _stringReferences;

	private List<ClassDefinition> _classDefinitions;

	private static IAMFReader[][] AmfTypeTable;

	public bool UseLegacyCollection
	{
		get
		{
			return _useLegacyCollection;
		}
		set
		{
			_useLegacyCollection = value;
		}
	}

	public bool FaultTolerancy
	{
		get
		{
			return _faultTolerancy;
		}
		set
		{
			_faultTolerancy = value;
		}
	}

	public Exception LastError => _lastError;

	static AMFReader()
	{
		IAMFReader[] array = new IAMFReader[18]
		{
			new AMF0NumberReader(),
			new AMF0BooleanReader(),
			new AMF0StringReader(),
			new AMF0ASObjectReader(),
			new AMFUnknownTagReader(),
			new AMF0NullReader(),
			new AMF0NullReader(),
			new AMF0ReferenceReader(),
			new AMF0AssociativeArrayReader(),
			new AMFUnknownTagReader(),
			new AMF0ArrayReader(),
			new AMF0DateTimeReader(),
			new AMF0LongStringReader(),
			new AMFUnknownTagReader(),
			new AMFUnknownTagReader(),
			new AMF0XmlReader(),
			new AMF0ObjectReader(),
			new AMF0AMF3TagReader()
		};
		IAMFReader[] array2 = new IAMFReader[18]
		{
			new AMF3NullReader(),
			new AMF3NullReader(),
			new AMF3BooleanFalseReader(),
			new AMF3BooleanTrueReader(),
			new AMF3IntegerReader(),
			new AMF3NumberReader(),
			new AMF3StringReader(),
			new AMF3XmlReader(),
			new AMF3DateTimeReader(),
			new AMF3ArrayReader(),
			new AMF3ObjectReader(),
			new AMF3XmlReader(),
			new AMF3ByteArrayReader(),
			new AMFUnknownTagReader(),
			new AMFUnknownTagReader(),
			new AMFUnknownTagReader(),
			new AMFUnknownTagReader(),
			new AMFUnknownTagReader()
		};
		AmfTypeTable = new IAMFReader[4][] { array, null, null, array2 };
	}

	public AMFReader(Stream stream)
		: base(stream)
	{
		Reset();
	}

	public void Reset()
	{
		_amf0ObjectReferences = new List<object>(5);
		_objectReferences = new List<object>(15);
		_stringReferences = new List<object>(15);
		_classDefinitions = new List<ClassDefinition>(2);
		_lastError = null;
	}

	public object ReadData()
	{
		byte typeMarker = ReadByte();
		return ReadData(typeMarker);
	}

	public object ReadData(byte typeMarker)
	{
		return AmfTypeTable[0][typeMarker].ReadData(this);
	}

	public object ReadReference()
	{
		int index = ReadUInt16();
		return _amf0ObjectReferences[index];
	}

	public override ushort ReadUInt16()
	{
		byte[] array = ReadBytes(2);
		return (ushort)((uint)((array[0] & 0xFF) << 8) | (array[1] & 0xFFu));
	}

	public override short ReadInt16()
	{
		byte[] array = ReadBytes(2);
		return (short)((array[0] << 8) | array[1]);
	}

	public override string ReadString()
	{
		int length = ReadUInt16();
		return ReadUTF(length);
	}

	public override bool ReadBoolean()
	{
		return base.ReadBoolean();
	}

	public override int ReadInt32()
	{
		byte[] array = ReadBytes(4);
		return (array[0] << 24) | (array[1] << 16) | (array[2] << 8) | array[3];
	}

	public int ReadUInt24()
	{
		byte[] array = ReadBytes(3);
		return (array[0] << 16) | (array[1] << 8) | array[2];
	}

	public override double ReadDouble()
	{
		byte[] array = ReadBytes(8);
		byte[] array2 = new byte[8];
		int num = 7;
		int num2 = 0;
		while (num >= 0)
		{
			array2[num2] = array[num];
			num--;
			num2++;
		}
		return BitConverter.ToDouble(array2, 0);
	}

	public float ReadFloat()
	{
		byte[] array = ReadBytes(4);
		byte[] array2 = new byte[4];
		int num = 3;
		int num2 = 0;
		while (num >= 0)
		{
			array2[num2] = array[num];
			num--;
			num2++;
		}
		return BitConverter.ToSingle(array2, 0);
	}

	public void AddReference(object instance)
	{
		_amf0ObjectReferences.Add(instance);
	}

	public object ReadObject()
	{
		string typeName = ReadString();
		Type type = ObjectFactory.Locate(typeName);
		if (type != null)
		{
			object obj = ObjectFactory.CreateInstance(type);
			AddReference(obj);
			string memberName = ReadString();
			for (byte b = ReadByte(); b != 9; b = ReadByte())
			{
				object value = ReadData(b);
				SetMember(obj, memberName, value);
				memberName = ReadString();
			}
			return obj;
		}
		ASObject aSObject = ReadASObject();
		aSObject.TypeName = typeName;
		return aSObject;
	}

	public ASObject ReadASObject()
	{
		ASObject aSObject = new ASObject();
		AddReference(aSObject);
		string key = ReadString();
		for (byte b = ReadByte(); b != 9; b = ReadByte())
		{
			aSObject[key] = ReadData(b);
			key = ReadString();
		}
		return aSObject;
	}

	public string ReadUTF(int length)
	{
		if (length == 0)
		{
			return string.Empty;
		}
		UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
		byte[] array = ReadBytes(length);
		return uTF8Encoding.GetString(array, 0, array.Length);
	}

	public string ReadLongString()
	{
		int length = ReadInt32();
		return ReadUTF(length);
	}

	internal Dictionary<string, object> ReadAssociativeArray()
	{
		int capacity = ReadInt32();
		Dictionary<string, object> dictionary = new Dictionary<string, object>(capacity);
		AddReference(dictionary);
		string key = ReadString();
		for (byte b = ReadByte(); b != 9; b = ReadByte())
		{
			object value = ReadData(b);
			dictionary.Add(key, value);
			key = ReadString();
		}
		return dictionary;
	}

	internal IList ReadArray()
	{
		int num = ReadInt32();
		object[] array = new object[num];
		AddReference(array);
		for (int i = 0; i < num; i++)
		{
			array[i] = ReadData();
		}
		return array;
	}

	public DateTime ReadDateTime()
	{
		double value = ReadDouble();
		DateTime value2 = new DateTime(1970, 1, 1).AddMilliseconds(value);
		value2 = DateTime.SpecifyKind(value2, DateTimeKind.Utc);
		int num = ReadUInt16();
		if (num > 720)
		{
			num = 65536 - num;
		}
		int num2 = num / 60;
		TimezoneCompensation timezoneCompensation = AMFConfiguration.Instance.TimezoneCompensation;
		TimezoneCompensation timezoneCompensation2 = timezoneCompensation;
		if (timezoneCompensation2 != 0 && timezoneCompensation2 == TimezoneCompensation.Auto)
		{
			value2 = value2.AddHours(num2);
			value2 = DateTime.SpecifyKind(value2, DateTimeKind.Unspecified);
		}
		return value2;
	}

	public XmlDocument ReadXmlDocument()
	{
		string text = ReadLongString();
		XmlDocument xmlDocument = new XmlDocument();
		if (text != null && text != string.Empty)
		{
			xmlDocument.LoadXml(text);
		}
		return xmlDocument;
	}

	public object ReadAMF3Data()
	{
		byte typeMarker = ReadByte();
		return ReadAMF3Data(typeMarker);
	}

	public object ReadAMF3Data(byte typeMarker)
	{
		return AmfTypeTable[3][typeMarker].ReadData(this);
	}

	public void AddAMF3ObjectReference(object instance)
	{
		_objectReferences.Add(instance);
	}

	public object ReadAMF3ObjectReference(int index)
	{
		return _objectReferences[index];
	}

	public int ReadAMF3IntegerData()
	{
		int num = ReadByte();
		if (num < 128)
		{
			return num;
		}
		num = (num & 0x7F) << 7;
		int num2 = ReadByte();
		if (num2 < 128)
		{
			num |= num2;
		}
		else
		{
			num = (num | (num2 & 0x7F)) << 7;
			num2 = ReadByte();
			if (num2 < 128)
			{
				num |= num2;
			}
			else
			{
				num = (num | (num2 & 0x7F)) << 8;
				num2 = ReadByte();
				num |= num2;
			}
		}
		int num3 = 268435456;
		return -(num & num3) | num;
	}

	public int ReadAMF3Int()
	{
		return ReadAMF3IntegerData();
	}

	public DateTime ReadAMF3Date()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (flag)
		{
			double value = ReadDouble();
			DateTime value2 = new DateTime(1970, 1, 1, 1, 0, 0).AddMilliseconds(value);
			value2 = DateTime.SpecifyKind(value2, DateTimeKind.Utc);
			AddAMF3ObjectReference(value2);
			return value2;
		}
		return (DateTime)ReadAMF3ObjectReference(num);
	}

	internal void AddStringReference(string str)
	{
		_stringReferences.Add(str);
	}

	internal string ReadStringReference(int index)
	{
		return _stringReferences[index] as string;
	}

	public string ReadAMF3String()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (flag)
		{
			int num2 = num;
			if (num2 == 0)
			{
				return string.Empty;
			}
			string text = ReadUTF(num2);
			AddStringReference(text);
			return text;
		}
		return ReadStringReference(num);
	}

	public XmlDocument ReadAMF3XmlDocument()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		string text = string.Empty;
		if (flag)
		{
			if (num > 0)
			{
				text = ReadUTF(num);
			}
			AddAMF3ObjectReference(text);
		}
		else
		{
			text = ReadAMF3ObjectReference(num) as string;
		}
		XmlDocument xmlDocument = new XmlDocument();
		if (text != null && text != string.Empty)
		{
			xmlDocument.LoadXml(text);
		}
		return xmlDocument;
	}

	public ByteArray ReadAMF3ByteArray()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (flag)
		{
			int count = num;
			byte[] buffer = ReadBytes(count);
			ByteArray byteArray = new ByteArray(buffer);
			AddAMF3ObjectReference(byteArray);
			return byteArray;
		}
		return ReadAMF3ObjectReference(num) as ByteArray;
	}

	public object ReadAMF3Array()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (flag)
		{
			Dictionary<string, object> dictionary = null;
			string text = ReadAMF3String();
			while (text != null && text != string.Empty)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AddAMF3ObjectReference(dictionary);
				}
				object value = ReadAMF3Data();
				dictionary.Add(text, value);
				text = ReadAMF3String();
			}
			if (dictionary == null)
			{
				object[] array = new object[num];
				AddAMF3ObjectReference(array);
				for (int i = 0; i < num; i++)
				{
					byte typeMarker = ReadByte();
					object obj = (array[i] = ReadAMF3Data(typeMarker));
				}
				return array;
			}
			for (int j = 0; j < num; j++)
			{
				object value2 = ReadAMF3Data();
				dictionary.Add(j.ToString(), value2);
			}
			return dictionary;
		}
		return ReadAMF3ObjectReference(num);
	}

	internal void AddClassReference(ClassDefinition classDefinition)
	{
		_classDefinitions.Add(classDefinition);
	}

	internal ClassDefinition ReadClassReference(int index)
	{
		return _classDefinitions[index];
	}

	internal ClassDefinition ReadClassDefinition(int handle)
	{
		ClassDefinition classDefinition = null;
		bool flag = (handle & 1) != 0;
		handle >>= 1;
		if (flag)
		{
			string className = ReadAMF3String();
			bool externalizable = (handle & 1) != 0;
			handle >>= 1;
			bool dynamic = (handle & 1) != 0;
			handle >>= 1;
			ClassMember[] array = new ClassMember[handle];
			for (int i = 0; i < handle; i++)
			{
				string name = ReadAMF3String();
				ClassMember classMember = (array[i] = new ClassMember(name, BindingFlags.Default, MemberTypes.Custom));
			}
			classDefinition = new ClassDefinition(className, array, externalizable, dynamic);
			AddClassReference(classDefinition);
		}
		else
		{
			classDefinition = ReadClassReference(handle);
		}
		return classDefinition;
	}

	internal object ReadAMF3Object(ClassDefinition classDefinition)
	{
		object obj = null;
		obj = ((classDefinition.ClassName == null || !(classDefinition.ClassName != string.Empty)) ? new ASObject() : ObjectFactory.CreateInstance(classDefinition.ClassName));
		if (obj == null)
		{
			obj = new ASObject(classDefinition.ClassName);
		}
		AddAMF3ObjectReference(obj);
		if (classDefinition.IsExternalizable)
		{
			if (!(obj is IExternalizable))
			{
				string @string = __Res.GetString("Externalizable_CastFail", obj.GetType().FullName);
				throw new AMFException(@string);
			}
			IExternalizable externalizable = obj as IExternalizable;
			DataInput input = new DataInput(this);
			externalizable.ReadExternal(input);
		}
		else
		{
			for (int i = 0; i < classDefinition.MemberCount; i++)
			{
				string name = classDefinition.Members[i].Name;
				object value = ReadAMF3Data();
				SetMember(obj, name, value);
			}
			if (classDefinition.IsDynamic)
			{
				string text = ReadAMF3String();
				while (text != null && text != string.Empty)
				{
					object value2 = ReadAMF3Data();
					SetMember(obj, text, value2);
					text = ReadAMF3String();
				}
			}
		}
		return obj;
	}

	public object ReadAMF3Object()
	{
		int num = ReadAMF3IntegerData();
		bool flag = (num & 1) != 0;
		num >>= 1;
		if (!flag)
		{
			return ReadAMF3ObjectReference(num);
		}
		ClassDefinition classDefinition = ReadClassDefinition(num);
		return ReadAMF3Object(classDefinition);
	}

	internal void SetMember(object instance, string memberName, object value)
	{
		if (instance is ASObject)
		{
			((ASObject)instance)[memberName] = value;
			return;
		}
		Type type = instance.GetType();
		PropertyInfo propertyInfo = null;
		try
		{
			propertyInfo = type.GetProperty(memberName);
		}
		catch (AmbiguousMatchException)
		{
			propertyInfo = type.GetProperty(memberName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
		}
		if (propertyInfo != null)
		{
			try
			{
				value = ((!(propertyInfo.PropertyType == typeof(TimeSpan)) && (!(propertyInfo.PropertyType == typeof(TimeSpan?)) || value == null)) ? TypeHelper.ChangeType(value, propertyInfo.PropertyType) : ((object)Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToTimeSpan(value)));
				if (propertyInfo.CanWrite)
				{
					if (propertyInfo.GetIndexParameters() == null || propertyInfo.GetIndexParameters().Length == 0)
					{
						propertyInfo.SetValue(instance, value, null);
					}
					else
					{
						string @string = __Res.GetString("Reflection_PropertyIndexFail", $"{type.FullName}.{memberName}");
						if (!_faultTolerancy)
						{
							throw new AMFException(@string);
						}
						_lastError = new AMFException(@string);
					}
				}
				else
				{
					string string2 = __Res.GetString("Reflection_PropertyReadOnly", $"{type.FullName}.{memberName}");
				}
				return;
			}
			catch (Exception ex2)
			{
				string string3 = __Res.GetString("Reflection_PropertySetFail", $"{type.FullName}.{memberName}", ex2.Message);
				if (!_faultTolerancy)
				{
					throw new AMFException(string3);
				}
				_lastError = new AMFException(string3);
				return;
			}
		}
		FieldInfo field = type.GetField(memberName, BindingFlags.Instance | BindingFlags.Public);
		try
		{
			if (field != null)
			{
				value = ((!(field.FieldType == typeof(TimeSpan))) ? TypeHelper.ChangeType(value, field.FieldType) : ((object)Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToTimeSpan(value)));
				field.SetValue(instance, value);
			}
			else
			{
				string string4 = __Res.GetString("Reflection_MemberNotFound", $"{type.FullName}.{memberName}");
			}
		}
		catch (Exception ex3)
		{
			string string5 = __Res.GetString("Reflection_FieldSetFail", $"{type.FullName}.{memberName}", ex3.Message);
			if (!_faultTolerancy)
			{
				throw new AMFException(string5);
			}
			_lastError = new AMFException(string5);
		}
	}
}
