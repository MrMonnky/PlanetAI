using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine;

internal sealed class TypeHelper
{
	private static object _syncLock;

	private static sbyte _defaultSByteNullValue;

	private static short _defaultInt16NullValue;

	private static int _defaultInt32NullValue;

	private static long _defaultInt64NullValue;

	private static byte _defaultByteNullValue;

	private static ushort _defaultUInt16NullValue;

	private static uint _defaultUInt32NullValue;

	private static ulong _defaultUInt64NullValue;

	private static char _defaultCharNullValue;

	private static float _defaultSingleNullValue;

	private static double _defaultDoubleNullValue;

	private static bool _defaultBooleanNullValue;

	private static string _defaultStringNullValue;

	private static DateTime _defaultDateTimeNullValue;

	private static decimal _defaultDecimalNullValue;

	private static Guid _defaultGuidNullValue;

	private static XmlReader _defaultXmlReaderNullValue;

	private static XmlDocument _defaultXmlDocumentNullValue;

	private static XDocument _defaultXDocumentNullValue;

	private static XElement _defaultXElementNullValue;

	static TypeHelper()
	{
		_syncLock = new object();
		_defaultSByteNullValue = (sbyte)GetNullValue(typeof(sbyte));
		_defaultInt16NullValue = (short)GetNullValue(typeof(short));
		_defaultInt32NullValue = (int)GetNullValue(typeof(int));
		_defaultInt64NullValue = (long)GetNullValue(typeof(long));
		_defaultByteNullValue = (byte)GetNullValue(typeof(byte));
		_defaultUInt16NullValue = (ushort)GetNullValue(typeof(ushort));
		_defaultUInt32NullValue = (uint)GetNullValue(typeof(uint));
		_defaultUInt64NullValue = (ulong)GetNullValue(typeof(ulong));
		_defaultCharNullValue = (char)GetNullValue(typeof(char));
		_defaultSingleNullValue = (float)GetNullValue(typeof(float));
		_defaultDoubleNullValue = (double)GetNullValue(typeof(double));
		_defaultBooleanNullValue = (bool)GetNullValue(typeof(bool));
		_defaultStringNullValue = (string)GetNullValue(typeof(string));
		_defaultDateTimeNullValue = (DateTime)GetNullValue(typeof(DateTime));
		_defaultDecimalNullValue = (decimal)GetNullValue(typeof(decimal));
		_defaultGuidNullValue = (Guid)GetNullValue(typeof(Guid));
		_defaultXmlReaderNullValue = (XmlReader)GetNullValue(typeof(XmlReader));
		_defaultXmlDocumentNullValue = (XmlDocument)GetNullValue(typeof(XmlDocument));
		_defaultXDocumentNullValue = (XDocument)GetNullValue(typeof(XDocument));
		_defaultXElementNullValue = (XElement)GetNullValue(typeof(XElement));
		_Init();
	}

	internal static void _Init()
	{
	}

	public static Assembly[] GetAssemblies()
	{
		return AppDomain.CurrentDomain.GetAssemblies();
	}

	public static Type Locate(string typeName)
	{
		if (typeName == null || typeName == string.Empty)
		{
			return null;
		}
		Assembly[] assemblies = GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			Type type = assembly.GetType(typeName, throwOnError: false);
			if (type != null)
			{
				return type;
			}
		}
		return null;
	}

	public static Type LocateInLac(string typeName, string lac)
	{
		return null;
	}

	public static Type[] SearchAllTypes(string lac, Hashtable excludedBaseTypes)
	{
		ArrayList arrayList = new ArrayList();
		string[] files = Directory.GetFiles(lac, "*.dll");
		foreach (string assemblyFile in files)
		{
			try
			{
				Assembly assembly = Assembly.LoadFrom(assemblyFile);
				if (assembly == Assembly.GetExecutingAssembly())
				{
					continue;
				}
				Type[] types = assembly.GetTypes();
				foreach (Type type in types)
				{
					if (excludedBaseTypes == null || (!excludedBaseTypes.ContainsKey(type) && (!(type.BaseType != null) || !excludedBaseTypes.ContainsKey(type.BaseType))))
					{
						arrayList.Add(type);
					}
				}
			}
			catch (Exception)
			{
			}
		}
		return (Type[])arrayList.ToArray(typeof(Type));
	}

	public static bool SkipMethod(MethodInfo methodInfo)
	{
		if (methodInfo.ReturnType == typeof(IAsyncResult))
		{
			return true;
		}
		ParameterInfo[] parameters = methodInfo.GetParameters();
		foreach (ParameterInfo parameterInfo in parameters)
		{
			if (parameterInfo.ParameterType == typeof(IAsyncResult))
			{
				return true;
			}
		}
		return false;
	}

	public static string GetDescription(Type type)
	{
		Attribute attribute = ReflectionUtils.GetAttribute(typeof(DescriptionAttribute), type, inherit: false);
		if (attribute != null)
		{
			return (attribute as DescriptionAttribute).Description;
		}
		return null;
	}

	public static string GetDescription(MethodInfo methodInfo)
	{
		Attribute attribute = ReflectionUtils.GetAttribute(typeof(DescriptionAttribute), methodInfo, inherit: false);
		if (attribute != null)
		{
			return (attribute as DescriptionAttribute).Description;
		}
		return null;
	}

	internal static void NarrowValues(object[] values, ParameterInfo[] parameterInfos)
	{
		int num = 0;
		while (values != null && num < values.Length)
		{
			object value = values[num];
			values[num] = ChangeType(value, parameterInfos[num].ParameterType);
			num++;
		}
	}

	internal static object GetNullValue(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (AMFConfiguration.Instance.NullableValues != null && AMFConfiguration.Instance.NullableValues.ContainsKey(type))
		{
			return AMFConfiguration.Instance.NullableValues[type];
		}
		if (type.IsValueType)
		{
			if (type.IsPrimitive)
			{
				if (type == typeof(int))
				{
					return 0;
				}
				if (type == typeof(double))
				{
					return 0.0;
				}
				if (type == typeof(short))
				{
					return (short)0;
				}
				if (type == typeof(bool))
				{
					return false;
				}
				if (type == typeof(sbyte))
				{
					return (sbyte)0;
				}
				if (type == typeof(long))
				{
					return 0L;
				}
				if (type == typeof(byte))
				{
					return (byte)0;
				}
				if (type == typeof(ushort))
				{
					return (ushort)0;
				}
				if (type == typeof(uint))
				{
					return 0u;
				}
				if (type == typeof(ulong))
				{
					return 0uL;
				}
				if (type == typeof(float))
				{
					return 0f;
				}
				if (type == typeof(char))
				{
					return '\0';
				}
			}
			else
			{
				if (type == typeof(DateTime))
				{
					return DateTime.MinValue;
				}
				if (type == typeof(decimal))
				{
					return 0m;
				}
				if (type == typeof(Guid))
				{
					return Guid.Empty;
				}
			}
		}
		else
		{
			if (type == typeof(string))
			{
				return null;
			}
			if (type == typeof(DBNull))
			{
				return DBNull.Value;
			}
		}
		return null;
	}

	internal static object CreateInstance(Type type)
	{
		if (ReflectionUtils.IsGenericType(type))
		{
			Type genericTypeDefinition = ReflectionUtils.GetGenericTypeDefinition(type);
			Type[] genericArguments = ReflectionUtils.GetGenericArguments(type);
			Type type2 = ReflectionUtils.MakeGenericType(genericTypeDefinition, genericArguments);
			return Activator.CreateInstance(type2);
		}
		return Activator.CreateInstance(type);
	}

	public static string[] GetLacLocations()
	{
		ArrayList arrayList = new ArrayList();
		try
		{
			try
			{
				string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				if (directoryName != null)
				{
					arrayList.Add(directoryName);
				}
			}
			catch (SecurityException)
			{
			}
			try
			{
				if (AppDomain.CurrentDomain.DynamicDirectory != null)
				{
					string directoryName2 = Path.GetDirectoryName(AppDomain.CurrentDomain.DynamicDirectory);
					arrayList.Add(directoryName2);
				}
			}
			catch (SecurityException)
			{
			}
		}
		catch (Exception)
		{
		}
		return arrayList.ToArray(typeof(string)) as string[];
	}

	public static bool GetTypeIsAccessible(Type type)
	{
		if (type == null || type.Assembly == typeof(TypeHelper).Assembly)
		{
			return false;
		}
		return true;
	}

	public static Type GetUnderlyingType(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (ReflectionUtils.IsNullable(type))
		{
			type = type.GetGenericArguments()[0];
		}
		if (type.IsEnum)
		{
			type = Enum.GetUnderlyingType(type);
		}
		return type;
	}

	public static string GetCSharpName(Type type)
	{
		int num = 0;
		while (type.IsArray)
		{
			type = type.GetElementType();
			num++;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(type.Namespace);
		stringBuilder.Append(".");
		Type[] parameters = Type.EmptyTypes;
		if (ReflectionUtils.IsGenericType(type) && ReflectionUtils.GetGenericArguments(type) != null)
		{
			parameters = ReflectionUtils.GetGenericArguments(type);
		}
		GetCSharpName(type, parameters, 0, stringBuilder);
		for (int i = 0; i < num; i++)
		{
			stringBuilder.Append("[]");
		}
		return stringBuilder.ToString();
	}

	private static int GetCSharpName(Type type, Type[] parameters, int index, StringBuilder sb)
	{
		if (type.DeclaringType != null && type.DeclaringType != type)
		{
			index = GetCSharpName(type.DeclaringType, parameters, index, sb);
			sb.Append(".");
		}
		string name = type.Name;
		int num = name.IndexOf('`');
		if (num < 0)
		{
			num = name.IndexOf('!');
		}
		if (num > 0)
		{
			sb.Append(name.Substring(0, num));
			sb.Append("<");
			int num2 = int.Parse(name.Substring(num + 1), CultureInfo.InvariantCulture) + index;
			while (index < num2)
			{
				sb.Append(GetCSharpName(parameters[index]));
				if (index < num2 - 1)
				{
					sb.Append(",");
				}
				index++;
			}
			sb.Append(">");
			return index;
		}
		sb.Append(name);
		return index;
	}

	public static bool IsAssignable(object obj, Type targetType)
	{
		return IsAssignable(obj, targetType, ReflectionUtils.IsNullable(targetType));
	}

	private static bool IsAssignable(object obj, Type targetType, bool isNullable)
	{
		if (obj != null && targetType.IsAssignableFrom(obj.GetType()))
		{
			return true;
		}
		if (isNullable && obj == null)
		{
			return true;
		}
		if (targetType.IsArray)
		{
			if (obj == null)
			{
				return true;
			}
			Type type = obj.GetType();
			if (type == targetType)
			{
				return true;
			}
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				Type elementType2 = targetType.GetElementType();
				if (elementType.IsArray != elementType2.IsArray || (elementType.IsArray && elementType.GetArrayRank() != elementType2.GetArrayRank()))
				{
					return false;
				}
				Array array = (Array)obj;
				int rank = array.Rank;
				if (rank == 1 && array.GetLowerBound(0) == 0)
				{
					int length = array.Length;
					if (elementType2.IsAssignableFrom(elementType))
					{
						return true;
					}
					for (int i = 0; i < length; i++)
					{
						if (!IsAssignable(array.GetValue(i), elementType2))
						{
							return false;
						}
					}
				}
				else
				{
					int num = 1;
					int[] array2 = new int[rank];
					int[] array3 = new int[rank];
					int[] array4 = new int[rank];
					for (int j = 0; j < rank; j++)
					{
						num *= (array2[j] = array.GetLength(j));
						array4[j] = array.GetLowerBound(j);
					}
					for (int k = 0; k < num; k++)
					{
						int num2 = k;
						for (int num3 = rank - 1; num3 >= 0; num3--)
						{
							array3[num3] = num2 % array2[num3] + array4[num3];
							num2 /= array2[num3];
						}
						if (!IsAssignable(array.GetValue(array3), elementType2))
						{
							return false;
						}
					}
				}
				return true;
			}
		}
		else if (targetType.IsEnum)
		{
			try
			{
				Enum.Parse(targetType, obj.ToString(), ignoreCase: true);
				return true;
			}
			catch (ArgumentException)
			{
				return false;
			}
		}
		if (obj != null)
		{
			TypeConverter typeConverter = ReflectionUtils.GetTypeConverter(obj);
			if (typeConverter != null && typeConverter.CanConvertTo(targetType))
			{
				return true;
			}
			typeConverter = ReflectionUtils.GetTypeConverter(targetType);
			if (typeConverter != null && typeConverter.CanConvertFrom(obj.GetType()))
			{
				return true;
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.Generic.ICollection`1") && obj is IList)
			{
				Type[] genericArguments = ReflectionUtils.GetGenericArguments(targetType);
				if (genericArguments != null && genericArguments.Length == 1)
				{
					Type @interface = targetType.GetInterface("System.Collections.Generic.ICollection`1", ignoreCase: true);
					return @interface != null;
				}
				return false;
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.IList") && obj is IList)
			{
				return true;
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.Generic.IDictionary`2") && obj is IDictionary)
			{
				Type[] genericArguments2 = ReflectionUtils.GetGenericArguments(targetType);
				if (genericArguments2 != null && genericArguments2.Length == 2)
				{
					Type interface2 = targetType.GetInterface("System.Collections.Generic.IDictionary`2", ignoreCase: true);
					return interface2 != null;
				}
				return false;
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.IDictionary") && obj is IDictionary)
			{
				return true;
			}
			try
			{
				if (isNullable)
				{
					TypeCode typeCode = Type.GetTypeCode(GetUnderlyingType(targetType));
					TypeCode typeCode2 = typeCode;
					if (typeCode2 == TypeCode.Char)
					{
						return CanConvertToNullableChar(obj);
					}
				}
				TypeCode typeCode3 = Type.GetTypeCode(targetType);
				TypeCode typeCode4 = typeCode3;
				if (typeCode4 == TypeCode.Char)
				{
					return CanConvertToChar(obj);
				}
			}
			catch (InvalidCastException)
			{
			}
			if (typeof(XDocument) == targetType && obj is XmlDocument)
			{
				return true;
			}
			if (typeof(XElement) == targetType && obj is XmlDocument)
			{
				return true;
			}
			return false;
		}
		if (targetType.IsValueType)
		{
			if (AMFConfiguration.Instance.AcceptNullValueTypes)
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public static object ChangeType(object value, Type targetType)
	{
		return ConvertChangeType(value, targetType, ReflectionUtils.IsNullable(targetType));
	}

	private static object ConvertChangeType(object value, Type targetType, bool isNullable)
	{
		if (targetType.IsArray)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == targetType)
			{
				return value;
			}
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				Type elementType2 = targetType.GetElementType();
				if (elementType.IsArray != elementType2.IsArray || (elementType.IsArray && elementType.GetArrayRank() != elementType2.GetArrayRank()))
				{
					throw new InvalidCastException($"Can not convert array of type '{type.FullName}' to array of '{targetType.FullName}'.");
				}
				Array array = (Array)value;
				int rank = array.Rank;
				Array array2;
				if (rank == 1 && array.GetLowerBound(0) == 0)
				{
					int length = array.Length;
					array2 = Array.CreateInstance(elementType2, length);
					if (elementType2.IsAssignableFrom(elementType))
					{
						Array.Copy(array, array2, length);
					}
					else
					{
						for (int i = 0; i < length; i++)
						{
							array2.SetValue(ConvertChangeType(array.GetValue(i), elementType2, isNullable), i);
						}
					}
				}
				else
				{
					int num = 1;
					int[] array3 = new int[rank];
					int[] array4 = new int[rank];
					int[] array5 = new int[rank];
					for (int j = 0; j < rank; j++)
					{
						num *= (array3[j] = array.GetLength(j));
						array5[j] = array.GetLowerBound(j);
					}
					array2 = Array.CreateInstance(elementType2, array3, array5);
					for (int k = 0; k < num; k++)
					{
						int num2 = k;
						for (int num3 = rank - 1; num3 >= 0; num3--)
						{
							array4[num3] = num2 % array3[num3] + array5[num3];
							num2 /= array3[num3];
						}
						array2.SetValue(ConvertChangeType(array.GetValue(array4), elementType2, isNullable), array4);
					}
				}
				return array2;
			}
		}
		else if (targetType.IsEnum)
		{
			try
			{
				return Enum.Parse(targetType, value.ToString(), ignoreCase: true);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidCastException(__Res.GetString("TypeHelper_ConversionFail"), innerException);
			}
		}
		if (isNullable)
		{
			switch (Type.GetTypeCode(GetUnderlyingType(targetType)))
			{
			case TypeCode.Boolean:
				return ConvertToNullableBoolean(value);
			case TypeCode.Byte:
				return ConvertToNullableByte(value);
			case TypeCode.Char:
				return ConvertToNullableChar(value);
			case TypeCode.DateTime:
				return ConvertToNullableDateTime(value);
			case TypeCode.Decimal:
				return ConvertToNullableDecimal(value);
			case TypeCode.Double:
				return ConvertToNullableDouble(value);
			case TypeCode.Int16:
				return ConvertToNullableInt16(value);
			case TypeCode.Int32:
				return ConvertToNullableInt32(value);
			case TypeCode.Int64:
				return ConvertToNullableInt64(value);
			case TypeCode.SByte:
				return ConvertToNullableSByte(value);
			case TypeCode.Single:
				return ConvertToNullableSingle(value);
			case TypeCode.UInt16:
				return ConvertToNullableUInt16(value);
			case TypeCode.UInt32:
				return ConvertToNullableUInt32(value);
			case TypeCode.UInt64:
				return ConvertToNullableUInt64(value);
			}
			if (typeof(Guid) == targetType)
			{
				return ConvertToNullableGuid(value);
			}
		}
		switch (Type.GetTypeCode(targetType))
		{
		case TypeCode.Boolean:
			return ConvertToBoolean(value);
		case TypeCode.Byte:
			return ConvertToByte(value);
		case TypeCode.Char:
			return ConvertToChar(value);
		case TypeCode.DateTime:
			return ConvertToDateTime(value);
		case TypeCode.Decimal:
			return ConvertToDecimal(value);
		case TypeCode.Double:
			return ConvertToDouble(value);
		case TypeCode.Int16:
			return ConvertToInt16(value);
		case TypeCode.Int32:
			return ConvertToInt32(value);
		case TypeCode.Int64:
			return ConvertToInt64(value);
		case TypeCode.SByte:
			return ConvertToSByte(value);
		case TypeCode.Single:
			return ConvertToSingle(value);
		case TypeCode.String:
			return ConvertToString(value);
		case TypeCode.UInt16:
			return ConvertToUInt16(value);
		case TypeCode.UInt32:
			return ConvertToUInt32(value);
		case TypeCode.UInt64:
			return ConvertToUInt64(value);
		default:
		{
			if (typeof(Guid) == targetType)
			{
				return ConvertToGuid(value);
			}
			if (typeof(XmlDocument) == targetType)
			{
				return ConvertToXmlDocument(value);
			}
			if (typeof(XDocument) == targetType)
			{
				return ConvertToXDocument(value);
			}
			if (typeof(XElement) == targetType)
			{
				return ConvertToXElement(value);
			}
			if (typeof(byte[]) == targetType)
			{
				return ConvertToByteArray(value);
			}
			if (typeof(char[]) == targetType)
			{
				return ConvertToCharArray(value);
			}
			if (value == null)
			{
				return null;
			}
			if (targetType.IsAssignableFrom(value.GetType()))
			{
				return value;
			}
			TypeConverter typeConverter = ReflectionUtils.GetTypeConverter(targetType);
			if (typeConverter != null && typeConverter.CanConvertFrom(value.GetType()))
			{
				return typeConverter.ConvertFrom(value);
			}
			typeConverter = ReflectionUtils.GetTypeConverter(value);
			if (typeConverter != null && typeConverter.CanConvertTo(targetType))
			{
				return typeConverter.ConvertTo(value, targetType);
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.Generic.ICollection`1") && value is IList)
			{
				object obj = CreateInstance(targetType);
				if (obj != null)
				{
					Type[] genericArguments = ReflectionUtils.GetGenericArguments(targetType);
					if (genericArguments != null && genericArguments.Length == 1)
					{
						Type @interface = targetType.GetInterface("System.Collections.Generic.ICollection`1", ignoreCase: true);
						MethodInfo method = targetType.GetMethod("Add");
						IList list = value as IList;
						for (int l = 0; l < (value as IList).Count; l++)
						{
							method.Invoke(obj, new object[1] { ChangeType(list[l], genericArguments[0]) });
						}
					}
					return obj;
				}
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.IList") && value is IList)
			{
				object obj2 = CreateInstance(targetType);
				if (obj2 != null)
				{
					IList list2 = value as IList;
					IList list3 = obj2 as IList;
					for (int m = 0; m < list2.Count; m++)
					{
						list3.Add(list2[m]);
					}
					return obj2;
				}
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.Generic.IDictionary`2") && value is IDictionary)
			{
				object obj3 = CreateInstance(targetType);
				if (obj3 != null)
				{
					IDictionary dictionary = value as IDictionary;
					Type[] genericArguments2 = ReflectionUtils.GetGenericArguments(targetType);
					if (genericArguments2 != null && genericArguments2.Length == 2)
					{
						Type interface2 = targetType.GetInterface("System.Collections.Generic.IDictionary`2", ignoreCase: true);
						MethodInfo method2 = targetType.GetMethod("Add");
						IDictionary dictionary2 = value as IDictionary;
						foreach (DictionaryEntry item in dictionary2)
						{
							method2.Invoke(obj3, new object[2]
							{
								ChangeType(item.Key, genericArguments2[0]),
								ChangeType(item.Value, genericArguments2[1])
							});
						}
					}
					return obj3;
				}
			}
			if (ReflectionUtils.ImplementsInterface(targetType, "System.Collections.IDictionary") && value is IDictionary)
			{
				object obj4 = CreateInstance(targetType);
				if (obj4 != null)
				{
					IDictionary dictionary3 = value as IDictionary;
					IDictionary dictionary4 = obj4 as IDictionary;
					foreach (DictionaryEntry item2 in dictionary3)
					{
						dictionary4.Add(item2.Key, item2.Value);
					}
					return obj4;
				}
			}
			return System.Convert.ChangeType(value, targetType, null);
		}
		}
	}

	public static sbyte? ConvertToNullableSByte(object value)
	{
		if (value is sbyte)
		{
			return (sbyte?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableSByte(value);
	}

	public static short? ConvertToNullableInt16(object value)
	{
		if (value is short)
		{
			return (short?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableInt16(value);
	}

	public static int? ConvertToNullableInt32(object value)
	{
		if (value is int)
		{
			return (int?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableInt32(value);
	}

	public static long? ConvertToNullableInt64(object value)
	{
		if (value is long)
		{
			return (long?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableInt64(value);
	}

	public static byte? ConvertToNullableByte(object value)
	{
		if (value is byte)
		{
			return (byte?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableByte(value);
	}

	public static ushort? ConvertToNullableUInt16(object value)
	{
		if (value is ushort)
		{
			return (ushort?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableUInt16(value);
	}

	public static uint? ConvertToNullableUInt32(object value)
	{
		if (value is uint)
		{
			return (uint?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableUInt32(value);
	}

	public static ulong? ConvertToNullableUInt64(object value)
	{
		if (value is ulong)
		{
			return (ulong?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableUInt64(value);
	}

	public static char? ConvertToNullableChar(object value)
	{
		if (value is char)
		{
			return (char?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableChar(value);
	}

	public static bool CanConvertToNullableChar(object value)
	{
		if (value is char)
		{
			return true;
		}
		if (value == null)
		{
			return true;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.CanConvertToNullableChar(value);
	}

	public static double? ConvertToNullableDouble(object value)
	{
		if (value is double)
		{
			return (double?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableDouble(value);
	}

	public static float? ConvertToNullableSingle(object value)
	{
		if (value is float)
		{
			return (float?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableSingle(value);
	}

	public static bool? ConvertToNullableBoolean(object value)
	{
		if (value is bool)
		{
			return (bool?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableBoolean(value);
	}

	public static DateTime? ConvertToNullableDateTime(object value)
	{
		if (value is DateTime)
		{
			return (DateTime?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableDateTime(value);
	}

	public static decimal? ConvertToNullableDecimal(object value)
	{
		if (value is decimal)
		{
			return (decimal?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableDecimal(value);
	}

	public static Guid? ConvertToNullableGuid(object value)
	{
		if (value is Guid)
		{
			return (Guid?)value;
		}
		if (value == null)
		{
			return null;
		}
		return Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToNullableGuid(value);
	}

	public static sbyte ConvertToSByte(object value)
	{
		return (value is sbyte) ? ((sbyte)value) : ((value == null) ? _defaultSByteNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToSByte(value));
	}

	public static short ConvertToInt16(object value)
	{
		return (value is short) ? ((short)value) : ((value == null) ? _defaultInt16NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToInt16(value));
	}

	public static int ConvertToInt32(object value)
	{
		return (value is int) ? ((int)value) : ((value == null) ? _defaultInt32NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToInt32(value));
	}

	public static long ConvertToInt64(object value)
	{
		return (value is long) ? ((long)value) : ((value == null) ? _defaultInt64NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToInt64(value));
	}

	public static byte ConvertToByte(object value)
	{
		return (value is byte) ? ((byte)value) : ((value == null) ? _defaultByteNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToByte(value));
	}

	public static ushort ConvertToUInt16(object value)
	{
		return (value is ushort) ? ((ushort)value) : ((value == null) ? _defaultUInt16NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToUInt16(value));
	}

	public static uint ConvertToUInt32(object value)
	{
		return (value is uint) ? ((uint)value) : ((value == null) ? _defaultUInt32NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToUInt32(value));
	}

	public static ulong ConvertToUInt64(object value)
	{
		return (value is ulong) ? ((ulong)value) : ((value == null) ? _defaultUInt64NullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToUInt64(value));
	}

	public static char ConvertToChar(object value)
	{
		return (value is char) ? ((char)value) : ((value == null) ? _defaultCharNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToChar(value));
	}

	public static bool CanConvertToChar(object value)
	{
		return value is char || value == null || Dolo.PlanetAI.NET.Fluorine.Util.Convert.CanConvertToChar(value);
	}

	public static float ConvertToSingle(object value)
	{
		return (value is float) ? ((float)value) : ((value == null) ? _defaultSingleNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToSingle(value));
	}

	public static double ConvertToDouble(object value)
	{
		return (value is double) ? ((double)value) : ((value == null) ? _defaultDoubleNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToDouble(value));
	}

	public static bool ConvertToBoolean(object value)
	{
		return (value is bool) ? ((bool)value) : ((value == null) ? _defaultBooleanNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToBoolean(value));
	}

	public static string ConvertToString(object value)
	{
		return (value is string) ? ((string)value) : ((value == null) ? _defaultStringNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToString(value));
	}

	public static DateTime ConvertToDateTime(object value)
	{
		return (value is DateTime) ? ((DateTime)value) : ((value == null) ? _defaultDateTimeNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToDateTime(value));
	}

	public static decimal ConvertToDecimal(object value)
	{
		return (value is decimal) ? ((decimal)value) : ((value == null) ? _defaultDecimalNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToDecimal(value));
	}

	public static Guid ConvertToGuid(object value)
	{
		return (value is Guid) ? ((Guid)value) : ((value == null) ? _defaultGuidNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToGuid(value));
	}

	public static XmlReader ConvertToXmlReader(object value)
	{
		return (value is XmlReader) ? ((XmlReader)value) : ((value == null) ? _defaultXmlReaderNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToXmlReader(value));
	}

	public static XmlDocument ConvertToXmlDocument(object value)
	{
		return (value is XmlDocument) ? ((XmlDocument)value) : ((value == null) ? _defaultXmlDocumentNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToXmlDocument(value));
	}

	public static XDocument ConvertToXDocument(object value)
	{
		return (value is XDocument) ? ((XDocument)value) : ((value == null) ? _defaultXDocumentNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToXDocument(value));
	}

	public static XElement ConvertToXElement(object value)
	{
		return (value is XElement) ? ((XElement)value) : ((value == null) ? _defaultXElementNullValue : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToXElement(value));
	}

	public static byte[] ConvertToByteArray(object value)
	{
		return (value is byte[]) ? ((byte[])value) : ((value == null) ? null : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToByteArray(value));
	}

	public static char[] ConvertToCharArray(object value)
	{
		return (value is char[]) ? ((char[])value) : ((value == null) ? null : Dolo.PlanetAI.NET.Fluorine.Util.Convert.ToCharArray(value));
	}
}
