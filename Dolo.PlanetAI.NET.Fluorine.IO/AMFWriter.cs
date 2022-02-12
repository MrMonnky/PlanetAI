using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Dolo.PlanetAI.NET.Fluorine.AMF3;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.IO.Writers;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFWriter
{
	private bool _useLegacyCollection = true;

	private Dictionary<object, int> _amf0ObjectReferences;

	private Dictionary<object, int> _objectReferences;

	private Dictionary<object, int> _stringReferences;

	private Dictionary<ClassDefinition, int> _classDefinitionReferences;

	private static Dictionary<string, ClassDefinition> classDefinitions;

	private static Dictionary<Type, IAMFWriter>[] AmfWriterTable;

	public BinaryWriter BinaryWriter;

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

	static AMFWriter()
	{
		Dictionary<Type, IAMFWriter> dictionary = new Dictionary<Type, IAMFWriter>();
		AMF0NumberWriter value = new AMF0NumberWriter();
		dictionary.Add(typeof(sbyte), value);
		dictionary.Add(typeof(byte), value);
		dictionary.Add(typeof(short), value);
		dictionary.Add(typeof(ushort), value);
		dictionary.Add(typeof(int), value);
		dictionary.Add(typeof(uint), value);
		dictionary.Add(typeof(long), value);
		dictionary.Add(typeof(ulong), value);
		dictionary.Add(typeof(float), value);
		dictionary.Add(typeof(double), value);
		dictionary.Add(typeof(decimal), value);
		dictionary.Add(typeof(DBNull), new AMF0NullWriter());
		dictionary.Add(typeof(CacheableObject), new AMF0CacheableObjectWriter());
		dictionary.Add(typeof(XmlDocument), new AMF0XmlDocumentWriter());
		dictionary.Add(typeof(RawBinary), new RawBinaryWriter());
		dictionary.Add(typeof(NameObjectCollectionBase), new AMF0NameObjectCollectionWriter());
		dictionary.Add(typeof(XDocument), new AMF0XDocumentWriter());
		dictionary.Add(typeof(XElement), new AMF0XElementWriter());
		dictionary.Add(typeof(Guid), new AMF0GuidWriter());
		dictionary.Add(typeof(string), new AMF0StringWriter());
		dictionary.Add(typeof(bool), new AMF0BooleanWriter());
		dictionary.Add(typeof(Enum), new AMF0EnumWriter());
		dictionary.Add(typeof(char), new AMF0CharWriter());
		dictionary.Add(typeof(DateTime), new AMF0DateTimeWriter());
		dictionary.Add(typeof(Array), new AMF0ArrayWriter());
		dictionary.Add(typeof(ASObject), new AMF0ASObjectWriter());
		Dictionary<Type, IAMFWriter> dictionary2 = new Dictionary<Type, IAMFWriter>();
		AMF3IntWriter value2 = new AMF3IntWriter();
		AMF3DoubleWriter value3 = new AMF3DoubleWriter();
		dictionary2.Add(typeof(sbyte), value2);
		dictionary2.Add(typeof(byte), value2);
		dictionary2.Add(typeof(short), value2);
		dictionary2.Add(typeof(ushort), value2);
		dictionary2.Add(typeof(int), value2);
		dictionary2.Add(typeof(uint), value2);
		dictionary2.Add(typeof(long), value3);
		dictionary2.Add(typeof(ulong), value3);
		dictionary2.Add(typeof(float), value3);
		dictionary2.Add(typeof(double), value3);
		dictionary2.Add(typeof(decimal), value3);
		dictionary2.Add(typeof(DBNull), new AMF3DBNullWriter());
		dictionary2.Add(typeof(CacheableObject), new AMF3CacheableObjectWriter());
		dictionary2.Add(typeof(XmlDocument), new AMF3XmlDocumentWriter());
		dictionary2.Add(typeof(RawBinary), new RawBinaryWriter());
		dictionary2.Add(typeof(NameObjectCollectionBase), new AMF3NameObjectCollectionWriter());
		dictionary2.Add(typeof(XDocument), new AMF3XDocumentWriter());
		dictionary2.Add(typeof(XElement), new AMF3XElementWriter());
		dictionary2.Add(typeof(Guid), new AMF3GuidWriter());
		dictionary2.Add(typeof(string), new AMF3StringWriter());
		dictionary2.Add(typeof(bool), new AMF3BooleanWriter());
		dictionary2.Add(typeof(Enum), new AMF3EnumWriter());
		dictionary2.Add(typeof(char), new AMF3CharWriter());
		dictionary2.Add(typeof(DateTime), new AMF3DateTimeWriter());
		dictionary2.Add(typeof(Array), new AMF3ArrayWriter());
		dictionary2.Add(typeof(ASObject), new AMF3ASObjectWriter());
		dictionary2.Add(typeof(ByteArray), new AMF3ByteArrayWriter());
		dictionary2.Add(typeof(byte[]), new AMF3ByteArrayWriter());
		AmfWriterTable = new Dictionary<Type, IAMFWriter>[4] { dictionary, null, null, dictionary2 };
		classDefinitions = new Dictionary<string, ClassDefinition>();
	}

	public AMFWriter()
	{
		Reset();
	}

	public void SetBinary(Stream stream)
	{
		BinaryWriter = new BinaryWriter(stream);
	}

	internal AMFWriter(AMFWriter writer, Stream stream)
	{
		BinaryWriter = new BinaryWriter(stream);
		_amf0ObjectReferences = writer._amf0ObjectReferences;
		_objectReferences = writer._objectReferences;
		_stringReferences = writer._stringReferences;
		_classDefinitionReferences = writer._classDefinitionReferences;
		_useLegacyCollection = writer._useLegacyCollection;
	}

	public void Reset()
	{
		_amf0ObjectReferences = new Dictionary<object, int>(5);
		_objectReferences = new Dictionary<object, int>(5);
		_stringReferences = new Dictionary<object, int>(5);
		_classDefinitionReferences = new Dictionary<ClassDefinition, int>();
	}

	public void WriteByte(byte value)
	{
		BinaryWriter.BaseStream.WriteByte(value);
	}

	public void WriteBytes(byte[] buffer)
	{
		int num = 0;
		while (buffer != null && num < buffer.Length)
		{
			BinaryWriter.BaseStream.WriteByte(buffer[num]);
			num++;
		}
	}

	public void WriteShort(int value)
	{
		byte[] bytes = BitConverter.GetBytes((ushort)value);
		WriteBigEndian(bytes);
	}

	public void WriteString(string value)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true, throwOnInvalidBytes: true);
		int byteCount = uTF8Encoding.GetByteCount(value);
		if (byteCount < 65536)
		{
			WriteByte(2);
			WriteUTF(value);
		}
		else
		{
			WriteByte(12);
			WriteLongUTF(value);
		}
	}

	public void WriteUTF(string value)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		int byteCount = uTF8Encoding.GetByteCount(value);
		byte[] bytes = uTF8Encoding.GetBytes(value);
		WriteShort(byteCount);
		if (bytes.Length != 0)
		{
			BinaryWriter.BaseStream.Write(bytes);
		}
	}

	public void WriteUTFBytes(string value)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(value);
		if (bytes.Length != 0)
		{
			BinaryWriter.BaseStream.Write(bytes);
		}
	}

	private void WriteLongUTF(string value)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true, throwOnInvalidBytes: true);
		uint byteCount = (uint)uTF8Encoding.GetByteCount(value);
		byte[] array = new byte[byteCount + 4];
		array[0] = (byte)((byteCount >> 24) & 0xFFu);
		array[1] = (byte)((byteCount >> 16) & 0xFFu);
		array[2] = (byte)((byteCount >> 8) & 0xFFu);
		array[3] = (byte)(byteCount & 0xFFu);
		int bytes = uTF8Encoding.GetBytes(value, 0, value.Length, array, 4);
		if (array.Length != 0)
		{
			BinaryWriter.BaseStream.Write(array, 0, array.Length);
		}
	}

	public void WriteData(ObjectEncoding objectEncoding, object data)
	{
		if (data == null)
		{
			WriteNull();
			return;
		}
		Type type = data.GetType();
		if (AMFConfiguration.Instance.AcceptNullValueTypes && AMFConfiguration.Instance.NullableValues != null && AMFConfiguration.Instance.NullableValues.ContainsKey(type) && data.Equals(AMFConfiguration.Instance.NullableValues[type]))
		{
			WriteNull();
			return;
		}
		if (_amf0ObjectReferences.ContainsKey(data))
		{
			WriteReference(data);
			return;
		}
		IAMFWriter iAMFWriter = null;
		if (AmfWriterTable[0].ContainsKey(type))
		{
			iAMFWriter = AmfWriterTable[0][type];
		}
		if (iAMFWriter == null && AmfWriterTable[0].ContainsKey(type.BaseType))
		{
			iAMFWriter = AmfWriterTable[0][type.BaseType];
		}
		if (iAMFWriter == null)
		{
			lock (AmfWriterTable)
			{
				if (!AmfWriterTable[0].ContainsKey(type))
				{
					iAMFWriter = new AMF0ObjectWriter();
					AmfWriterTable[0].Add(type, iAMFWriter);
				}
				else
				{
					iAMFWriter = AmfWriterTable[0][type];
				}
			}
		}
		if (iAMFWriter != null)
		{
			if (objectEncoding == ObjectEncoding.AMF0)
			{
				iAMFWriter.WriteData(this, data);
				return;
			}
			if (iAMFWriter.IsPrimitive)
			{
				iAMFWriter.WriteData(this, data);
				return;
			}
			WriteByte(17);
			WriteAMF3Data(data);
			return;
		}
		string @string = __Res.GetString("TypeSerializer_NotFound", type.FullName);
		throw new AMFException(@string);
	}

	internal void AddReference(object value)
	{
		_amf0ObjectReferences.Add(value, _amf0ObjectReferences.Count);
	}

	internal void WriteReference(object value)
	{
		WriteByte(7);
		WriteShort(_amf0ObjectReferences[value]);
	}

	public void WriteNull()
	{
		WriteByte(5);
	}

	public void WriteDouble(double value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}

	public void WriteFloat(float value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}

	public void WriteInt32(int value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}

	public void WriteUInt24(int value)
	{
		byte[] array = new byte[3]
		{
			(byte)(0xFFu & (uint)(value >> 16)),
			(byte)(0xFFu & (uint)(value >> 8)),
			(byte)(0xFFu & (uint)value)
		};
		BinaryWriter.BaseStream.Write(array, 0, array.Length);
	}

	public void WriteBoolean(bool value)
	{
		BinaryWriter.BaseStream.WriteByte((byte)(value ? 1 : 0));
	}

	public void WriteLong(long value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}

	private void WriteBigEndian(byte[] bytes)
	{
		if (bytes != null)
		{
			for (int num = bytes.Length - 1; num >= 0; num--)
			{
				BinaryWriter.BaseStream.WriteByte(bytes[num]);
			}
		}
	}

	public void WriteDateTime(DateTime value)
	{
		value = value.ToUniversalTime();
		DateTime value2 = new DateTime(1970, 1, 1);
		long num = (long)value.Subtract(value2).TotalMilliseconds;
		WriteDouble(num);
		TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
		if (AMFConfiguration.Instance.TimezoneCompensation == TimezoneCompensation.None)
		{
			WriteShort(0);
		}
		else
		{
			WriteShort((int)(utcOffset.TotalMilliseconds / 60000.0));
		}
	}

	public void WriteXmlDocument(XmlDocument value)
	{
		if (value != null)
		{
			AddReference(value);
			BinaryWriter.BaseStream.WriteByte(15);
			string outerXml = value.DocumentElement!.OuterXml;
			WriteLongUTF(outerXml);
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteXDocument(XDocument xDocument)
	{
		if (xDocument != null)
		{
			AddReference(xDocument);
			BinaryWriter.BaseStream.WriteByte(15);
			string value = xDocument.ToString();
			WriteLongUTF(value);
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteXElement(XElement xElement)
	{
		if (xElement != null)
		{
			AddReference(xElement);
			BinaryWriter.BaseStream.WriteByte(15);
			string value = xElement.ToString();
			WriteLongUTF(value);
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteArray(ObjectEncoding objectEcoding, Array value)
	{
		if (value == null)
		{
			WriteNull();
			return;
		}
		AddReference(value);
		WriteByte(10);
		WriteInt32(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			WriteData(objectEcoding, value.GetValue(i));
		}
	}

	public void WriteAssociativeArray(ObjectEncoding objectEncoding, IDictionary value)
	{
		if (value == null)
		{
			WriteNull();
			return;
		}
		AddReference(value);
		WriteByte(8);
		WriteInt32(value.Count);
		foreach (DictionaryEntry item in value)
		{
			WriteUTF(item.Key.ToString());
			WriteData(objectEncoding, item.Value);
		}
		WriteEndMarkup();
	}

	public void WriteObject(ObjectEncoding objectEncoding, object obj)
	{
		if (obj == null)
		{
			WriteNull();
			return;
		}
		AddReference(obj);
		Type type = obj.GetType();
		WriteByte(16);
		string fullName = type.FullName;
		fullName = AMFConfiguration.Instance.GetCustomClass(fullName);
		WriteUTF(fullName);
		PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
		List<PropertyInfo> list = new List<PropertyInfo>(properties);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			PropertyInfo propertyInfo = list[num];
			if (propertyInfo.GetCustomAttributes(typeof(NonSerializedAttribute), inherit: true).Length != 0)
			{
				list.RemoveAt(num);
			}
			if (propertyInfo.GetCustomAttributes(typeof(TransientAttribute), inherit: true).Length != 0)
			{
				list.RemoveAt(num);
			}
		}
		foreach (PropertyInfo item in list)
		{
			WriteUTF(item.Name);
			object value = item.GetValue(obj, null);
			WriteData(objectEncoding, value);
		}
		FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		List<FieldInfo> list2 = new List<FieldInfo>(fields);
		for (int num2 = list2.Count - 1; num2 >= 0; num2--)
		{
			FieldInfo fieldInfo = list2[num2];
			if (fieldInfo.GetCustomAttributes(typeof(NonSerializedAttribute), inherit: true).Length != 0)
			{
				list2.RemoveAt(num2);
			}
		}
		for (int i = 0; i < list2.Count; i++)
		{
			FieldInfo fieldInfo2 = list2[i];
			WriteUTF(fieldInfo2.Name);
			WriteData(objectEncoding, fieldInfo2.GetValue(obj));
		}
		WriteEndMarkup();
	}

	internal void WriteEndMarkup()
	{
		BinaryWriter.BaseStream.WriteByte(0);
		BinaryWriter.BaseStream.WriteByte(0);
		BinaryWriter.BaseStream.WriteByte(9);
	}

	public void WriteASO(ObjectEncoding objectEncoding, ASObject asObject)
	{
		if (asObject != null)
		{
			AddReference(asObject);
			if (asObject.TypeName == null)
			{
				BinaryWriter.BaseStream.WriteByte(3);
			}
			else
			{
				BinaryWriter.BaseStream.WriteByte(16);
				WriteUTF(asObject.TypeName);
			}
			foreach (KeyValuePair<string, object> item in asObject)
			{
				WriteUTF(item.Key.ToString());
				WriteData(objectEncoding, item.Value);
			}
			WriteEndMarkup();
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteAMF3Data(object data)
	{
		if (data == null)
		{
			WriteAMF3Null();
			return;
		}
		if (data is DBNull)
		{
			WriteAMF3Null();
			return;
		}
		Type type = data.GetType();
		IAMFWriter iAMFWriter = null;
		if (AmfWriterTable[3].ContainsKey(type))
		{
			iAMFWriter = AmfWriterTable[3][type];
		}
		if (iAMFWriter == null && type.BaseType != null && AmfWriterTable[3].ContainsKey(type.BaseType))
		{
			iAMFWriter = AmfWriterTable[3][type.BaseType];
		}
		if (iAMFWriter == null)
		{
			lock (AmfWriterTable)
			{
				if (!AmfWriterTable[3].ContainsKey(type))
				{
					iAMFWriter = new AMF3ObjectWriter();
					AmfWriterTable[3].Add(type, iAMFWriter);
				}
				else
				{
					iAMFWriter = AmfWriterTable[3][type];
				}
			}
		}
		if (iAMFWriter != null)
		{
			iAMFWriter.WriteData(this, data);
		}
		else
		{
			string text = $"Could not find serializer for type {type.FullName}";
		}
	}

	public void WriteAMF3Null()
	{
		WriteByte(1);
	}

	public void WriteAMF3Bool(bool value)
	{
		WriteByte((byte)(value ? 3 : 2));
	}

	public void WriteAMF3Array(Array value)
	{
		if (_amf0ObjectReferences.ContainsKey(value))
		{
			WriteReference(value);
		}
		else if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			int length = value.Length;
			length <<= 1;
			length |= 1;
			WriteAMF3IntegerData(length);
			WriteAMF3UTF(string.Empty);
			for (int i = 0; i < value.Length; i++)
			{
				WriteAMF3Data(value.GetValue(i));
			}
		}
		else
		{
			int num = _objectReferences[value];
			num <<= 1;
			WriteAMF3IntegerData(num);
		}
	}

	public void WriteAMF3Array(IList value)
	{
		if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			int count = value.Count;
			count <<= 1;
			count |= 1;
			WriteAMF3IntegerData(count);
			WriteAMF3UTF(string.Empty);
			for (int i = 0; i < value.Count; i++)
			{
				WriteAMF3Data(value[i]);
			}
		}
		else
		{
			int num = _objectReferences[value];
			num <<= 1;
			WriteAMF3IntegerData(num);
		}
	}

	public void WriteAMF3AssociativeArray(IDictionary value)
	{
		if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			WriteAMF3IntegerData(1);
			foreach (DictionaryEntry item in value)
			{
				WriteAMF3UTF(item.Key.ToString());
				WriteAMF3Data(item.Value);
			}
			WriteAMF3UTF(string.Empty);
		}
		else
		{
			int num = _objectReferences[value];
			num <<= 1;
			WriteAMF3IntegerData(num);
		}
	}

	internal void WriteByteArray(ByteArray byteArray)
	{
		_objectReferences.Add(byteArray, _objectReferences.Count);
		WriteByte(12);
		int length = (int)byteArray.Length;
		length <<= 1;
		length |= 1;
		WriteAMF3IntegerData(length);
		WriteBytes(byteArray.MemoryStream.ToArray());
	}

	public void WriteAMF3UTF(string value)
	{
		if (value == string.Empty)
		{
			WriteAMF3IntegerData(1);
		}
		else if (!_stringReferences.ContainsKey(value))
		{
			_stringReferences.Add(value, _stringReferences.Count);
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			int byteCount = uTF8Encoding.GetByteCount(value);
			int num = byteCount;
			num <<= 1;
			num |= 1;
			WriteAMF3IntegerData(num);
			byte[] bytes = uTF8Encoding.GetBytes(value);
			if (bytes.Length != 0)
			{
				BinaryWriter.BaseStream.Write(bytes);
			}
		}
		else
		{
			int num2 = _stringReferences[value];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	public void WriteAMF3String(string value)
	{
		WriteByte(6);
		WriteAMF3UTF(value);
	}

	public void WriteAMF3DateTime(DateTime value)
	{
		if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			int value2 = 1;
			WriteAMF3IntegerData(value2);
			DateTime value3 = new DateTime(1970, 1, 1, 0, 0, 0);
			value = value.ToUniversalTime();
			long num = (long)value.Subtract(value3).TotalMilliseconds;
			WriteDouble(num);
		}
		else
		{
			int num2 = _objectReferences[value];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	private void WriteAMF3IntegerData(int value)
	{
		value &= 0x1FFFFFFF;
		if (value < 128)
		{
			WriteByte((byte)value);
		}
		else if (value < 16384)
		{
			WriteByte((byte)(((uint)(value >> 7) & 0x7Fu) | 0x80u));
			WriteByte((byte)((uint)value & 0x7Fu));
		}
		else if (value < 2097152)
		{
			WriteByte((byte)(((uint)(value >> 14) & 0x7Fu) | 0x80u));
			WriteByte((byte)(((uint)(value >> 7) & 0x7Fu) | 0x80u));
			WriteByte((byte)((uint)value & 0x7Fu));
		}
		else
		{
			WriteByte((byte)(((uint)(value >> 22) & 0x7Fu) | 0x80u));
			WriteByte((byte)(((uint)(value >> 15) & 0x7Fu) | 0x80u));
			WriteByte((byte)(((uint)(value >> 8) & 0x7Fu) | 0x80u));
			WriteByte((byte)((uint)value & 0xFFu));
		}
	}

	public void WriteAMF3Int(int value)
	{
		if (value >= -268435456 && value <= 268435455)
		{
			WriteByte(4);
			WriteAMF3IntegerData(value);
		}
		else
		{
			WriteAMF3Double(value);
		}
	}

	public void WriteAMF3Double(double value)
	{
		WriteByte(5);
		WriteDouble(value);
	}

	public void WriteAMF3XmlDocument(XmlDocument value)
	{
		WriteByte(11);
		string text = string.Empty;
		if (value.DocumentElement != null && value.DocumentElement!.OuterXml != null)
		{
			text = value.DocumentElement!.OuterXml;
		}
		if (text == string.Empty)
		{
			WriteAMF3IntegerData(1);
		}
		else if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			int byteCount = uTF8Encoding.GetByteCount(text);
			int num = byteCount;
			num <<= 1;
			num |= 1;
			WriteAMF3IntegerData(num);
			byte[] bytes = uTF8Encoding.GetBytes(text);
			if (bytes.Length != 0)
			{
				BinaryWriter.BaseStream.Write(bytes);
			}
		}
		else
		{
			int num2 = _objectReferences[value];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	public void WriteAMF3XDocument(XDocument xDocument)
	{
		WriteByte(11);
		string text = string.Empty;
		if (xDocument != null)
		{
			text = xDocument.ToString();
		}
		if (text == string.Empty)
		{
			WriteAMF3IntegerData(1);
		}
		else if (!_objectReferences.ContainsKey(text))
		{
			_objectReferences.Add(text, _objectReferences.Count);
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			int byteCount = uTF8Encoding.GetByteCount(text);
			int num = byteCount;
			num <<= 1;
			num |= 1;
			WriteAMF3IntegerData(num);
			byte[] bytes = uTF8Encoding.GetBytes(text);
			if (bytes.Length != 0)
			{
				BinaryWriter.BaseStream.Write(bytes);
			}
		}
		else
		{
			int num2 = _objectReferences[text];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	public void WriteAMF3XElement(XElement xElement)
	{
		WriteByte(11);
		string text = string.Empty;
		if (xElement != null)
		{
			text = xElement.ToString();
		}
		if (text == string.Empty)
		{
			WriteAMF3IntegerData(1);
		}
		else if (!_objectReferences.ContainsKey(text))
		{
			_objectReferences.Add(text, _objectReferences.Count);
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			int byteCount = uTF8Encoding.GetByteCount(text);
			int num = byteCount;
			num <<= 1;
			num |= 1;
			WriteAMF3IntegerData(num);
			byte[] bytes = uTF8Encoding.GetBytes(text);
			if (bytes.Length != 0)
			{
				BinaryWriter.BaseStream.Write(bytes);
			}
		}
		else
		{
			int num2 = _objectReferences[text];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	public void WriteAMF3Object(object value)
	{
		if (!_objectReferences.ContainsKey(value))
		{
			_objectReferences.Add(value, _objectReferences.Count);
			ClassDefinition classDefinition = GetClassDefinition(value);
			if (classDefinition != null && _classDefinitionReferences.ContainsKey(classDefinition))
			{
				int num = _classDefinitionReferences[classDefinition];
				num <<= 2;
				num |= 1;
				WriteAMF3IntegerData(num);
			}
			else
			{
				classDefinition = CreateClassDefinition(value);
				_classDefinitionReferences.Add(classDefinition, _classDefinitionReferences.Count);
				int memberCount = classDefinition.MemberCount;
				memberCount <<= 1;
				memberCount |= (classDefinition.IsDynamic ? 1 : 0);
				memberCount <<= 1;
				memberCount |= (classDefinition.IsExternalizable ? 1 : 0);
				memberCount <<= 2;
				memberCount |= 3;
				WriteAMF3IntegerData(memberCount);
				WriteAMF3UTF(classDefinition.ClassName);
				for (int i = 0; i < classDefinition.MemberCount; i++)
				{
					string name = classDefinition.Members[i].Name;
					WriteAMF3UTF(name);
				}
			}
			if (classDefinition.IsExternalizable)
			{
				if (value is IExternalizable)
				{
					IExternalizable externalizable = value as IExternalizable;
					DataOutput output = new DataOutput(this);
					externalizable.WriteExternal(output);
					return;
				}
				throw new AMFException(__Res.GetString("Externalizable_CastFail", classDefinition.ClassName));
			}
			for (int j = 0; j < classDefinition.MemberCount; j++)
			{
				object member = GetMember(value, classDefinition.Members[j]);
				WriteAMF3Data(member);
			}
			if (!classDefinition.IsDynamic)
			{
				return;
			}
			IDictionary dictionary = value as IDictionary;
			foreach (DictionaryEntry item in dictionary)
			{
				WriteAMF3UTF(item.Key.ToString());
				WriteAMF3Data(item.Value);
			}
			WriteAMF3UTF(string.Empty);
		}
		else
		{
			int num2 = _objectReferences[value];
			num2 <<= 1;
			WriteAMF3IntegerData(num2);
		}
	}

	private ClassDefinition GetClassDefinition(object obj)
	{
		if (obj is ASObject)
		{
			ASObject aSObject = obj as ASObject;
			if (aSObject.IsTypedObject && classDefinitions.ContainsKey(aSObject.TypeName))
			{
				return classDefinitions[aSObject.TypeName];
			}
			return null;
		}
		if (classDefinitions.ContainsKey(obj.GetType().FullName))
		{
			return classDefinitions[obj.GetType().FullName];
		}
		return null;
	}

	private ClassDefinition CreateClassDefinition(object obj)
	{
		ClassDefinition classDefinition = null;
		Type type = obj.GetType();
		bool externalizable = type.GetInterface(typeof(IExternalizable).FullName, ignoreCase: true) != null;
		bool dynamic = false;
		string text = null;
		if (obj is IDictionary)
		{
			if (obj is ASObject && (obj as ASObject).IsTypedObject)
			{
				ASObject aSObject = obj as ASObject;
				ClassMember[] array = new ClassMember[aSObject.Count];
				int num = 0;
				foreach (KeyValuePair<string, object> item3 in aSObject)
				{
					ClassMember classMember = (array[num] = new ClassMember(item3.Key, BindingFlags.Default, MemberTypes.Custom));
					num++;
				}
				text = aSObject.TypeName;
				classDefinition = new ClassDefinition(text, array, externalizable, dynamic);
				classDefinitions[text] = classDefinition;
			}
			else
			{
				dynamic = true;
				text = string.Empty;
				classDefinition = new ClassDefinition(text, ClassDefinition.EmptyClassMembers, externalizable, dynamic);
			}
		}
		else if (obj is IExternalizable)
		{
			text = type.FullName;
			text = AMFConfiguration.Instance.GetCustomClass(text);
			classDefinition = new ClassDefinition(text, ClassDefinition.EmptyClassMembers, externalizable: true, dynamic: false);
			classDefinitions[type.FullName] = classDefinition;
		}
		else
		{
			List<string> list = new List<string>();
			List<ClassMember> list2 = new List<ClassMember>();
			PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				if (propertyInfo.GetCustomAttributes(typeof(TransientAttribute), inherit: true).Length != 0)
				{
					continue;
				}
				if (propertyInfo.GetGetMethod() == null || propertyInfo.GetGetMethod()!.GetParameters().Length != 0)
				{
					string @string = __Res.GetString("Reflection_PropertyIndexFail", $"{type.FullName}.{propertyInfo.Name}");
				}
				else if (!list.Contains(name))
				{
					list.Add(name);
					BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
					try
					{
						PropertyInfo property = obj.GetType().GetProperty(name);
					}
					catch (AmbiguousMatchException)
					{
						bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
					}
					ClassMember item = new ClassMember(name, bindingFlags, propertyInfo.MemberType);
					list2.Add(item);
				}
			}
			FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.GetCustomAttributes(typeof(NonSerializedAttribute), inherit: true).Length == 0 && fieldInfo.GetCustomAttributes(typeof(TransientAttribute), inherit: true).Length == 0)
				{
					string name2 = fieldInfo.Name;
					ClassMember item2 = new ClassMember(name2, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, fieldInfo.MemberType);
					list2.Add(item2);
				}
			}
			ClassMember[] members = list2.ToArray();
			text = type.FullName;
			text = AMFConfiguration.Instance.GetCustomClass(text);
			classDefinition = new ClassDefinition(text, members, externalizable, dynamic);
			classDefinitions[type.FullName] = classDefinition;
		}
		return classDefinition;
	}

	internal object GetMember(object instance, ClassMember member)
	{
		if (instance is ASObject)
		{
			ASObject aSObject = instance as ASObject;
			if (aSObject.ContainsKey(member.Name))
			{
				return aSObject[member.Name];
			}
		}
		Type type = instance.GetType();
		if (member.MemberType == MemberTypes.Property)
		{
			PropertyInfo property = type.GetProperty(member.Name, member.BindingFlags);
			return property.GetValue(instance, null);
		}
		if (member.MemberType == MemberTypes.Field)
		{
			FieldInfo field = type.GetField(member.Name, member.BindingFlags);
			return field.GetValue(instance);
		}
		string @string = __Res.GetString("Reflection_MemberNotFound", $"{type.FullName}.{member.Name}");
		throw new AMFException(@string);
	}
}
