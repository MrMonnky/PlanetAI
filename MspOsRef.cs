using System;

namespace Dolo.PlanetAI;

public sealed class MspOsRef : MspBaseHttp
{
	public string RefId { get; internal set; }

	public string Data { get; internal set; }

	public DateTime Created { get; internal set; }

	internal MspOsRef()
	{
	}
}
