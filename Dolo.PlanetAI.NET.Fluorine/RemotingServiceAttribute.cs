using System;

namespace Dolo.PlanetAI.NET.Fluorine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal sealed class RemotingServiceAttribute : Attribute
{
	private string _serviceName;

	public RemotingServiceAttribute()
	{
	}

	public RemotingServiceAttribute(string serviceName)
	{
		_serviceName = serviceName;
	}
}
