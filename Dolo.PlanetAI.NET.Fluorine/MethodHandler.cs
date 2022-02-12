using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;

namespace Dolo.PlanetAI.NET.Fluorine;

internal sealed class MethodHandler
{
	private MethodHandler()
	{
	}

	public static MethodInfo GetMethod(Type type, string methodName, IList arguments)
	{
		return GetMethod(type, methodName, arguments, exactMatch: false);
	}

	public static MethodInfo GetMethod(Type type, string methodName, IList arguments, bool exactMatch)
	{
		return GetMethod(type, methodName, arguments, exactMatch, throwException: true);
	}

	public static MethodInfo GetMethod(Type type, string methodName, IList arguments, bool exactMatch, bool throwException)
	{
		return GetMethod(type, methodName, arguments, exactMatch, throwException, traceError: true);
	}

	public static MethodInfo GetMethod(Type type, string methodName, IList arguments, bool exactMatch, bool throwException, bool traceError)
	{
		MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		List<MethodInfo> list = new List<MethodInfo>();
		foreach (MethodInfo methodInfo in methods)
		{
			if (methodInfo.Name == methodName && ((methodInfo.GetParameters().Length == 0 && arguments == null) || (arguments != null && methodInfo.GetParameters().Length == arguments.Count)))
			{
				list.Add(methodInfo);
			}
		}
		if (list.Count > 0)
		{
			for (int num = list.Count - 1; num >= 0; num--)
			{
				MethodInfo methodInfo2 = list[num];
				ParameterInfo[] parameters = methodInfo2.GetParameters();
				bool flag = true;
				for (int j = 0; j < parameters.Length; j++)
				{
					ParameterInfo parameterInfo = parameters[j];
					if (!exactMatch)
					{
						if (!TypeHelper.IsAssignable(arguments[j], parameterInfo.ParameterType))
						{
							flag = false;
							break;
						}
					}
					else if (arguments[j] == null || arguments[j]!.GetType() != parameterInfo.ParameterType)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					list.Remove(methodInfo2);
				}
			}
		}
		if (list.Count == 0)
		{
			string @string = __Res.GetString("Invocation_NoSuitableMethod", methodName);
			if (throwException)
			{
				throw new AMFException(@string);
			}
		}
		if (list.Count > 1)
		{
			string string2 = __Res.GetString("Invocation_Ambiguity", methodName);
			if (throwException)
			{
				throw new AMFException(string2);
			}
		}
		if (list.Count > 0)
		{
			return list[0];
		}
		return null;
	}
}
