using System;
using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal class ServiceContainer : IServiceContainer, IServiceProvider
{
	private Dictionary<Type, object> _services = new Dictionary<Type, object>();

	private IServiceProvider _parentProvider;

	private IServiceContainer Container
	{
		get
		{
			IServiceContainer result = null;
			if (_parentProvider != null)
			{
				result = (IServiceContainer)_parentProvider.GetService(typeof(IServiceContainer));
			}
			return result;
		}
	}

	public object SyncRoot => ((ICollection)_services).SyncRoot;

	public ServiceContainer()
		: this(null)
	{
	}

	public ServiceContainer(IServiceProvider parentProvider)
	{
		_parentProvider = parentProvider;
	}

	public void AddService(Type serviceType, object service)
	{
		AddService(serviceType, service, promote: false);
	}

	public void AddService(Type serviceType, object service, bool promote)
	{
		ValidationUtils.ArgumentNotNull(serviceType, "serviceType");
		ValidationUtils.ArgumentNotNull(service, "service");
		lock (SyncRoot)
		{
			if (promote)
			{
				IServiceContainer container = Container;
				if (container != null)
				{
					container.AddService(serviceType, service, promote);
					return;
				}
			}
			if (_services.ContainsKey(serviceType))
			{
				throw new ArgumentException($"Service {serviceType.FullName} already exists");
			}
			_services[serviceType] = service;
		}
	}

	public void RemoveService(Type serviceType)
	{
		RemoveService(serviceType, promote: false);
	}

	public void RemoveService(Type serviceType, bool promote)
	{
		ValidationUtils.ArgumentNotNull(serviceType, "serviceType");
		lock (SyncRoot)
		{
			if (promote)
			{
				IServiceContainer container = Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (_services.ContainsKey(serviceType) && _services[serviceType] is IService service)
			{
				service.Shutdown();
			}
			_services.Remove(serviceType);
		}
	}

	public object GetService(Type serviceType)
	{
		ValidationUtils.ArgumentNotNull(serviceType, "serviceType");
		object obj = null;
		lock (SyncRoot)
		{
			if (_services.ContainsKey(serviceType))
			{
				obj = _services[serviceType];
			}
			if (obj == null && _parentProvider != null)
			{
				obj = _parentProvider.GetService(serviceType);
			}
		}
		return obj;
	}

	internal void Shutdown()
	{
		lock (SyncRoot)
		{
			foreach (object value in _services.Values)
			{
				if (value is IService service)
				{
					service.Shutdown();
				}
			}
			_services.Clear();
			_services = null;
			_parentProvider = null;
		}
	}
}
