using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspScoreMovie : MspBase
{
	public DateTime PublishedAt { get; internal set; }

	public DateTime MembershipPurchasedAt { get; internal set; }

	public DateTime MembershipTimeoutAt { get; internal set; }

	public double Rating { get; internal set; }

	public int ActorId { get; internal set; }

	public int Id { get; internal set; }

	public string Name { get; internal set; }

	public string Username { get; internal set; }

	public int RatedCount { get; internal set; }

	public int StarCoinsEarned { get; internal set; }

	public int State { get; internal set; }

	public int WatchedCount { get; internal set; }

	public int Guid { get; internal set; }

	public bool HasWatched { get; internal set; }

	public bool HasRated { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public string MovieUrl { get; internal set; }

	internal MspScoreMovie()
	{
	}

	public async Task<MspLike> LikeAsync()
	{
		return await MovieStarPlanet.LikeAddAsync(Id, ActorId, LikeType.movie);
	}
}
