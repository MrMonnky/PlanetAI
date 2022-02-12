using System.Threading;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class AMFConfiguration
{
	private static object _objLock = new object();

	private static AMFConfiguration _instance;

	private static AMFSettings _amfSettings;

	public static AMFConfiguration Instance
	{
		get
		{
			if (_instance == null)
			{
				lock (_objLock)
				{
					if (_instance == null)
					{
						AMFConfiguration instance = new AMFConfiguration();
						_amfSettings = new AMFSettings();
						Thread.MemoryBarrier();
						_instance = instance;
					}
				}
			}
			return _instance;
		}
	}

	public AMFSettings AMFSettings => _amfSettings;

	internal ServiceCollection ServiceMap
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.Services;
			}
			return null;
		}
	}

	internal ClassMappingCollection ClassMappings => _amfSettings.ClassMappings;

	public NullableTypeCollection NullableValues
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.Nullables;
			}
			return null;
		}
	}

	public bool AcceptNullValueTypes
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.AcceptNullValueTypes;
			}
			return false;
		}
	}

	public RemotingServiceAttributeConstraint RemotingServiceAttributeConstraint
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.RemotingServiceAttribute;
			}
			return RemotingServiceAttributeConstraint.Access;
		}
	}

	public TimezoneCompensation TimezoneCompensation
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.TimezoneCompensation;
			}
			return TimezoneCompensation.None;
		}
	}

	public HttpCompressSettings HttpCompressSettings
	{
		get
		{
			if (_amfSettings != null && _amfSettings.HttpCompressSettings != null)
			{
				return _amfSettings.HttpCompressSettings;
			}
			return HttpCompressSettings.Default;
		}
	}

	internal OptimizerSettings OptimizerSettings
	{
		get
		{
			if (_amfSettings != null)
			{
				return _amfSettings.Optimizer;
			}
			return null;
		}
	}

	private AMFConfiguration()
	{
	}

	internal string GetServiceName(string serviceLocation)
	{
		if (ServiceMap != null)
		{
			return ServiceMap.GetServiceName(serviceLocation);
		}
		return serviceLocation;
	}

	internal string GetServiceLocation(string serviceName)
	{
		if (ServiceMap != null)
		{
			return ServiceMap.GetServiceLocation(serviceName);
		}
		return serviceName;
	}

	internal string GetMethodName(string serviceLocation, string method)
	{
		if (ServiceMap != null)
		{
			return ServiceMap.GetMethodName(serviceLocation, method);
		}
		return method;
	}

	internal string GetMappedTypeName(string customClass)
	{
		if (ClassMappings != null)
		{
			return ClassMappings.GetType(customClass);
		}
		return customClass;
	}

	internal string GetCustomClass(string type)
	{
		if (ClassMappings != null)
		{
			return ClassMappings.GetCustomClass(type);
		}
		return type;
	}
}
