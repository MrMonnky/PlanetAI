using System;

namespace Dolo.PlanetAI;

public sealed class MspStatusUpdate : MspBaseHttp
{
	public DateTime ChangedAt { get; internal set; }

	public Status Status { get; internal set; }

	public bool Updated { get; internal set; }

	internal MspStatusUpdate()
	{
	}
}
