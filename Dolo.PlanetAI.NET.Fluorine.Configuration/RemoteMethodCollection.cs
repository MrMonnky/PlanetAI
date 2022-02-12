using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RemoteMethodCollection : CollectionBase<RemoteMethod>
{
	private Dictionary<string, string> _methods = new Dictionary<string, string>(3);

	private Dictionary<string, string> _methodsNames = new Dictionary<string, string>(3);

	public override void Add(RemoteMethod value)
	{
		_methods[value.Name] = value.Method;
		_methodsNames[value.Method] = value.Name;
		base.Add(value);
	}

	public override void Insert(int index, RemoteMethod value)
	{
		_methods[value.Name] = value.Method;
		_methodsNames[value.Method] = value.Name;
		base.Insert(index, value);
	}

	public string GetMethod(string name)
	{
		if (_methods.ContainsKey(name))
		{
			return _methods[name];
		}
		return name;
	}

	public string GetMethodName(string method)
	{
		if (_methodsNames.ContainsKey(method))
		{
			return _methodsNames[method];
		}
		return method;
	}
}
