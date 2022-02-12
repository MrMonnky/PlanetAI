using System;
using System.Security.Cryptography;
using System.Text;

namespace Dolo.PlanetAI.NET;

internal static class Ticket
{
	private static readonly object tmpObject = new object();

	public static MspTicket GetTicket()
	{
		lock (tmpObject)
		{
			MspTicket mspTicket = new MspTicket();
			using (MD5 mD = MD5.Create())
			{
				Random random = new Random();
				string text = random.Next(100, 1000).ToString();
				mspTicket.Ticket = BitConverter.ToString(mD.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "").ToLower() + Secret(text);
			}
			return mspTicket;
		}
	}

	private static string Secret(string data)
	{
		string text = string.Empty;
		byte[] bytes = Encoding.UTF8.GetBytes(data);
		for (int i = 0; i < bytes.Length; i++)
		{
			text += Convert.ToString(bytes[i], 16);
		}
		return text;
	}
}
