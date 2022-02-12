using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal abstract class StringUtils
{
	public const string CarriageReturnLineFeed = "\r\n";

	public const string Empty = "";

	public const char CarriageReturn = '\r';

	public const char LineFeed = '\n';

	public const char Tab = '\t';

	public static bool ContainsWhiteSpace(string s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		for (int i = 0; i < s.Length; i++)
		{
			if (char.IsWhiteSpace(s[i]))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsWhiteSpace(string s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (s.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < s.Length; i++)
		{
			if (!char.IsWhiteSpace(s[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static string EnsureEndsWith(string target, string value)
	{
		if (target == null)
		{
			throw new ArgumentNullException("target");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (target.Length >= value.Length)
		{
			if (string.Compare(target, target.Length - value.Length, value, 0, value.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return target;
			}
			string text = target.TrimEnd(null);
			if (string.Compare(text, text.Length - value.Length, value, 0, value.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return target;
			}
		}
		return target + value;
	}

	public static bool IsNullOrEmptyOrWhiteSpace(string s)
	{
		if (IsNullOrEmpty(s))
		{
			return true;
		}
		if (IsWhiteSpace(s))
		{
			return true;
		}
		return false;
	}

	public static bool IsNullOrEmpty(string s)
	{
		return s == null || s == string.Empty;
	}

	public static string NullEmptyString(string s)
	{
		return IsNullOrEmpty(s) ? null : s;
	}

	public static string MaskNullString(string actual)
	{
		return (actual == null) ? string.Empty : actual;
	}

	public static string MaskNullString(string actual, string mask)
	{
		return (actual == null) ? mask : actual;
	}

	public static string MaskEmptyString(string actual, string emptyValue)
	{
		return (MaskNullString(actual).Length == 0) ? emptyValue : actual;
	}

	public static string ReplaceNewLines(string s, string replacement)
	{
		StringReader stringReader = new StringReader(s);
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		string value;
		while ((value = stringReader.ReadLine()) != null)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append(replacement);
			}
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	public static string Truncate(string s, int maximumLength)
	{
		return Truncate(s, maximumLength, "...");
	}

	public static string Truncate(string s, int maximumLength, string suffix)
	{
		if (suffix == null)
		{
			throw new ArgumentNullException("suffix");
		}
		if (maximumLength <= 0)
		{
			throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
		}
		int num = maximumLength - suffix.Length;
		if (num <= 0)
		{
			throw new ArgumentException("Length of suffix string is greater or equal to maximumLength");
		}
		if (s != null && s.Length > maximumLength)
		{
			string text = s.Substring(0, num);
			text = text.Trim();
			return text + suffix;
		}
		return s;
	}

	public static StringWriter CreateStringWriter(int capacity)
	{
		StringBuilder sb = new StringBuilder(capacity);
		return new StringWriter(sb);
	}

	public static int GetLength(string value)
	{
		return value?.Length ?? 0;
	}

	public static string ToCharAsUnicode(char c)
	{
		using StringWriter stringWriter = new StringWriter();
		WriteCharAsUnicode(stringWriter, c);
		return stringWriter.ToString();
	}

	public static void WriteCharAsUnicode(TextWriter writer, char c)
	{
		ValidationUtils.ArgumentNotNull(writer, "writer");
		char value = NumberUtils.IntToHex(((int)c >> 12) & 0xF);
		char value2 = NumberUtils.IntToHex(((int)c >> 8) & 0xF);
		char value3 = NumberUtils.IntToHex(((int)c >> 4) & 0xF);
		char value4 = NumberUtils.IntToHex(c & 0xF);
		writer.Write('\\');
		writer.Write('u');
		writer.Write(value);
		writer.Write(value2);
		writer.Write(value3);
		writer.Write(value4);
	}

	public static bool HasText(string target)
	{
		if (target == null)
		{
			return false;
		}
		return HasLength(target.Trim());
	}

	public static bool HasLength(string target)
	{
		return target != null && target.Length > 0;
	}

	public static bool CaselessEquals(string a, string b)
	{
		return string.Compare(a, b, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0;
	}
}
