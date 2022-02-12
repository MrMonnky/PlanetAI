using System.Linq;
using System.Reflection;

namespace Dolo.PlanetAI.NET.Utils;

internal static class AMFAttribute
{
	public static (string method, bool isTicketRequired) GetAttribute(this string method)
	{
		object obj = (from a in (from a in typeof(MspClient)?.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where a.Name == method
				select a).FirstOrDefault().GetCustomAttributes(inherit: false)
			where a is AMFMethod
			select a).FirstOrDefault();
		if (1 == 0)
		{
		}
		(string, bool) result = ((!(obj is AMFMethod aMFMethod)) ? (string.Empty, false) : (aMFMethod.Name, aMFMethod.IsTicketRequired));
		if (1 == 0)
		{
		}
		return result;
	}
}
