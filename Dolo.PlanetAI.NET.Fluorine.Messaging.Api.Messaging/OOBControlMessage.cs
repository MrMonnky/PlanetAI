using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Messaging;

internal class OOBControlMessage
{
	private string _target;

	private string _serviceName;

	private Dictionary<string, object> _serviceParameterMap;

	private object _result;

	public string ServiceName
	{
		get
		{
			return _serviceName;
		}
		set
		{
			_serviceName = value;
		}
	}

	public Dictionary<string, object> ServiceParameterMap
	{
		get
		{
			if (_serviceParameterMap == null)
			{
				_serviceParameterMap = new Dictionary<string, object>();
			}
			return _serviceParameterMap;
		}
		set
		{
			_serviceParameterMap = value;
		}
	}

	public string Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	public object Result
	{
		get
		{
			return _result;
		}
		set
		{
			_result = value;
		}
	}
}
