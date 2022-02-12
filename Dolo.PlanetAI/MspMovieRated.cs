namespace Dolo.PlanetAI;

public sealed class MspMovieRated : MspBaseHttp
{
	public bool HasRated { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Fame { get; internal set; }
}
