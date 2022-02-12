using System;

namespace Dolo.PlanetAI;

public sealed class MspAutograph : MspBaseHttp
{
	public DateTime SendedAt { get; internal set; }

	public ulong Fame { get; internal set; }

	public bool HasSended { get; internal set; }

	internal MspAutograph()
	{
	}
}
