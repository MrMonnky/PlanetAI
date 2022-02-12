using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal class Convert
{
	public static string ToString(sbyte value)
	{
		return value.ToString();
	}

	public static string ToString(short value)
	{
		return value.ToString();
	}

	public static string ToString(int value)
	{
		return value.ToString();
	}

	public static string ToString(long value)
	{
		return value.ToString();
	}

	public static string ToString(byte value)
	{
		return value.ToString();
	}

	public static string ToString(ushort value)
	{
		return value.ToString();
	}

	public static string ToString(uint value)
	{
		return value.ToString();
	}

	public static string ToString(ulong value)
	{
		return value.ToString();
	}

	public static string ToString(float value)
	{
		return value.ToString();
	}

	public static string ToString(double value)
	{
		return value.ToString();
	}

	public static string ToString(bool value)
	{
		return value.ToString();
	}

	public static string ToString(decimal value)
	{
		return value.ToString();
	}

	public static string ToString(char value)
	{
		return value.ToString();
	}

	public static string ToString(TimeSpan value)
	{
		return value.ToString();
	}

	public static string ToString(DateTime value)
	{
		return value.ToString();
	}

	public static string ToString(Guid value)
	{
		return value.ToString();
	}

	public static string ToString(sbyte? value)
	{
		return value.ToString();
	}

	public static string ToString(short? value)
	{
		return value.ToString();
	}

	public static string ToString(int? value)
	{
		return value.ToString();
	}

	public static string ToString(long? value)
	{
		return value.ToString();
	}

	public static string ToString(byte? value)
	{
		return value.ToString();
	}

	public static string ToString(ushort? value)
	{
		return value.ToString();
	}

	public static string ToString(uint? value)
	{
		return value.ToString();
	}

	public static string ToString(ulong? value)
	{
		return value.ToString();
	}

	public static string ToString(float? value)
	{
		return value.ToString();
	}

	public static string ToString(double? value)
	{
		return value.ToString();
	}

	public static string ToString(bool? value)
	{
		return value.ToString();
	}

	public static string ToString(decimal? value)
	{
		return value.ToString();
	}

	public static string ToString(char? value)
	{
		return value.ToString();
	}

	public static string ToString(TimeSpan? value)
	{
		return value.ToString();
	}

	public static string ToString(DateTime? value)
	{
		return value.ToString();
	}

	public static string ToString(Guid? value)
	{
		return value.ToString();
	}

	public static string ToString(Type value)
	{
		return (value == null) ? null : value.FullName;
	}

	public static string ToString(XmlDocument value)
	{
		return value?.InnerXml;
	}

	public static string ToString(object value)
	{
		if (value == null || value is DBNull)
		{
			return string.Empty;
		}
		if (value is string)
		{
			return (string)value;
		}
		if (value is char)
		{
			return ToString((char)value);
		}
		if (value is TimeSpan)
		{
			return ToString((TimeSpan)value);
		}
		if (value is DateTime)
		{
			return ToString((DateTime)value);
		}
		if (value is Guid)
		{
			return ToString((Guid)value);
		}
		if (value is XmlDocument)
		{
			return ToString((XmlDocument)value);
		}
		if (value is Type)
		{
			return ToString((Type)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToString(null);
		}
		if (value is IFormattable)
		{
			return ((IFormattable)value).ToString(null, null);
		}
		return value.ToString();
	}

	public static bool CanConvertToChar(object value)
	{
		if (value == null || value is DBNull)
		{
			return true;
		}
		if (value is char)
		{
			return true;
		}
		if (value is string)
		{
			return value == null || (value as string).Length == 1;
		}
		if (value is bool)
		{
			return true;
		}
		if (value is IConvertible)
		{
			try
			{
				((IConvertible)value).ToChar(null);
				return true;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}
		return false;
	}

	public static bool CanConvertToNullableChar(object value)
	{
		if (value == null || value is DBNull)
		{
			return true;
		}
		if (value is char)
		{
			return true;
		}
		if (value is string)
		{
			return value == null || (value as string).Length == 1;
		}
		if (value is bool)
		{
			return true;
		}
		if (value is IConvertible)
		{
			try
			{
				((IConvertible)value).ToChar(null);
				return true;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}
		return false;
	}

	public static sbyte ToSByte(string value)
	{
		return (sbyte)((value != null) ? sbyte.Parse(value) : 0);
	}

	public static sbyte ToSByte(short value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(int value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(long value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(byte value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(ushort value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(uint value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(ulong value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(float value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(double value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(decimal value)
	{
		return (sbyte)value;
	}

	public static sbyte ToSByte(bool value)
	{
		return (sbyte)(value ? 1 : 0);
	}

	public static sbyte ToSByte(char value)
	{
		return checked((sbyte)value);
	}

	public static sbyte ToSByte(sbyte? value)
	{
		return (sbyte)(value.HasValue ? value.Value : 0);
	}

	public static sbyte ToSByte(short? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(int? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(long? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(byte? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(ushort? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(uint? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(ulong? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(float? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(double? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(decimal? value)
	{
		return (sbyte)(value.HasValue ? ((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(char? value)
	{
		return (sbyte)(value.HasValue ? checked((sbyte)value.Value) : 0);
	}

	public static sbyte ToSByte(bool? value)
	{
		return (sbyte)((value.HasValue && value.Value) ? 1 : 0);
	}

	public static sbyte ToSByte(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0;
		}
		if (!(value is sbyte result))
		{
			if (value is string)
			{
				return ToSByte((string)value);
			}
			if (value is bool)
			{
				return ToSByte((bool)value);
			}
			if (value is char)
			{
				return ToSByte((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(sbyte));
		}
		return result;
	}

	public static bool ToBoolean(string value)
	{
		return value != null && bool.Parse(value);
	}

	public static bool ToBoolean(sbyte value)
	{
		return value != 0;
	}

	public static bool ToBoolean(short value)
	{
		return value != 0;
	}

	public static bool ToBoolean(int value)
	{
		return value != 0;
	}

	public static bool ToBoolean(long value)
	{
		return value != 0;
	}

	public static bool ToBoolean(byte value)
	{
		return value != 0;
	}

	public static bool ToBoolean(ushort value)
	{
		return value != 0;
	}

	public static bool ToBoolean(uint value)
	{
		return value != 0;
	}

	public static bool ToBoolean(ulong value)
	{
		return value != 0;
	}

	public static bool ToBoolean(float value)
	{
		return value != 0f;
	}

	public static bool ToBoolean(double value)
	{
		return value != 0.0;
	}

	public static bool ToBoolean(decimal value)
	{
		return value != 0m;
	}

	public static bool ToBoolean(char value)
	{
		return value switch
		{
			'\0' => false, 
			'0' => false, 
			'n' => false, 
			'N' => false, 
			'f' => false, 
			'F' => false, 
			'\u0001' => true, 
			'1' => true, 
			'y' => true, 
			'Y' => true, 
			't' => true, 
			'T' => true, 
			_ => throw new InvalidCastException($"Invalid cast from {typeof(char).FullName} to {typeof(bool).FullName}"), 
		};
	}

	public static bool ToBoolean(bool? value)
	{
		return value.HasValue && value.Value;
	}

	public static bool ToBoolean(sbyte? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(short? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(int? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(long? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(byte? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(ushort? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(uint? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(ulong? value)
	{
		return value.HasValue && value.Value != 0;
	}

	public static bool ToBoolean(float? value)
	{
		return value.HasValue && value.Value != 0f;
	}

	public static bool ToBoolean(double? value)
	{
		return value.HasValue && value.Value != 0.0;
	}

	public static bool ToBoolean(decimal? value)
	{
		return value.HasValue && value.Value != 0m;
	}

	public static bool ToBoolean(char? value)
	{
		return value.HasValue && ToBoolean(value.Value);
	}

	public static bool ToBoolean(object value)
	{
		if (value == null || value is DBNull)
		{
			return false;
		}
		if (!(value is bool result))
		{
			if (value is string)
			{
				return ToBoolean((string)value);
			}
			if (value is char)
			{
				return ToBoolean((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToBoolean(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(bool));
		}
		return result;
	}

	public static byte ToByte(string value)
	{
		return (byte)((value != null) ? byte.Parse(value) : 0);
	}

	public static byte ToByte(sbyte value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(short value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(int value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(long value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(ushort value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(uint value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(ulong value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(float value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(double value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(decimal value)
	{
		return (byte)value;
	}

	public static byte ToByte(bool value)
	{
		return (byte)(value ? 1u : 0u);
	}

	public static byte ToByte(char value)
	{
		return checked((byte)value);
	}

	public static byte ToByte(byte? value)
	{
		return (byte)(value.HasValue ? value.Value : 0);
	}

	public static byte ToByte(sbyte? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(short? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(int? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(long? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(ushort? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(uint? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(ulong? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(float? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(double? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(decimal? value)
	{
		return (byte)(value.HasValue ? ((byte)value.Value) : 0);
	}

	public static byte ToByte(char? value)
	{
		return (byte)(value.HasValue ? checked((byte)value.Value) : 0);
	}

	public static byte ToByte(bool? value)
	{
		return (byte)((value.HasValue && value.Value) ? 1 : 0);
	}

	public static byte ToByte(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0;
		}
		if (!(value is byte result))
		{
			if (value is string)
			{
				return ToByte((string)value);
			}
			if (value is bool)
			{
				return ToByte((bool)value);
			}
			if (value is char)
			{
				return ToByte((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToByte(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(byte));
		}
		return result;
	}

	public static byte[] ToByteArray(string p)
	{
		return (p == null) ? null : Encoding.UTF8.GetBytes(p);
	}

	public static byte[] ToByteArray(sbyte p)
	{
		return new byte[1] { checked((byte)p) };
	}

	public static byte[] ToByteArray(short p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(int p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(long p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(byte p)
	{
		return new byte[1] { p };
	}

	public static byte[] ToByteArray(ushort p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(uint p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(ulong p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(char p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(float p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(double p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(bool p)
	{
		return BitConverter.GetBytes(p);
	}

	public static byte[] ToByteArray(DateTime p)
	{
		return ToByteArray(p.ToBinary());
	}

	public static byte[] ToByteArray(TimeSpan p)
	{
		return ToByteArray(p.Ticks);
	}

	public static byte[] ToByteArray(Guid p)
	{
		return (p == Guid.Empty) ? null : p.ToByteArray();
	}

	public static byte[] ToByteArray(decimal p)
	{
		int[] bits = decimal.GetBits(p);
		byte[] array = new byte[bits.Length << 2];
		for (int i = 0; i < bits.Length; i++)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(bits[i]), 0, array, i * 4, 4);
		}
		return array;
	}

	public static byte[] ToByteArray(Stream p)
	{
		if (p == null)
		{
			return null;
		}
		if (p is MemoryStream)
		{
			return ((MemoryStream)p).ToArray();
		}
		long position = p.Seek(0L, SeekOrigin.Begin);
		byte[] array = new byte[p.Length];
		p.Read(array, 0, array.Length);
		p.Position = position;
		return array;
	}

	public static byte[] ToByteArray(ByteArray p)
	{
		return p?.GetBuffer();
	}

	public static byte[] ToByteArray(Guid? p)
	{
		return p.HasValue ? p.Value.ToByteArray() : null;
	}

	public static byte[] ToByteArray(sbyte? p)
	{
		return (!p.HasValue) ? null : new byte[1] { checked((byte)p.Value) };
	}

	public static byte[] ToByteArray(short? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(int? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(long? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(byte? p)
	{
		return (!p.HasValue) ? null : new byte[1] { p.Value };
	}

	public static byte[] ToByteArray(ushort? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(uint? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(ulong? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(char? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(float? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(double? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(bool? p)
	{
		return p.HasValue ? BitConverter.GetBytes(p.Value) : null;
	}

	public static byte[] ToByteArray(DateTime? p)
	{
		return p.HasValue ? ToByteArray(p.Value.ToBinary()) : null;
	}

	public static byte[] ToByteArray(TimeSpan? p)
	{
		return p.HasValue ? ToByteArray(p.Value.Ticks) : null;
	}

	public static byte[] ToByteArray(decimal? p)
	{
		return p.HasValue ? ToByteArray(p.Value) : null;
	}

	public static byte[] ToByteArray(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is byte[])
		{
			return (byte[])p;
		}
		if (p is string)
		{
			return ToByteArray((string)p);
		}
		if (p is sbyte)
		{
			return ToByteArray((sbyte)p);
		}
		if (p is short)
		{
			return ToByteArray((short)p);
		}
		if (p is int)
		{
			return ToByteArray((int)p);
		}
		if (p is long)
		{
			return ToByteArray((long)p);
		}
		if (p is byte)
		{
			return ToByteArray((byte)p);
		}
		if (p is ushort)
		{
			return ToByteArray((ushort)p);
		}
		if (p is uint)
		{
			return ToByteArray((uint)p);
		}
		if (p is ulong)
		{
			return ToByteArray((ulong)p);
		}
		if (p is char)
		{
			return ToByteArray((char)p);
		}
		if (p is float)
		{
			return ToByteArray((float)p);
		}
		if (p is double)
		{
			return ToByteArray((double)p);
		}
		if (p is bool)
		{
			return ToByteArray((bool)p);
		}
		if (p is decimal)
		{
			return ToByteArray((decimal)p);
		}
		if (p is DateTime)
		{
			return ToByteArray((DateTime)p);
		}
		if (p is TimeSpan)
		{
			return ToByteArray((TimeSpan)p);
		}
		if (p is Stream)
		{
			return ToByteArray((Stream)p);
		}
		if (p is Guid)
		{
			return ToByteArray((Guid)p);
		}
		if (p is ByteArray)
		{
			return ToByteArray((ByteArray)p);
		}
		throw CreateInvalidCastException(p.GetType(), typeof(byte[]));
	}

	public static char ToChar(string value)
	{
		if (char.TryParse(value, out var result))
		{
			return result;
		}
		return '\0';
	}

	public static char ToChar(sbyte value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(short value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(int value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(long value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(byte value)
	{
		return (char)value;
	}

	public static char ToChar(ushort value)
	{
		return (char)value;
	}

	public static char ToChar(uint value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(ulong value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(float value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(double value)
	{
		return (char)checked((ushort)value);
	}

	public static char ToChar(decimal value)
	{
		return (char)value;
	}

	public static char ToChar(bool value)
	{
		return (char)(value ? 1u : 0u);
	}

	public static char ToChar(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static char ToChar(sbyte? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(short? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(int? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(long? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(byte? value)
	{
		return (char)(value.HasValue ? value.Value : 0);
	}

	public static char ToChar(ushort? value)
	{
		return (char)(value.HasValue ? value.Value : 0);
	}

	public static char ToChar(uint? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(ulong? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(float? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(double? value)
	{
		return (char)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static char ToChar(decimal? value)
	{
		return value.HasValue ? ((char)value.Value) : '\0';
	}

	public static char ToChar(bool? value)
	{
		return (value.HasValue && value.Value) ? '\u0001' : '\0';
	}

	public static char ToChar(object value)
	{
		if (value == null || value is DBNull)
		{
			return '\0';
		}
		if (!(value is char result))
		{
			if (value is string)
			{
				return ToChar((string)value);
			}
			if (value is bool)
			{
				return ToChar((bool)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToChar(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(char));
		}
		return result;
	}

	public static char[] ToCharArray(string p)
	{
		return p?.ToCharArray();
	}

	public static char[] ToCharArray(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is char[])
		{
			return (char[])p;
		}
		if (p is string)
		{
			return ToCharArray((string)p);
		}
		return ToString(p).ToCharArray();
	}

	public static DateTime ToDateTime(string value)
	{
		return (value == null) ? DateTime.MinValue : DateTime.Parse(value);
	}

	public static DateTime ToDateTime(TimeSpan value)
	{
		return DateTime.MinValue + value;
	}

	public static DateTime ToDateTime(long value)
	{
		return DateTime.MinValue + TimeSpan.FromTicks(value);
	}

	public static DateTime ToDateTime(double value)
	{
		return DateTime.MinValue + TimeSpan.FromDays(value);
	}

	public static DateTime ToDateTime(DateTime? value)
	{
		return value.HasValue ? value.Value : DateTime.MinValue;
	}

	public static DateTime ToDateTime(TimeSpan? value)
	{
		return value.HasValue ? (DateTime.MinValue + value.Value) : DateTime.MinValue;
	}

	public static DateTime ToDateTime(long? value)
	{
		return value.HasValue ? (DateTime.MinValue + TimeSpan.FromTicks(value.Value)) : DateTime.MinValue;
	}

	public static DateTime ToDateTime(double? value)
	{
		return value.HasValue ? (DateTime.MinValue + TimeSpan.FromDays(value.Value)) : DateTime.MinValue;
	}

	public static DateTime ToDateTime(object value)
	{
		if (value == null || value is DBNull)
		{
			return DateTime.MinValue;
		}
		if (!(value is DateTime result))
		{
			if (value is string)
			{
				return ToDateTime((string)value);
			}
			if (value is TimeSpan)
			{
				return ToDateTime((TimeSpan)value);
			}
			if (value is long)
			{
				return ToDateTime((long)value);
			}
			if (value is double)
			{
				return ToDateTime((double)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(DateTime));
		}
		return result;
	}

	public static decimal ToDecimal(string value)
	{
		return (value == null) ? 0.0m : decimal.Parse(value);
	}

	public static decimal ToDecimal(sbyte value)
	{
		return value;
	}

	public static decimal ToDecimal(short value)
	{
		return value;
	}

	public static decimal ToDecimal(int value)
	{
		return value;
	}

	public static decimal ToDecimal(long value)
	{
		return value;
	}

	public static decimal ToDecimal(byte value)
	{
		return value;
	}

	public static decimal ToDecimal(ushort value)
	{
		return value;
	}

	public static decimal ToDecimal(uint value)
	{
		return value;
	}

	public static decimal ToDecimal(ulong value)
	{
		return value;
	}

	public static decimal ToDecimal(float value)
	{
		return (decimal)value;
	}

	public static decimal ToDecimal(double value)
	{
		return (decimal)value;
	}

	public static decimal ToDecimal(bool value)
	{
		return value ? 1.0m : 0.0m;
	}

	public static decimal ToDecimal(char value)
	{
		return value;
	}

	public static decimal ToDecimal(decimal? value)
	{
		return value.HasValue ? value.Value : 0.0m;
	}

	public static decimal ToDecimal(sbyte? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(short? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(int? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(long? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(byte? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(ushort? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(uint? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(ulong? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(float? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(double? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(char? value)
	{
		return value.HasValue ? ((decimal)value.Value) : 0.0m;
	}

	public static decimal ToDecimal(bool? value)
	{
		return (value.HasValue && value.Value) ? 1.0m : 0.0m;
	}

	public static decimal ToDecimal(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0.0m;
		}
		if (!(value is decimal result))
		{
			if (value is string)
			{
				return ToDecimal((string)value);
			}
			if (value is bool)
			{
				return ToDecimal((bool)value);
			}
			if (value is char)
			{
				return ToDecimal((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(decimal));
		}
		return result;
	}

	public static double ToDouble(string value)
	{
		return (value == null) ? 0.0 : double.Parse(value);
	}

	public static double ToDouble(sbyte value)
	{
		return value;
	}

	public static double ToDouble(short value)
	{
		return value;
	}

	public static double ToDouble(int value)
	{
		return value;
	}

	public static double ToDouble(long value)
	{
		return value;
	}

	public static double ToDouble(byte value)
	{
		return (int)value;
	}

	public static double ToDouble(ushort value)
	{
		return (int)value;
	}

	public static double ToDouble(uint value)
	{
		return value;
	}

	public static double ToDouble(ulong value)
	{
		return value;
	}

	public static double ToDouble(float value)
	{
		return value;
	}

	public static double ToDouble(decimal value)
	{
		return (double)value;
	}

	public static double ToDouble(bool value)
	{
		return value ? 1.0 : 0.0;
	}

	public static double ToDouble(char value)
	{
		return (int)value;
	}

	public static double ToDouble(DateTime value)
	{
		return (value - DateTime.MinValue).TotalDays;
	}

	public static double ToDouble(TimeSpan value)
	{
		return value.TotalDays;
	}

	public static double ToDouble(double? value)
	{
		return value.HasValue ? value.Value : 0.0;
	}

	public static double ToDouble(sbyte? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(short? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(int? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(long? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(byte? value)
	{
		return value.HasValue ? ((double)(int)value.Value) : 0.0;
	}

	public static double ToDouble(ushort? value)
	{
		return value.HasValue ? ((double)(int)value.Value) : 0.0;
	}

	public static double ToDouble(uint? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(ulong? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(float? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(decimal? value)
	{
		return value.HasValue ? ((double)value.Value) : 0.0;
	}

	public static double ToDouble(char? value)
	{
		return value.HasValue ? ((double)(int)value.Value) : 0.0;
	}

	public static double ToDouble(bool? value)
	{
		return (value.HasValue && value.Value) ? 1.0 : 0.0;
	}

	public static double ToDouble(DateTime? value)
	{
		return value.HasValue ? (value.Value - DateTime.MinValue).TotalDays : 0.0;
	}

	public static double ToDouble(TimeSpan? value)
	{
		return value.HasValue ? value.Value.TotalDays : 0.0;
	}

	public static double ToDouble(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0.0;
		}
		if (!(value is double result))
		{
			if (value is string)
			{
				return ToDouble((string)value);
			}
			if (value is bool)
			{
				return ToDouble((bool)value);
			}
			if (value is char)
			{
				return ToDouble((char)value);
			}
			if (value is DateTime)
			{
				return ToDouble((DateTime)value);
			}
			if (value is TimeSpan)
			{
				return ToDouble((TimeSpan)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(double));
		}
		return result;
	}

	public static short ToInt16(string value)
	{
		return (short)((value != null) ? short.Parse(value) : 0);
	}

	public static short ToInt16(sbyte value)
	{
		return value;
	}

	public static short ToInt16(int value)
	{
		return checked((short)value);
	}

	public static short ToInt16(long value)
	{
		return checked((short)value);
	}

	public static short ToInt16(byte value)
	{
		return value;
	}

	public static short ToInt16(ushort value)
	{
		return checked((short)value);
	}

	public static short ToInt16(uint value)
	{
		return checked((short)value);
	}

	public static short ToInt16(ulong value)
	{
		return checked((short)value);
	}

	public static short ToInt16(float value)
	{
		return checked((short)value);
	}

	public static short ToInt16(double value)
	{
		return checked((short)value);
	}

	public static short ToInt16(decimal value)
	{
		return (short)value;
	}

	public static short ToInt16(bool value)
	{
		return (short)(value ? 1 : 0);
	}

	public static short ToInt16(char value)
	{
		return checked((short)value);
	}

	public static short ToInt16(short? value)
	{
		return (short)(value.HasValue ? value.Value : 0);
	}

	public static short ToInt16(sbyte? value)
	{
		return (short)(value.HasValue ? value.Value : 0);
	}

	public static short ToInt16(int? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(long? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(byte? value)
	{
		return (short)(value.HasValue ? value.Value : 0);
	}

	public static short ToInt16(ushort? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(uint? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(ulong? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(float? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(double? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(decimal? value)
	{
		return (short)(value.HasValue ? ((short)value.Value) : 0);
	}

	public static short ToInt16(char? value)
	{
		return (short)(value.HasValue ? checked((short)value.Value) : 0);
	}

	public static short ToInt16(bool? value)
	{
		return (short)((value.HasValue && value.Value) ? 1 : 0);
	}

	public static short ToInt16(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0;
		}
		if (!(value is short result))
		{
			if (value is string)
			{
				return ToInt16((string)value);
			}
			if (value is bool)
			{
				return ToInt16((bool)value);
			}
			if (value is char)
			{
				return ToInt16((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(short));
		}
		return result;
	}

	public static int ToInt32(string value)
	{
		return (value != null) ? int.Parse(value) : 0;
	}

	public static int ToInt32(sbyte value)
	{
		return value;
	}

	public static int ToInt32(short value)
	{
		return value;
	}

	public static int ToInt32(long value)
	{
		return checked((int)value);
	}

	public static int ToInt32(byte value)
	{
		return value;
	}

	public static int ToInt32(ushort value)
	{
		return value;
	}

	public static int ToInt32(uint value)
	{
		return checked((int)value);
	}

	public static int ToInt32(ulong value)
	{
		return checked((int)value);
	}

	public static int ToInt32(float value)
	{
		return checked((int)value);
	}

	public static int ToInt32(double value)
	{
		return checked((int)value);
	}

	public static int ToInt32(decimal value)
	{
		return (int)value;
	}

	public static int ToInt32(bool value)
	{
		return value ? 1 : 0;
	}

	public static int ToInt32(char value)
	{
		return value;
	}

	public static int ToInt32(int? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static int ToInt32(sbyte? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static int ToInt32(short? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static int ToInt32(long? value)
	{
		return value.HasValue ? checked((int)value.Value) : 0;
	}

	public static int ToInt32(byte? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static int ToInt32(ushort? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static int ToInt32(uint? value)
	{
		return value.HasValue ? checked((int)value.Value) : 0;
	}

	public static int ToInt32(ulong? value)
	{
		return value.HasValue ? checked((int)value.Value) : 0;
	}

	public static int ToInt32(float? value)
	{
		return value.HasValue ? checked((int)value.Value) : 0;
	}

	public static int ToInt32(double? value)
	{
		return value.HasValue ? checked((int)value.Value) : 0;
	}

	public static int ToInt32(decimal? value)
	{
		return value.HasValue ? ((int)value.Value) : 0;
	}

	public static int ToInt32(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static int ToInt32(bool? value)
	{
		return (value.HasValue && value.Value) ? 1 : 0;
	}

	public static int ToInt32(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0;
		}
		if (!(value is int result))
		{
			if (value is string)
			{
				return ToInt32((string)value);
			}
			if (value is bool)
			{
				return ToInt32((bool)value);
			}
			if (value is char)
			{
				return ToInt32((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(int));
		}
		return result;
	}

	public static long ToInt64(string value)
	{
		return (value == null) ? 0 : long.Parse(value);
	}

	public static long ToInt64(sbyte value)
	{
		return value;
	}

	public static long ToInt64(short value)
	{
		return value;
	}

	public static long ToInt64(int value)
	{
		return value;
	}

	public static long ToInt64(byte value)
	{
		return value;
	}

	public static long ToInt64(ushort value)
	{
		return value;
	}

	public static long ToInt64(uint value)
	{
		return value;
	}

	public static long ToInt64(ulong value)
	{
		return checked((long)value);
	}

	public static long ToInt64(float value)
	{
		return checked((long)value);
	}

	public static long ToInt64(double value)
	{
		return checked((long)value);
	}

	public static long ToInt64(decimal value)
	{
		return (long)value;
	}

	public static long ToInt64(char value)
	{
		return value;
	}

	public static long ToInt64(bool value)
	{
		return value ? 1 : 0;
	}

	public static long ToInt64(DateTime value)
	{
		return (value - DateTime.MinValue).Ticks;
	}

	public static long ToInt64(TimeSpan value)
	{
		return value.Ticks;
	}

	public static long ToInt64(long? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(sbyte? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(short? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(int? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(byte? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(ushort? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(uint? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static long ToInt64(ulong? value)
	{
		return value.HasValue ? checked((long)value.Value) : 0;
	}

	public static long ToInt64(float? value)
	{
		return value.HasValue ? checked((long)value.Value) : 0;
	}

	public static long ToInt64(double? value)
	{
		return value.HasValue ? checked((long)value.Value) : 0;
	}

	public static long ToInt64(decimal? value)
	{
		return value.HasValue ? ((long)value.Value) : 0;
	}

	public static long ToInt64(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static long ToInt64(bool? value)
	{
		return (value.HasValue && value.Value) ? 1 : 0;
	}

	public static long ToInt64(DateTime? value)
	{
		return value.HasValue ? (value.Value - DateTime.MinValue).Ticks : 0;
	}

	public static long ToInt64(TimeSpan? value)
	{
		return value.HasValue ? value.Value.Ticks : 0;
	}

	public static long ToInt64(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0L;
		}
		if (!(value is long result))
		{
			if (value is string)
			{
				return ToInt64((string)value);
			}
			if (value is char)
			{
				return ToInt64((char)value);
			}
			if (value is bool)
			{
				return ToInt64((bool)value);
			}
			if (value is DateTime)
			{
				return ToInt64((DateTime)value);
			}
			if (value is TimeSpan)
			{
				return ToInt64((TimeSpan)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(long));
		}
		return result;
	}

	public static Guid ToGuid(string value)
	{
		return (value == null) ? Guid.Empty : new Guid(value);
	}

	public static Guid ToGuid(Guid? value)
	{
		return value.HasValue ? value.Value : Guid.Empty;
	}

	public static Guid ToGuid(byte[] value)
	{
		return (value == null) ? Guid.Empty : new Guid(value);
	}

	public static Guid ToGuid(Type value)
	{
		return (value == null) ? Guid.Empty : value.GUID;
	}

	public static Guid ToGuid(object value)
	{
		if (value == null || value is DBNull)
		{
			return Guid.Empty;
		}
		if (!(value is Guid result))
		{
			if (value is string)
			{
				return ToGuid((string)value);
			}
			if (value is byte[])
			{
				return ToGuid((byte[])value);
			}
			if (value is Type)
			{
				return ToGuid((Type)value);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(Guid));
		}
		return result;
	}

	public static float? ToNullableSingle(float value)
	{
		return value;
	}

	public static float? ToNullableSingle(string value)
	{
		return (value == null) ? null : new float?(float.Parse(value));
	}

	public static float? ToNullableSingle(sbyte value)
	{
		return value;
	}

	public static float? ToNullableSingle(short value)
	{
		return value;
	}

	public static float? ToNullableSingle(int value)
	{
		return value;
	}

	public static float? ToNullableSingle(long value)
	{
		return value;
	}

	public static float? ToNullableSingle(byte value)
	{
		return (int)value;
	}

	public static float? ToNullableSingle(ushort value)
	{
		return (int)value;
	}

	public static float? ToNullableSingle(uint value)
	{
		return value;
	}

	public static float? ToNullableSingle(ulong value)
	{
		return value;
	}

	public static float? ToNullableSingle(double value)
	{
		return (float)value;
	}

	public static float? ToNullableSingle(decimal value)
	{
		return (float)value;
	}

	public static float? ToNullableSingle(char value)
	{
		return (int)value;
	}

	public static float? ToNullableSingle(bool value)
	{
		return value ? 1f : 0f;
	}

	public static float? ToNullableSingle(sbyte? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(short? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(int? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(long? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(byte? value)
	{
		return value.HasValue ? new float?((int)value.Value) : null;
	}

	public static float? ToNullableSingle(ushort? value)
	{
		return value.HasValue ? new float?((int)value.Value) : null;
	}

	public static float? ToNullableSingle(uint? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(ulong? value)
	{
		return value.HasValue ? new float?(value.Value) : null;
	}

	public static float? ToNullableSingle(double? value)
	{
		return value.HasValue ? new float?((float)value.Value) : null;
	}

	public static float? ToNullableSingle(decimal? value)
	{
		return value.HasValue ? new float?((float)value.Value) : null;
	}

	public static float? ToNullableSingle(char? value)
	{
		return value.HasValue ? new float?((int)value.Value) : null;
	}

	public static float? ToNullableSingle(bool? value)
	{
		return value.HasValue ? new float?(value.Value ? 1f : 0f) : null;
	}

	public static float? ToNullableSingle(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is float)
		{
			return ToNullableSingle((float)value);
		}
		if (value is string)
		{
			return ToNullableSingle((string)value);
		}
		if (value is char)
		{
			return ToNullableSingle((char)value);
		}
		if (value is bool)
		{
			return ToNullableSingle((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToSingle(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(float?));
	}

	public static double? ToNullableDouble(double value)
	{
		return value;
	}

	public static double? ToNullableDouble(string value)
	{
		return (value == null) ? null : new double?(double.Parse(value));
	}

	public static double? ToNullableDouble(sbyte value)
	{
		return value;
	}

	public static double? ToNullableDouble(short value)
	{
		return value;
	}

	public static double? ToNullableDouble(int value)
	{
		return value;
	}

	public static double? ToNullableDouble(long value)
	{
		return value;
	}

	public static double? ToNullableDouble(byte value)
	{
		return (int)value;
	}

	public static double? ToNullableDouble(ushort value)
	{
		return (int)value;
	}

	public static double? ToNullableDouble(uint value)
	{
		return value;
	}

	public static double? ToNullableDouble(ulong value)
	{
		return value;
	}

	public static double? ToNullableDouble(float value)
	{
		return value;
	}

	public static double? ToNullableDouble(decimal value)
	{
		return (double)value;
	}

	public static double? ToNullableDouble(char value)
	{
		return (int)value;
	}

	public static double? ToNullableDouble(bool value)
	{
		return value ? 1.0 : 0.0;
	}

	public static double? ToNullableDouble(DateTime value)
	{
		return (value - DateTime.MinValue).TotalDays;
	}

	public static double? ToNullableDouble(TimeSpan value)
	{
		return value.TotalDays;
	}

	public static double? ToNullableDouble(sbyte? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(short? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(int? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(long? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(byte? value)
	{
		return value.HasValue ? new double?((int)value.Value) : null;
	}

	public static double? ToNullableDouble(ushort? value)
	{
		return value.HasValue ? new double?((int)value.Value) : null;
	}

	public static double? ToNullableDouble(uint? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(ulong? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(float? value)
	{
		return value.HasValue ? new double?(value.Value) : null;
	}

	public static double? ToNullableDouble(decimal? value)
	{
		return value.HasValue ? new double?((double)value.Value) : null;
	}

	public static double? ToNullableDouble(char? value)
	{
		return value.HasValue ? new double?((int)value.Value) : null;
	}

	public static double? ToNullableDouble(bool? value)
	{
		return value.HasValue ? new double?(value.Value ? 1.0 : 0.0) : null;
	}

	public static double? ToNullableDouble(DateTime? value)
	{
		return value.HasValue ? new double?((value.Value - DateTime.MinValue).TotalDays) : null;
	}

	public static double? ToNullableDouble(TimeSpan? value)
	{
		return value.HasValue ? new double?(value.Value.TotalDays) : null;
	}

	public static double? ToNullableDouble(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is double)
		{
			return ToNullableDouble((double)value);
		}
		if (value is string)
		{
			return ToNullableDouble((string)value);
		}
		if (value is char)
		{
			return ToNullableDouble((char)value);
		}
		if (value is bool)
		{
			return ToNullableDouble((bool)value);
		}
		if (value is DateTime)
		{
			return ToNullableDouble((DateTime)value);
		}
		if (value is TimeSpan)
		{
			return ToNullableDouble((TimeSpan)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToDouble(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(double?));
	}

	public static bool? ToNullableBoolean(bool value)
	{
		return value;
	}

	public static bool? ToNullableBoolean(string value)
	{
		return (value == null) ? null : new bool?(bool.Parse(value));
	}

	public static bool? ToNullableBoolean(sbyte value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(short value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(int value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(long value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(byte value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(ushort value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(uint value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(ulong value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(float value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(double value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(decimal value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(char value)
	{
		return ToBoolean(value);
	}

	public static bool? ToNullableBoolean(sbyte? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(short? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(int? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(long? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(byte? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(ushort? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(uint? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(ulong? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(float? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(double? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(decimal? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(char? value)
	{
		return value.HasValue ? new bool?(ToBoolean(value.Value)) : null;
	}

	public static bool? ToNullableBoolean(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is bool)
		{
			return ToNullableBoolean((bool)value);
		}
		if (value is string)
		{
			return ToNullableBoolean((string)value);
		}
		if (value is char)
		{
			return ToNullableBoolean((char)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToBoolean(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(bool?));
	}

	public static decimal? ToNullableDecimal(decimal value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(string value)
	{
		return (value == null) ? null : new decimal?(decimal.Parse(value));
	}

	public static decimal? ToNullableDecimal(sbyte value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(short value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(int value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(long value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(byte value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(ushort value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(uint value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(ulong value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(float value)
	{
		return (decimal)value;
	}

	public static decimal? ToNullableDecimal(double value)
	{
		return (decimal)value;
	}

	public static decimal? ToNullableDecimal(char value)
	{
		return value;
	}

	public static decimal? ToNullableDecimal(bool value)
	{
		return value ? 1.0m : 0.0m;
	}

	public static decimal? ToNullableDecimal(sbyte? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(short? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(int? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(long? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(byte? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(ushort? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(uint? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(ulong? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(float? value)
	{
		return value.HasValue ? new decimal?((decimal)value.Value) : null;
	}

	public static decimal? ToNullableDecimal(double? value)
	{
		return value.HasValue ? new decimal?((decimal)value.Value) : null;
	}

	public static decimal? ToNullableDecimal(char? value)
	{
		return value.HasValue ? new decimal?(value.Value) : null;
	}

	public static decimal? ToNullableDecimal(bool? value)
	{
		return value.HasValue ? new decimal?(value.Value ? 1.0m : 0.0m) : null;
	}

	public static decimal? ToNullableDecimal(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is double && double.IsNaN((double)value))
		{
			return null;
		}
		if (value is decimal)
		{
			return ToNullableDecimal((decimal)value);
		}
		if (value is string)
		{
			return ToNullableDecimal((string)value);
		}
		if (value is char)
		{
			return ToNullableDecimal((char)value);
		}
		if (value is bool)
		{
			return ToNullableDecimal((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToDecimal(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(decimal?));
	}

	public static DateTime? ToNullableDateTime(DateTime value)
	{
		return value;
	}

	public static DateTime? ToNullableDateTime(string value)
	{
		return (value == null) ? null : new DateTime?(DateTime.Parse(value));
	}

	public static DateTime? ToNullableDateTime(TimeSpan value)
	{
		return DateTime.MinValue + value;
	}

	public static DateTime? ToNullableDateTime(long value)
	{
		return DateTime.MinValue + TimeSpan.FromTicks(value);
	}

	public static DateTime? ToNullableDateTime(double value)
	{
		return DateTime.MinValue + TimeSpan.FromDays(value);
	}

	public static DateTime? ToNullableDateTime(TimeSpan? value)
	{
		return value.HasValue ? new DateTime?(DateTime.MinValue + value.Value) : null;
	}

	public static DateTime? ToNullableDateTime(long? value)
	{
		return value.HasValue ? new DateTime?(DateTime.MinValue + TimeSpan.FromTicks(value.Value)) : null;
	}

	public static DateTime? ToNullableDateTime(double? value)
	{
		return value.HasValue ? new DateTime?(DateTime.MinValue + TimeSpan.FromDays(value.Value)) : null;
	}

	public static DateTime? ToNullableDateTime(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is DateTime)
		{
			return ToNullableDateTime((DateTime)value);
		}
		if (value is string)
		{
			return ToNullableDateTime((string)value);
		}
		if (value is TimeSpan)
		{
			return ToNullableDateTime((TimeSpan)value);
		}
		if (value is long)
		{
			return ToNullableDateTime((long)value);
		}
		if (value is double)
		{
			return ToNullableDateTime((double)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToDateTime(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(DateTime?));
	}

	public static byte? ToNullableByte(byte value)
	{
		return value;
	}

	public static byte? ToNullableByte(string value)
	{
		return (value == null) ? null : new byte?(byte.Parse(value));
	}

	public static byte? ToNullableByte(sbyte value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(short value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(int value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(long value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(ushort value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(uint value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(ulong value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(float value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(double value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(decimal value)
	{
		return (byte)value;
	}

	public static byte? ToNullableByte(char value)
	{
		return checked((byte)value);
	}

	public static byte? ToNullableByte(bool value)
	{
		return (byte)(value ? 1u : 0u);
	}

	public static byte? ToNullableByte(sbyte? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(short? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(int? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(long? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(ushort? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(uint? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(ulong? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(float? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(double? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(decimal? value)
	{
		return value.HasValue ? new byte?((byte)value.Value) : null;
	}

	public static byte? ToNullableByte(char? value)
	{
		return value.HasValue ? new byte?(checked((byte)value.Value)) : null;
	}

	public static byte? ToNullableByte(bool? value)
	{
		return value.HasValue ? new byte?((byte)(value.Value ? 1u : 0u)) : null;
	}

	public static byte? ToNullableByte(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is byte)
		{
			return ToNullableByte((byte)value);
		}
		if (value is string)
		{
			return ToNullableByte((string)value);
		}
		if (value is char)
		{
			return ToNullableByte((char)value);
		}
		if (value is bool)
		{
			return ToNullableByte((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToByte(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(byte?));
	}

	public static ushort? ToNullableUInt16(ushort value)
	{
		return value;
	}

	public static ushort? ToNullableUInt16(string value)
	{
		return (value == null) ? null : new ushort?(ushort.Parse(value));
	}

	public static ushort? ToNullableUInt16(sbyte value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(short value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(int value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(long value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(byte value)
	{
		return value;
	}

	public static ushort? ToNullableUInt16(uint value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(ulong value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(float value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(double value)
	{
		return checked((ushort)value);
	}

	public static ushort? ToNullableUInt16(decimal value)
	{
		return (ushort)value;
	}

	public static ushort? ToNullableUInt16(char value)
	{
		return value;
	}

	public static ushort? ToNullableUInt16(bool value)
	{
		return (ushort)(value ? 1u : 0u);
	}

	public static ushort? ToNullableUInt16(sbyte? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(short? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(int? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(long? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(byte? value)
	{
		return value.HasValue ? new ushort?(value.Value) : null;
	}

	public static ushort? ToNullableUInt16(uint? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(ulong? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(float? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(double? value)
	{
		return value.HasValue ? new ushort?(checked((ushort)value.Value)) : null;
	}

	public static ushort? ToNullableUInt16(decimal? value)
	{
		return value.HasValue ? new ushort?((ushort)value.Value) : null;
	}

	public static ushort? ToNullableUInt16(char? value)
	{
		return value.HasValue ? new ushort?(value.Value) : null;
	}

	public static ushort? ToNullableUInt16(bool? value)
	{
		return value.HasValue ? new ushort?((ushort)(value.Value ? 1u : 0u)) : null;
	}

	public static ushort? ToNullableUInt16(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is ushort)
		{
			return ToNullableUInt16((ushort)value);
		}
		if (value is string)
		{
			return ToNullableUInt16((string)value);
		}
		if (value is char)
		{
			return ToNullableUInt16((char)value);
		}
		if (value is bool)
		{
			return ToNullableUInt16((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToUInt16(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(ushort?));
	}

	public static uint? ToNullableUInt32(uint value)
	{
		return value;
	}

	public static uint? ToNullableUInt32(string value)
	{
		return (value == null) ? null : new uint?(uint.Parse(value));
	}

	public static uint? ToNullableUInt32(sbyte value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(short value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(int value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(long value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(byte value)
	{
		return value;
	}

	public static uint? ToNullableUInt32(ushort value)
	{
		return value;
	}

	public static uint? ToNullableUInt32(ulong value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(float value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(double value)
	{
		return checked((uint)value);
	}

	public static uint? ToNullableUInt32(decimal value)
	{
		return (uint)value;
	}

	public static uint? ToNullableUInt32(char value)
	{
		return value;
	}

	public static uint? ToNullableUInt32(bool value)
	{
		return value ? 1u : 0u;
	}

	public static uint? ToNullableUInt32(sbyte? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(short? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(int? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(long? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(byte? value)
	{
		return value.HasValue ? new uint?(value.Value) : null;
	}

	public static uint? ToNullableUInt32(ushort? value)
	{
		return value.HasValue ? new uint?(value.Value) : null;
	}

	public static uint? ToNullableUInt32(ulong? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(float? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(double? value)
	{
		return value.HasValue ? new uint?(checked((uint)value.Value)) : null;
	}

	public static uint? ToNullableUInt32(decimal? value)
	{
		return value.HasValue ? new uint?((uint)value.Value) : null;
	}

	public static uint? ToNullableUInt32(char? value)
	{
		return value.HasValue ? new uint?(value.Value) : null;
	}

	public static uint? ToNullableUInt32(bool? value)
	{
		return value.HasValue ? new uint?(value.Value ? 1u : 0u) : null;
	}

	public static uint? ToNullableUInt32(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is uint)
		{
			return ToNullableUInt32((uint)value);
		}
		if (value is string)
		{
			return ToNullableUInt32((string)value);
		}
		if (value is char)
		{
			return ToNullableUInt32((char)value);
		}
		if (value is bool)
		{
			return ToNullableUInt32((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToUInt32(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(uint?));
	}

	public static ulong? ToNullableUInt64(ulong value)
	{
		return value;
	}

	public static ulong? ToNullableUInt64(string value)
	{
		return (value == null) ? null : new ulong?(ulong.Parse(value));
	}

	public static ulong? ToNullableUInt64(sbyte value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(short value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(int value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(long value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(byte value)
	{
		return value;
	}

	public static ulong? ToNullableUInt64(ushort value)
	{
		return value;
	}

	public static ulong? ToNullableUInt64(uint value)
	{
		return value;
	}

	public static ulong? ToNullableUInt64(float value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(double value)
	{
		return checked((ulong)value);
	}

	public static ulong? ToNullableUInt64(decimal value)
	{
		return (ulong)value;
	}

	public static ulong? ToNullableUInt64(char value)
	{
		return value;
	}

	public static ulong? ToNullableUInt64(bool value)
	{
		return (ulong)(int)(value ? 1u : 0u);
	}

	public static ulong? ToNullableUInt64(sbyte? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(short? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(int? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(long? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(byte? value)
	{
		return value.HasValue ? new ulong?(value.Value) : null;
	}

	public static ulong? ToNullableUInt64(ushort? value)
	{
		return value.HasValue ? new ulong?(value.Value) : null;
	}

	public static ulong? ToNullableUInt64(uint? value)
	{
		return value.HasValue ? new ulong?(value.Value) : null;
	}

	public static ulong? ToNullableUInt64(float? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(double? value)
	{
		return value.HasValue ? new ulong?(checked((ulong)value.Value)) : null;
	}

	public static ulong? ToNullableUInt64(decimal? value)
	{
		return value.HasValue ? new ulong?((ulong)value.Value) : null;
	}

	public static ulong? ToNullableUInt64(char? value)
	{
		return value.HasValue ? new ulong?(value.Value) : null;
	}

	public static ulong? ToNullableUInt64(bool? value)
	{
		return value.HasValue ? new ulong?((ulong)(int)(value.Value ? 1u : 0u)) : null;
	}

	public static ulong? ToNullableUInt64(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is ulong)
		{
			return ToNullableUInt64((ulong)value);
		}
		if (value is string)
		{
			return ToNullableUInt64((string)value);
		}
		if (value is char)
		{
			return ToNullableUInt64((char)value);
		}
		if (value is bool)
		{
			return ToNullableUInt64((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToUInt64(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(ulong?));
	}

	public static char? ToNullableChar(char value)
	{
		return value;
	}

	public static char? ToNullableChar(string value)
	{
		if (char.TryParse(value, out var result))
		{
			return result;
		}
		return '\0';
	}

	public static char? ToNullableChar(sbyte value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(short value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(int value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(long value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(byte value)
	{
		return (char)value;
	}

	public static char? ToNullableChar(ushort value)
	{
		return (char)value;
	}

	public static char? ToNullableChar(uint value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(ulong value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(float value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(double value)
	{
		return (char)checked((ushort)value);
	}

	public static char? ToNullableChar(decimal value)
	{
		return (char)value;
	}

	public static char? ToNullableChar(bool value)
	{
		return (char)(value ? 1u : 0u);
	}

	public static char? ToNullableChar(sbyte? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(short? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(int? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(long? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(byte? value)
	{
		return value.HasValue ? new char?((char)value.Value) : null;
	}

	public static char? ToNullableChar(ushort? value)
	{
		return value.HasValue ? new char?((char)value.Value) : null;
	}

	public static char? ToNullableChar(uint? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(ulong? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(float? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(double? value)
	{
		return value.HasValue ? new char?((char)checked((ushort)value.Value)) : null;
	}

	public static char? ToNullableChar(decimal? value)
	{
		return value.HasValue ? new char?((char)value.Value) : null;
	}

	public static char? ToNullableChar(bool? value)
	{
		return value.HasValue ? new char?((char)(value.Value ? 1u : 0u)) : null;
	}

	public static char? ToNullableChar(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is char)
		{
			return ToNullableChar((char)value);
		}
		if (value is string)
		{
			return ToNullableChar((string)value);
		}
		if (value is bool)
		{
			return ToNullableChar((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToChar(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(char?));
	}

	public static Guid? ToNullableGuid(Guid value)
	{
		return value;
	}

	public static Guid? ToNullableGuid(string value)
	{
		return (value == null) ? null : new Guid?(new Guid(value));
	}

	public static Guid? ToNullableGuid(Type value)
	{
		return (value == null) ? null : new Guid?(value.GUID);
	}

	public static Guid? ToNullableGuid(byte[] value)
	{
		return (value == null) ? null : new Guid?(new Guid(value));
	}

	public static Guid? ToNullableGuid(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is Guid)
		{
			return ToNullableGuid((Guid)value);
		}
		if (value is string)
		{
			return ToNullableGuid((string)value);
		}
		if (value is Type)
		{
			return ToNullableGuid((Type)value);
		}
		if (value is byte[])
		{
			return ToNullableGuid((byte[])value);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(Guid?));
	}

	public static short? ToNullableInt16(short value)
	{
		return value;
	}

	public static short? ToNullableInt16(string value)
	{
		return (value == null) ? null : new short?(short.Parse(value));
	}

	public static short? ToNullableInt16(sbyte value)
	{
		return value;
	}

	public static short? ToNullableInt16(int value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(long value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(byte value)
	{
		return value;
	}

	public static short? ToNullableInt16(ushort value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(uint value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(ulong value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(float value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(double value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(decimal value)
	{
		return (short)value;
	}

	public static short? ToNullableInt16(char value)
	{
		return checked((short)value);
	}

	public static short? ToNullableInt16(bool value)
	{
		return (short)(value ? 1 : 0);
	}

	public static short? ToNullableInt16(sbyte? value)
	{
		return value.HasValue ? new short?(value.Value) : null;
	}

	public static short? ToNullableInt16(int? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(long? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(byte? value)
	{
		return value.HasValue ? new short?(value.Value) : null;
	}

	public static short? ToNullableInt16(ushort? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(uint? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(ulong? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(float? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(double? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(decimal? value)
	{
		return value.HasValue ? new short?((short)value.Value) : null;
	}

	public static short? ToNullableInt16(char? value)
	{
		return value.HasValue ? new short?(checked((short)value.Value)) : null;
	}

	public static short? ToNullableInt16(bool? value)
	{
		return value.HasValue ? new short?((short)(value.Value ? 1 : 0)) : null;
	}

	public static short? ToNullableInt16(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is short)
		{
			return ToNullableInt16((short)value);
		}
		if (value is string)
		{
			return ToNullableInt16((string)value);
		}
		if (value is char)
		{
			return ToNullableInt16((char)value);
		}
		if (value is bool)
		{
			return ToNullableInt16((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToInt16(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(short?));
	}

	public static int? ToNullableInt32(int value)
	{
		return value;
	}

	public static int? ToNullableInt32(string value)
	{
		return (value == null) ? null : new int?(int.Parse(value));
	}

	public static int? ToNullableInt32(sbyte value)
	{
		return value;
	}

	public static int? ToNullableInt32(short value)
	{
		return value;
	}

	public static int? ToNullableInt32(long value)
	{
		return checked((int)value);
	}

	public static int? ToNullableInt32(byte value)
	{
		return value;
	}

	public static int? ToNullableInt32(ushort value)
	{
		return value;
	}

	public static int? ToNullableInt32(uint value)
	{
		return checked((int)value);
	}

	public static int? ToNullableInt32(ulong value)
	{
		return checked((int)value);
	}

	public static int? ToNullableInt32(float value)
	{
		return checked((int)value);
	}

	public static int? ToNullableInt32(double value)
	{
		return checked((int)value);
	}

	public static int? ToNullableInt32(decimal value)
	{
		return (int)value;
	}

	public static int? ToNullableInt32(char value)
	{
		return value;
	}

	public static int? ToNullableInt32(bool value)
	{
		return value ? 1 : 0;
	}

	public static int? ToNullableInt32(sbyte? value)
	{
		return value.HasValue ? new int?(value.Value) : null;
	}

	public static int? ToNullableInt32(short? value)
	{
		return value.HasValue ? new int?(value.Value) : null;
	}

	public static int? ToNullableInt32(long? value)
	{
		return value.HasValue ? new int?(checked((int)value.Value)) : null;
	}

	public static int? ToNullableInt32(byte? value)
	{
		return value.HasValue ? new int?(value.Value) : null;
	}

	public static int? ToNullableInt32(ushort? value)
	{
		return value.HasValue ? new int?(value.Value) : null;
	}

	public static int? ToNullableInt32(uint? value)
	{
		return value.HasValue ? new int?(checked((int)value.Value)) : null;
	}

	public static int? ToNullableInt32(ulong? value)
	{
		return value.HasValue ? new int?(checked((int)value.Value)) : null;
	}

	public static int? ToNullableInt32(float? value)
	{
		return value.HasValue ? new int?(checked((int)value.Value)) : null;
	}

	public static int? ToNullableInt32(double? value)
	{
		return value.HasValue ? new int?(checked((int)value.Value)) : null;
	}

	public static int? ToNullableInt32(decimal? value)
	{
		return value.HasValue ? new int?((int)value.Value) : null;
	}

	public static int? ToNullableInt32(char? value)
	{
		return value.HasValue ? new int?(value.Value) : null;
	}

	public static int? ToNullableInt32(bool? value)
	{
		return value.HasValue ? new int?(value.Value ? 1 : 0) : null;
	}

	public static int? ToNullableInt32(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is int)
		{
			return ToNullableInt32((int)value);
		}
		if (value is string)
		{
			return ToNullableInt32((string)value);
		}
		if (value is char)
		{
			return ToNullableInt32((char)value);
		}
		if (value is bool)
		{
			return ToNullableInt32((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToInt32(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(int?));
	}

	public static long? ToNullableInt64(long value)
	{
		return value;
	}

	public static long? ToNullableInt64(string value)
	{
		return (value == null) ? null : new long?(long.Parse(value));
	}

	public static long? ToNullableInt64(sbyte value)
	{
		return value;
	}

	public static long? ToNullableInt64(short value)
	{
		return value;
	}

	public static long? ToNullableInt64(int value)
	{
		return value;
	}

	public static long? ToNullableInt64(byte value)
	{
		return value;
	}

	public static long? ToNullableInt64(ushort value)
	{
		return value;
	}

	public static long? ToNullableInt64(uint value)
	{
		return value;
	}

	public static long? ToNullableInt64(ulong value)
	{
		return checked((long)value);
	}

	public static long? ToNullableInt64(float value)
	{
		return checked((long)value);
	}

	public static long? ToNullableInt64(double value)
	{
		return checked((long)value);
	}

	public static long? ToNullableInt64(decimal value)
	{
		return (long)value;
	}

	public static long? ToNullableInt64(char value)
	{
		return value;
	}

	public static long? ToNullableInt64(bool value)
	{
		return value ? 1 : 0;
	}

	public static long? ToNullableInt64(DateTime value)
	{
		return (value - DateTime.MinValue).Ticks;
	}

	public static long? ToNullableInt64(TimeSpan value)
	{
		return value.Ticks;
	}

	public static long? ToNullableInt64(sbyte? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(short? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(int? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(byte? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(ushort? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(uint? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(ulong? value)
	{
		return value.HasValue ? new long?(checked((long)value.Value)) : null;
	}

	public static long? ToNullableInt64(float? value)
	{
		return value.HasValue ? new long?(checked((long)value.Value)) : null;
	}

	public static long? ToNullableInt64(double? value)
	{
		return value.HasValue ? new long?(checked((long)value.Value)) : null;
	}

	public static long? ToNullableInt64(decimal? value)
	{
		return value.HasValue ? new long?((long)value.Value) : null;
	}

	public static long? ToNullableInt64(char? value)
	{
		return value.HasValue ? new long?(value.Value) : null;
	}

	public static long? ToNullableInt64(bool? value)
	{
		return value.HasValue ? new long?(value.Value ? 1 : 0) : null;
	}

	public static long? ToNullableInt64(DateTime? value)
	{
		return value.HasValue ? new long?((value.Value - DateTime.MinValue).Ticks) : null;
	}

	public static long? ToNullableInt64(TimeSpan? value)
	{
		return value.HasValue ? new long?(value.Value.Ticks) : null;
	}

	public static long? ToNullableInt64(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is long)
		{
			return ToNullableInt64((long)value);
		}
		if (value is string)
		{
			return ToNullableInt64((string)value);
		}
		if (value is char)
		{
			return ToNullableInt64((char)value);
		}
		if (value is bool)
		{
			return ToNullableInt64((bool)value);
		}
		if (value is DateTime)
		{
			return ToNullableInt64((DateTime)value);
		}
		if (value is TimeSpan)
		{
			return ToNullableInt64((TimeSpan)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToInt64(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(long?));
	}

	public static sbyte? ToNullableSByte(sbyte value)
	{
		return value;
	}

	public static sbyte? ToNullableSByte(string value)
	{
		return (value == null) ? null : new sbyte?(sbyte.Parse(value));
	}

	public static sbyte? ToNullableSByte(short value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(int value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(long value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(byte value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(ushort value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(uint value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(ulong value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(float value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(double value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(decimal value)
	{
		return (sbyte)value;
	}

	public static sbyte? ToNullableSByte(char value)
	{
		return checked((sbyte)value);
	}

	public static sbyte? ToNullableSByte(bool value)
	{
		return (sbyte)(value ? 1 : 0);
	}

	public static sbyte? ToNullableSByte(short? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(int? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(long? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(byte? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(ushort? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(uint? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(ulong? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(float? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(double? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(decimal? value)
	{
		return value.HasValue ? new sbyte?((sbyte)value.Value) : null;
	}

	public static sbyte? ToNullableSByte(char? value)
	{
		return value.HasValue ? new sbyte?(checked((sbyte)value.Value)) : null;
	}

	public static sbyte? ToNullableSByte(bool? value)
	{
		return value.HasValue ? new sbyte?((sbyte)(value.Value ? 1 : 0)) : null;
	}

	public static sbyte? ToNullableSByte(object value)
	{
		if (value == null || value is DBNull)
		{
			return null;
		}
		if (value is sbyte)
		{
			return ToNullableSByte((sbyte)value);
		}
		if (value is string)
		{
			return ToNullableSByte((string)value);
		}
		if (value is char)
		{
			return ToNullableSByte((char)value);
		}
		if (value is bool)
		{
			return ToNullableSByte((bool)value);
		}
		if (value is IConvertible)
		{
			return ((IConvertible)value).ToSByte(null);
		}
		throw CreateInvalidCastException(value.GetType(), typeof(sbyte?));
	}

	public static float ToSingle(string value)
	{
		return (value == null) ? 0f : float.Parse(value);
	}

	public static float ToSingle(sbyte value)
	{
		return value;
	}

	public static float ToSingle(short value)
	{
		return value;
	}

	public static float ToSingle(int value)
	{
		return value;
	}

	public static float ToSingle(long value)
	{
		return value;
	}

	public static float ToSingle(byte value)
	{
		return (int)value;
	}

	public static float ToSingle(ushort value)
	{
		return (int)value;
	}

	public static float ToSingle(uint value)
	{
		return value;
	}

	public static float ToSingle(ulong value)
	{
		return value;
	}

	public static float ToSingle(double value)
	{
		return (float)value;
	}

	public static float ToSingle(decimal value)
	{
		return (float)value;
	}

	public static float ToSingle(bool value)
	{
		return value ? 1f : 0f;
	}

	public static float ToSingle(char value)
	{
		return (int)value;
	}

	public static float ToSingle(float? value)
	{
		return value.HasValue ? value.Value : 0f;
	}

	public static float ToSingle(sbyte? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(short? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(int? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(long? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(byte? value)
	{
		return value.HasValue ? ((float)(int)value.Value) : 0f;
	}

	public static float ToSingle(ushort? value)
	{
		return value.HasValue ? ((float)(int)value.Value) : 0f;
	}

	public static float ToSingle(uint? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(ulong? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(double? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(decimal? value)
	{
		return value.HasValue ? ((float)value.Value) : 0f;
	}

	public static float ToSingle(char? value)
	{
		return value.HasValue ? ((float)(int)value.Value) : 0f;
	}

	public static float ToSingle(bool? value)
	{
		return (value.HasValue && value.Value) ? 1f : 0f;
	}

	public static float ToSingle(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0f;
		}
		if (!(value is float result))
		{
			if (value is string)
			{
				return ToSingle((string)value);
			}
			if (value is bool)
			{
				return ToSingle((bool)value);
			}
			if (value is char)
			{
				return ToSingle((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(float));
		}
		return result;
	}

	public static TimeSpan ToTimeSpan(string value)
	{
		return (value == null) ? TimeSpan.MinValue : TimeSpan.Parse(value);
	}

	public static TimeSpan ToTimeSpan(DateTime value)
	{
		return value - DateTime.MinValue;
	}

	public static TimeSpan ToTimeSpan(long value)
	{
		return TimeSpan.FromTicks(value);
	}

	public static TimeSpan ToTimeSpan(double value)
	{
		return TimeSpan.FromDays(value);
	}

	public static TimeSpan ToTimeSpan(TimeSpan? value)
	{
		return value.HasValue ? value.Value : TimeSpan.MinValue;
	}

	public static TimeSpan ToTimeSpan(DateTime? value)
	{
		return value.HasValue ? (value.Value - DateTime.MinValue) : TimeSpan.MinValue;
	}

	public static TimeSpan ToTimeSpan(long? value)
	{
		return value.HasValue ? TimeSpan.FromTicks(value.Value) : TimeSpan.MinValue;
	}

	public static TimeSpan ToTimeSpan(double? value)
	{
		return value.HasValue ? TimeSpan.FromDays(value.Value) : TimeSpan.MinValue;
	}

	public static TimeSpan ToTimeSpan(object value)
	{
		if (value == null || value is DBNull)
		{
			return TimeSpan.MinValue;
		}
		if (!(value is TimeSpan result))
		{
			if (value is string)
			{
				return ToTimeSpan((string)value);
			}
			if (value is DateTime)
			{
				return ToTimeSpan((DateTime)value);
			}
			if (value is long)
			{
				return ToTimeSpan((long)value);
			}
			if (value is double)
			{
				return ToTimeSpan((double)value);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(TimeSpan));
		}
		return result;
	}

	public static ushort ToUInt16(string value)
	{
		return (ushort)((value != null) ? ushort.Parse(value) : 0);
	}

	public static ushort ToUInt16(sbyte value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(short value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(int value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(long value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(byte value)
	{
		return value;
	}

	public static ushort ToUInt16(uint value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(ulong value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(float value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(double value)
	{
		return checked((ushort)value);
	}

	public static ushort ToUInt16(decimal value)
	{
		return (ushort)value;
	}

	public static ushort ToUInt16(bool value)
	{
		return (ushort)(value ? 1u : 0u);
	}

	public static ushort ToUInt16(char value)
	{
		return value;
	}

	public static ushort ToUInt16(ushort? value)
	{
		return (ushort)(value.HasValue ? value.Value : 0);
	}

	public static ushort ToUInt16(sbyte? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(short? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(int? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(long? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(byte? value)
	{
		return (ushort)(value.HasValue ? value.Value : 0);
	}

	public static ushort ToUInt16(uint? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(ulong? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(float? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(double? value)
	{
		return (ushort)(value.HasValue ? checked((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(decimal? value)
	{
		return (ushort)(value.HasValue ? ((ushort)value.Value) : 0);
	}

	public static ushort ToUInt16(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static ushort ToUInt16(bool? value)
	{
		return (ushort)((value.HasValue && value.Value) ? 1 : 0);
	}

	public static ushort ToUInt16(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0;
		}
		if (!(value is ushort result))
		{
			if (value is string)
			{
				return ToUInt16((string)value);
			}
			if (value is bool)
			{
				return ToUInt16((bool)value);
			}
			if (value is char)
			{
				return ToUInt16((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(ushort));
		}
		return result;
	}

	public static uint ToUInt32(string value)
	{
		return (value != null) ? uint.Parse(value) : 0u;
	}

	public static uint ToUInt32(sbyte value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(short value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(int value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(long value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(byte value)
	{
		return value;
	}

	public static uint ToUInt32(ushort value)
	{
		return value;
	}

	public static uint ToUInt32(ulong value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(float value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(double value)
	{
		return checked((uint)value);
	}

	public static uint ToUInt32(decimal value)
	{
		return (uint)value;
	}

	public static uint ToUInt32(bool value)
	{
		return value ? 1u : 0u;
	}

	public static uint ToUInt32(char value)
	{
		return value;
	}

	public static uint ToUInt32(uint? value)
	{
		return value.HasValue ? value.Value : 0u;
	}

	public static uint ToUInt32(sbyte? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(short? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(int? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(long? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(byte? value)
	{
		return (uint)(value.HasValue ? value.Value : 0);
	}

	public static uint ToUInt32(ushort? value)
	{
		return (uint)(value.HasValue ? value.Value : 0);
	}

	public static uint ToUInt32(ulong? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(float? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(double? value)
	{
		return value.HasValue ? checked((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(decimal? value)
	{
		return value.HasValue ? ((uint)value.Value) : 0u;
	}

	public static uint ToUInt32(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static uint ToUInt32(bool? value)
	{
		return (value.HasValue && value.Value) ? 1u : 0u;
	}

	public static uint ToUInt32(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0u;
		}
		if (!(value is uint result))
		{
			if (value is string)
			{
				return ToUInt32((string)value);
			}
			if (value is bool)
			{
				return ToUInt32((bool)value);
			}
			if (value is char)
			{
				return ToUInt32((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(uint));
		}
		return result;
	}

	public static ulong ToUInt64(string value)
	{
		return (value == null) ? 0 : ulong.Parse(value);
	}

	public static ulong ToUInt64(sbyte value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(short value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(int value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(long value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(byte value)
	{
		return value;
	}

	public static ulong ToUInt64(ushort value)
	{
		return value;
	}

	public static ulong ToUInt64(uint value)
	{
		return value;
	}

	public static ulong ToUInt64(float value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(double value)
	{
		return checked((ulong)value);
	}

	public static ulong ToUInt64(decimal value)
	{
		return (ulong)value;
	}

	public static ulong ToUInt64(bool value)
	{
		return (ulong)(int)(value ? 1u : 0u);
	}

	public static ulong ToUInt64(char value)
	{
		return value;
	}

	public static ulong ToUInt64(ulong? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static ulong ToUInt64(sbyte? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(short? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(int? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(long? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(byte? value)
	{
		return (ulong)(value.HasValue ? value.Value : 0);
	}

	public static ulong ToUInt64(ushort? value)
	{
		return (ulong)(value.HasValue ? value.Value : 0);
	}

	public static ulong ToUInt64(uint? value)
	{
		return value.HasValue ? value.Value : 0;
	}

	public static ulong ToUInt64(float? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(double? value)
	{
		return value.HasValue ? checked((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(decimal? value)
	{
		return value.HasValue ? ((ulong)value.Value) : 0;
	}

	public static ulong ToUInt64(char? value)
	{
		return value.HasValue ? value.Value : '\0';
	}

	public static ulong ToUInt64(bool? value)
	{
		return (ulong)((value.HasValue && value.Value) ? 1 : 0);
	}

	public static ulong ToUInt64(object value)
	{
		if (value == null || value is DBNull)
		{
			return 0uL;
		}
		if (!(value is ulong result))
		{
			if (value is string)
			{
				return ToUInt64((string)value);
			}
			if (value is bool)
			{
				return ToUInt64((bool)value);
			}
			if (value is char)
			{
				return ToUInt64((char)value);
			}
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			throw CreateInvalidCastException(value.GetType(), typeof(ulong));
		}
		return result;
	}

	public static XmlDocument ToXmlDocument(string p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(p);
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(Stream p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(p);
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(TextReader p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(p);
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(XDocument p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(p.ToString());
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(XElement p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(p.ToString());
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(char[] p)
	{
		return (p == null) ? null : ToXmlDocument(new string(p));
	}

	public static XmlDocument ToXmlDocument(byte[] p)
	{
		return (p == null) ? null : ToXmlDocument(new MemoryStream(p));
	}

	public static XmlDocument ToXmlDocument(XmlReader p)
	{
		if (p == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(p);
		return xmlDocument;
	}

	public static XmlDocument ToXmlDocument(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is XmlDocument)
		{
			return (XmlDocument)p;
		}
		if (p is string)
		{
			return ToXmlDocument((string)p);
		}
		if (p is Stream)
		{
			return ToXmlDocument((Stream)p);
		}
		if (p is TextReader)
		{
			return ToXmlDocument((TextReader)p);
		}
		if (p is XmlReader)
		{
			return ToXmlDocument((XmlReader)p);
		}
		if (p is char[])
		{
			return ToXmlDocument((char[])p);
		}
		if (p is byte[])
		{
			return ToXmlDocument((byte[])p);
		}
		if (p is XDocument)
		{
			return ToXmlDocument((XDocument)p);
		}
		if (p is XElement)
		{
			return ToXmlDocument((XElement)p);
		}
		throw CreateInvalidCastException(p.GetType(), typeof(XmlDocument));
	}

	public static XDocument ToXDocument(string p)
	{
		if (p == null)
		{
			return null;
		}
		return XDocument.Parse(p);
	}

	public static XDocument ToXDocument(XmlDocument p)
	{
		if (p == null)
		{
			return null;
		}
		return XDocument.Parse(p.OuterXml);
	}

	public static XDocument ToXDocument(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is XDocument)
		{
			return (XDocument)p;
		}
		if (p is string)
		{
			return ToXDocument((string)p);
		}
		if (p is XmlDocument)
		{
			return ToXDocument((XmlDocument)p);
		}
		throw CreateInvalidCastException(p.GetType(), typeof(XDocument));
	}

	public static XElement ToXElement(string p)
	{
		if (p == null)
		{
			return null;
		}
		return XElement.Parse(p);
	}

	public static XElement ToXElement(XmlDocument p)
	{
		if (p == null)
		{
			return null;
		}
		return XElement.Parse(p.OuterXml);
	}

	public static XElement ToXElement(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is XElement)
		{
			return (XElement)p;
		}
		if (p is string)
		{
			return ToXElement((string)p);
		}
		if (p is XmlDocument)
		{
			return ToXElement((XmlDocument)p);
		}
		throw CreateInvalidCastException(p.GetType(), typeof(XElement));
	}

	public static XmlReader ToXmlReader(string p)
	{
		return (p == null) ? null : new XmlTextReader(new StringReader(p));
	}

	public static XmlReader ToXmlReader(Stream p)
	{
		return (p == null) ? null : new XmlTextReader(p);
	}

	public static XmlReader ToXmlReader(TextReader p)
	{
		return (p == null) ? null : new XmlTextReader(p);
	}

	public static XmlReader ToXmlReader(XmlDocument p)
	{
		return (p == null) ? null : new XmlTextReader(new StringReader(p.InnerXml));
	}

	public static XmlReader ToXmlReader(char[] p)
	{
		return (p == null) ? null : new XmlTextReader(new StringReader(new string(p)));
	}

	public static XmlReader ToXmlReader(byte[] p)
	{
		return (p == null) ? null : new XmlTextReader(new MemoryStream(p));
	}

	public static XmlReader ToXmlReader(object p)
	{
		if (p == null || p is DBNull)
		{
			return null;
		}
		if (p is XmlReader)
		{
			return (XmlReader)p;
		}
		if (p is string)
		{
			return ToXmlReader((string)p);
		}
		if (p is Stream)
		{
			return ToXmlReader((Stream)p);
		}
		if (p is TextReader)
		{
			return ToXmlReader((TextReader)p);
		}
		if (p is XmlDocument)
		{
			return ToXmlReader((XmlDocument)p);
		}
		if (p is char[])
		{
			return ToXmlReader((char[])p);
		}
		if (p is byte[])
		{
			return ToXmlReader((byte[])p);
		}
		throw CreateInvalidCastException(p.GetType(), typeof(XmlReader));
	}

	private static InvalidCastException CreateInvalidCastException(Type originalType, Type conversionType)
	{
		return new InvalidCastException($"Invalid cast from {originalType.FullName} to {conversionType.FullName}");
	}
}
