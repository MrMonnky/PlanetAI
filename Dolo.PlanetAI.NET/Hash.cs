using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Dolo.PlanetAI.NET.Fluorine.AMF3;

namespace Dolo.PlanetAI.NET;

internal static class Hash
{
	private static readonly Random Rnd = new Random();

	public static string HashContent(object[] obj, bool encrypted = false)
	{
		return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(ConvertObjectArray(obj) + (encrypted ? GetTicketValue(obj) : "v1n3g4r") + "$CuaS44qoi0Mp2qp"))).Replace("-", "").ToLower();
	}

	public static string HashContent(string username, string password, string server, NebulaGameType gameid, string usefulparameter = "false")
	{
		HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes("7jA7^kAZSHtjxDAa"));
		Encoding uTF = Encoding.UTF8;
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 5);
		defaultInterpolatedStringHandler.AppendFormatted(gameid.ToString().Replace("MSP_", ""));
		defaultInterpolatedStringHandler.AppendFormatted(server);
		defaultInterpolatedStringHandler.AppendFormatted(password);
		defaultInterpolatedStringHandler.AppendFormatted(username);
		defaultInterpolatedStringHandler.AppendFormatted(usefulparameter);
		return BitConverter.ToString(hMACSHA.ComputeHash(uTF.GetBytes(defaultInterpolatedStringHandler.ToStringAndClear()))).Replace("-", "").ToLower();
	}

	private static string GetTicketValue(object[] ob)
	{
		string result = "v1n3g4r";
		foreach (object obj in ob)
		{
			if (obj is MspTicket mspTicket)
			{
				result = mspTicket.Ticket.Split(',').Last().Substring(0, 5);
			}
		}
		return result;
	}

	public static string HashID()
	{
		string text = "";
		do
		{
			text += Rnd.Next() * int.MaxValue;
		}
		while (text.Length < 48);
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(text.Substring(0, 46)));
	}

	private static string GetTypeObject(object Obj)
	{
		string text = string.Empty;
		PropertyInfo[] properties = Obj.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			if (!(propertyInfo.Name == "Length") && !(propertyInfo.Name == "LongLength") && !(propertyInfo.Name == "Rank") && !(propertyInfo.Name == "SyncRoot") && !(propertyInfo.Name == "IsReadOnly") && !(propertyInfo.Name == "IsFixedSize") && !(propertyInfo.Name == "IsSynchronized"))
			{
				text += GetObjectValue(propertyInfo.GetValue(Obj, null));
			}
		}
		return text;
	}

	public static object[] ToObjectArray(string data)
	{
		List<string> list = new List<string>();
		list.AddRange(data.Split('\n', '\r'));
		return list.ToArray();
	}

	public static string GetObjectValue(object Obj)
	{
		if (!(Obj is MspTicket) && Obj != null)
		{
			if (!(Obj is int) && !(Obj is string))
			{
				if (!(Obj is bool))
				{
					if (!(Obj is DateTime))
					{
						if (!(Obj is byte[]))
						{
							if (!(Obj is object[]))
							{
								if (!(Obj is ArrayCollection))
								{
									if (Obj is Array)
									{
										return ConvertObjectArray((Array)Obj);
									}
									if (Obj.GetType().IsGenericType)
									{
										IList list = (IList)Obj;
										Array array = new object[list.Count];
										list.CopyTo(array, 0);
										return GetObjectValue(array);
									}
									return (Obj != null) ? GetTypeObject(Obj) : "";
								}
								return ConvertObjectArray((ArrayCollection)Obj);
							}
							return ConvertObjectArray((object[])Obj);
						}
						return ConvertObjectArray((byte[])Obj);
					}
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 3);
					defaultInterpolatedStringHandler.AppendFormatted(((DateTime)Obj).Year);
					defaultInterpolatedStringHandler.AppendFormatted(DateTime.UtcNow.AddMonths(-1).Month);
					defaultInterpolatedStringHandler.AppendFormatted(DateTime.UtcNow.Day);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return Convert.ToBoolean(Obj) ? "True" : "False";
			}
			return Obj.ToString();
		}
		return "";
	}

	private static string ConvertObjectArray(object[] o)
	{
		string text = string.Empty;
		foreach (object obj in o)
		{
			text += GetObjectValue(obj);
		}
		return text;
	}

	private static string ConvertObjectArray(Array o)
	{
		string text = string.Empty;
		foreach (object item in o)
		{
			text += GetObjectValue(item);
		}
		return text;
	}

	private static string ConvertObjectArray(ArrayCollection o)
	{
		string text = string.Empty;
		foreach (object item in o)
		{
			text += GetObjectValue(item);
		}
		return text;
	}

	private static string ConvertObjectArray(byte[] o)
	{
		if (o.Length <= 20)
		{
			return BitConverter.ToString(o.ToArray()).Replace("-", "").ToLower();
		}
		using MemoryStream memoryStream = new MemoryStream(o);
		List<byte> list = new List<byte>();
		for (int i = 0; i < 20; i++)
		{
			memoryStream.Position = o.Length / 20 * i;
			list.Add(new BinaryReader(memoryStream).ReadByte());
		}
		return BitConverter.ToString(list.ToArray()).Replace("-", "").ToLower();
	}
}
