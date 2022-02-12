using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class ServiceCollection : CollectionBase<ServiceConfiguration>
{
	private Dictionary<string, ServiceConfiguration> _serviceNames = new Dictionary<string, ServiceConfiguration>(5);

	private Dictionary<string, ServiceConfiguration> _serviceLocations = new Dictionary<string, ServiceConfiguration>(5);

	public override void Add(ServiceConfiguration value)
	{
		_serviceNames[value.Name] = value;
		_serviceLocations[value.ServiceLocation] = value;
		base.Add(value);
	}

	public override void Insert(int index, ServiceConfiguration value)
	{
		_serviceNames[value.Name] = value;
		_serviceLocations[value.ServiceLocation] = value;
		base.Insert(index, value);
	}

	public override bool Remove(ServiceConfiguration value)
	{
		_serviceNames.Remove(value.Name);
		_serviceLocations.Remove(value.ServiceLocation);
		return base.Remove(value);
	}

	public bool Contains(string serviceName)
	{
		return _serviceNames.ContainsKey(serviceName);
	}

	public string GetServiceLocation(string serviceName)
	{
		if (_serviceNames.ContainsKey(serviceName))
		{
			return _serviceNames[serviceName].ServiceLocation;
		}
		return serviceName;
	}

	public string GetServiceName(string serviceLocation)
	{
		if (_serviceLocations.ContainsKey(serviceLocation))
		{
			return _serviceLocations[serviceLocation].Name;
		}
		return serviceLocation;
	}

	public string GetMethod(string serviceName, string name)
	{
		ServiceConfiguration serviceConfiguration = null;
		if (_serviceNames.ContainsKey(serviceName))
		{
			serviceConfiguration = _serviceNames[serviceName];
		}
		if (serviceConfiguration != null)
		{
			return serviceConfiguration.Methods.GetMethod(name);
		}
		return name;
	}

	public string GetMethodName(string serviceLocation, string method)
	{
		ServiceConfiguration serviceConfiguration = null;
		if (_serviceLocations.ContainsKey(serviceLocation))
		{
			serviceConfiguration = _serviceLocations[serviceLocation];
		}
		if (serviceConfiguration != null)
		{
			return serviceConfiguration.Methods.GetMethodName(method);
		}
		return method;
	}
}
