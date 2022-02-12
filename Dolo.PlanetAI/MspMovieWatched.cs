namespace Dolo.PlanetAI;

public sealed class MspMovieWatched : MspBaseHttp
{
	public bool HasWatched { get; internal set; }

	public ulong Fame { get; internal set; }
}
