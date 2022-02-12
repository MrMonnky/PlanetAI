using System;

namespace Dolo.PlanetAI;

public sealed class MspPiggy : MspBase
{
	public DateTime LastUpdatedAt { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Diamonds { get; internal set; }

	public ulong Fame { get; internal set; }

	internal MspPiggy()
	{
	}

	internal MspPiggy(ulong starcoins, ulong fame, ulong diamond)
	{
		StarCoins = starcoins;
		Fame = fame;
		Diamonds = diamond;
	}
}
