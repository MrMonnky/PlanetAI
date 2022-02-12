using System;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal sealed class NumberUtils
{
	public static bool IsInteger(object number)
	{
		return number is int || number is short || number is long || number is uint || number is ushort || number is ulong || number is byte || number is sbyte;
	}

	public static bool IsDecimal(object number)
	{
		return number is float || number is double || number is decimal;
	}

	public static bool IsNumber(object number)
	{
		return IsInteger(number) || IsDecimal(number);
	}

	public static bool IsZero(object number)
	{
		if (number is int)
		{
			return (int)number == 0;
		}
		if (number is short)
		{
			return (short)number == 0;
		}
		if (number is long)
		{
			return (long)number == 0;
		}
		if (number is ushort)
		{
			return (int)number == 0;
		}
		if (number is uint)
		{
			return (long)number == 0;
		}
		if (number is ulong)
		{
			return Convert.ToDecimal(number) == 0m;
		}
		if (number is byte)
		{
			return (short)number == 0;
		}
		if (number is sbyte)
		{
			return (short)number == 0;
		}
		if (number is float)
		{
			return (float)number == 0f;
		}
		if (number is double)
		{
			return (double)number == 0.0;
		}
		if (number is decimal)
		{
			return (decimal)number == 0m;
		}
		return false;
	}

	public static object Negate(object number)
	{
		if (number is int)
		{
			return -(int)number;
		}
		if (number is short)
		{
			return -(short)number;
		}
		if (number is long)
		{
			return -(long)number;
		}
		if (number is ushort)
		{
			return -(int)number;
		}
		if (number is uint)
		{
			return -(long)number;
		}
		if (number is ulong)
		{
			return -Convert.ToDecimal(number);
		}
		if (number is byte)
		{
			return -(short)number;
		}
		if (number is sbyte)
		{
			return -(short)number;
		}
		if (number is float)
		{
			return 0f - (float)number;
		}
		if (number is double)
		{
			return 0.0 - (double)number;
		}
		if (number is decimal)
		{
			return -(decimal)number;
		}
		throw new ArgumentException($"'{number}' is not one of the supported numeric types.");
	}

	public static object Add(object m, object n)
	{
		CoerceTypes(ref m, ref n);
		if (n is int)
		{
			return (int)m + (int)n;
		}
		if (n is short)
		{
			return (short)m + (short)n;
		}
		if (n is long)
		{
			return (long)m + (long)n;
		}
		if (n is ushort)
		{
			return (ushort)m + (ushort)n;
		}
		if (n is uint)
		{
			return (uint)m + (uint)n;
		}
		if (n is ulong)
		{
			return (ulong)m + (ulong)n;
		}
		if (n is byte)
		{
			return (byte)m + (byte)n;
		}
		if (n is sbyte)
		{
			return (sbyte)m + (sbyte)n;
		}
		if (n is float)
		{
			return (float)m + (float)n;
		}
		if (n is double)
		{
			return (double)m + (double)n;
		}
		if (n is decimal)
		{
			return (decimal)m + (decimal)n;
		}
		return null;
	}

	public static object Subtract(object m, object n)
	{
		CoerceTypes(ref m, ref n);
		if (n is int)
		{
			return (int)m - (int)n;
		}
		if (n is short)
		{
			return (short)m - (short)n;
		}
		if (n is long)
		{
			return (long)m - (long)n;
		}
		if (n is ushort)
		{
			return (ushort)m - (ushort)n;
		}
		if (n is uint)
		{
			return (uint)m - (uint)n;
		}
		if (n is ulong)
		{
			return (ulong)m - (ulong)n;
		}
		if (n is byte)
		{
			return (byte)m - (byte)n;
		}
		if (n is sbyte)
		{
			return (sbyte)m - (sbyte)n;
		}
		if (n is float)
		{
			return (float)m - (float)n;
		}
		if (n is double)
		{
			return (double)m - (double)n;
		}
		if (n is decimal)
		{
			return (decimal)m - (decimal)n;
		}
		return null;
	}

	public static object Multiply(object m, object n)
	{
		CoerceTypes(ref m, ref n);
		if (n is int)
		{
			return (int)m * (int)n;
		}
		if (n is short)
		{
			return (short)m * (short)n;
		}
		if (n is long)
		{
			return (long)m * (long)n;
		}
		if (n is ushort)
		{
			return (ushort)m * (ushort)n;
		}
		if (n is uint)
		{
			return (uint)m * (uint)n;
		}
		if (n is ulong)
		{
			return (ulong)m * (ulong)n;
		}
		if (n is byte)
		{
			return (byte)m * (byte)n;
		}
		if (n is sbyte)
		{
			return (sbyte)m * (sbyte)n;
		}
		if (n is float)
		{
			return (float)m * (float)n;
		}
		if (n is double)
		{
			return (double)m * (double)n;
		}
		if (n is decimal)
		{
			return (decimal)m * (decimal)n;
		}
		return null;
	}

	public static object Divide(object m, object n)
	{
		CoerceTypes(ref m, ref n);
		if (n is int)
		{
			return (int)m / (int)n;
		}
		if (n is short)
		{
			return (short)m / (short)n;
		}
		if (n is long)
		{
			return (long)m / (long)n;
		}
		if (n is ushort)
		{
			return (int)(ushort)m / (int)(ushort)n;
		}
		if (n is uint)
		{
			return (uint)m / (uint)n;
		}
		if (n is ulong)
		{
			return (ulong)m / (ulong)n;
		}
		if (n is byte)
		{
			return (int)(byte)m / (int)(byte)n;
		}
		if (n is sbyte)
		{
			return (sbyte)m / (sbyte)n;
		}
		if (n is float)
		{
			return (float)m / (float)n;
		}
		if (n is double)
		{
			return (double)m / (double)n;
		}
		if (n is decimal)
		{
			return (decimal)m / (decimal)n;
		}
		return null;
	}

	public static object Modulus(object m, object n)
	{
		CoerceTypes(ref m, ref n);
		if (n is int)
		{
			return (int)m % (int)n;
		}
		if (n is short)
		{
			return (short)m % (short)n;
		}
		if (n is long)
		{
			return (long)m % (long)n;
		}
		if (n is ushort)
		{
			return (int)(ushort)m % (int)(ushort)n;
		}
		if (n is uint)
		{
			return (uint)m % (uint)n;
		}
		if (n is ulong)
		{
			return (ulong)m % (ulong)n;
		}
		if (n is byte)
		{
			return (int)(byte)m % (int)(byte)n;
		}
		if (n is sbyte)
		{
			return (sbyte)m % (sbyte)n;
		}
		if (n is float)
		{
			return (float)m % (float)n;
		}
		if (n is double)
		{
			return (double)m % (double)n;
		}
		if (n is decimal)
		{
			return (decimal)m % (decimal)n;
		}
		return null;
	}

	public static object Power(object m, object n)
	{
		return Math.Pow(Convert.ToDouble(m), Convert.ToDouble(n));
	}

	public static void CoerceTypes(ref object m, ref object n)
	{
		TypeCode typeCode = System.Convert.GetTypeCode(m);
		TypeCode typeCode2 = System.Convert.GetTypeCode(n);
		if (typeCode > typeCode2)
		{
			n = System.Convert.ChangeType(n, typeCode, null);
		}
		else
		{
			m = System.Convert.ChangeType(m, typeCode2, null);
		}
	}

	public static int HexToInt(char h)
	{
		if (h >= '0' && h <= '9')
		{
			return h - 48;
		}
		if (h >= 'a' && h <= 'f')
		{
			return h - 97 + 10;
		}
		if (h >= 'A' && h <= 'F')
		{
			return h - 65 + 10;
		}
		return -1;
	}

	public static char IntToHex(int n)
	{
		if (n <= 9)
		{
			return (char)(n + 48);
		}
		return (char)(n - 10 + 97);
	}
}
