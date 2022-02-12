using System;

namespace Dolo.PlanetAI.NET.Utils;

[AttributeUsage(AttributeTargets.Method)]
internal class AMFMethod : Attribute
{
	public string Name { get; set; }

	public bool IsTicketRequired { get; set; }

	public AMFMethod(string name)
	{
		Name = name;
	}
}
