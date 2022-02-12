using System;
using System.Collections.Generic;
using System.Reflection;
using Dolo.PlanetAI.NET.Fluorine.Configuration;

namespace Dolo.PlanetAI.NET.Fluorine;

internal class ObjectFactory
{
	private static Dictionary<string, Type> _typeCache;

	private static string[] _lacLocations;

	static ObjectFactory()
	{
		_typeCache = new Dictionary<string, Type>();
		_lacLocations = TypeHelper.GetLacLocations();
	}

	public static Type Locate(string typeName)
	{
		if (typeName == null || typeName == string.Empty)
		{
			return null;
		}
		string text = typeName;
		text = AMFConfiguration.Instance.GetMappedTypeName(typeName);
		lock (typeof(Type))
		{
			Type type = null;
			if (_typeCache.ContainsKey(text))
			{
				type = _typeCache[text];
			}
			if (type == null)
			{
				type = TypeHelper.Locate(text);
				if (type != null)
				{
					_typeCache[text] = type;
					return type;
				}
				type = LocateInLac(text);
			}
			return type;
		}
	}

	public static Type LocateInLac(string typeName)
	{
		if (typeName == null || typeName == string.Empty)
		{
			return null;
		}
		string text = typeName;
		text = AMFConfiguration.Instance.GetMappedTypeName(typeName);
		lock (typeof(Type))
		{
			Type type = null;
			if (_typeCache.ContainsKey(text))
			{
				type = _typeCache[text];
			}
			if (type == null)
			{
				for (int i = 0; i < _lacLocations.Length; i++)
				{
					type = TypeHelper.LocateInLac(text, _lacLocations[i]);
					if (type != null)
					{
						_typeCache[text] = type;
						return type;
					}
				}
			}
			return type;
		}
	}

	internal static void AddTypeToCache(Type type)
	{
		if (type != null)
		{
			lock (typeof(Type))
			{
				_typeCache[type.FullName] = type;
			}
		}
	}

	public static bool ContainsType(string typeName)
	{
		if (typeName != null)
		{
			lock (typeof(Type))
			{
				return _typeCache.ContainsKey(typeName);
			}
		}
		return false;
	}

	public static object CreateInstance(Type type)
	{
		return CreateInstance(type, null);
	}

	public static object CreateInstance(Type type, object[] args)
	{
		if (type != null)
		{
			lock (typeof(Type))
			{
				if (type.IsAbstract && type.IsSealed)
				{
					return type;
				}
				if (args == null)
				{
					return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[0], null);
				}
				return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null);
			}
		}
		return null;
	}

	public static object CreateInstance(string typeName)
	{
		return CreateInstance(typeName, null);
	}

	public static object CreateInstance(string typeName, object[] args)
	{
		Type type = Locate(typeName);
		return CreateInstance(type, args);
	}
}
