using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Dolo.PlanetAI;

internal static class Extension
{
	public static string Build(this string endpoint, string method)
	{
		return endpoint + "/Gateway.aspx?method=" + method;
	}

	public static string Add(this string endpoint, string data)
	{
		return endpoint += data;
	}

	public static bool HasCode(this HttpStatusCode code, params HttpStatusCode[] codes)
	{
		return codes.Contains(code);
	}

	public static T GetAction<T>(this Action<T> act)
	{
		T val = (T)Activator.CreateInstance(typeof(T));
		act(val);
		return val;
	}

	public static byte[] ToArray(this Image image)
	{
		using MemoryStream memoryStream = new MemoryStream();
		image.Save(memoryStream, image.RawFormat);
		return memoryStream.ToArray();
	}

	public static bool TryToSafeBase64(this string token, out string data)
	{
		try
		{
			token = token.Split('.')[1];
			if (token.Length % 4 != 0)
			{
				token += "===".Substring(0, 4 - token.Length % 4);
			}
			data = Encoding.UTF8.GetString(Convert.FromBase64String(token.Replace("-", "+").Replace("_", "\\")));
			return true;
		}
		catch
		{
			data = null;
			return false;
		}
	}

	public static string GetChatServerSubDomain(Server s)
	{
		if (1 == 0)
		{
		}
		string result = ((s != Server.UnitedStates && (uint)(s - 11) > 4u) ? "chatroom" : "chatroom-us");
		if (1 == 0)
		{
		}
		return result;
	}
}
