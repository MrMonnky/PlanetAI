using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspVote : MspBaseHttp
{
	public int ThemeId { get; internal set; }

	public int ContentType { get; internal set; }

	public int IdA { get; internal set; }

	public int IdB { get; internal set; }

	public int ActorIdA { get; internal set; }

	public int ActorIdB { get; internal set; }

	public ulong ScoreA { get; internal set; }

	public ulong ScoreB { get; internal set; }

	public string UsernameA { get; internal set; }

	public string UsernameB { get; internal set; }

	internal MspVote()
	{
	}

	public async Task<MspResult<object>> VoteAsync()
	{
		return await MovieStarPlanet.VoteItemAsync(this);
	}
}
