namespace Dolo.PlanetAI;

public sealed class MspLike : MspBaseHttp
{
	public bool HasLiked { get; internal set; }

	public ulong Fame { get; internal set; }

	public ulong StarCoins { get; internal set; }

	internal MspLike()
	{
	}
}
