using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspScoreLook : MspBase
{
	public DateTime CreatedAt { get; internal set; }

	public DateTime MembershipPurchasedAt { get; internal set; }

	public DateTime MembershipTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public int Id { get; internal set; }

	public int ActorId { get; internal set; }

	public string Username { get; internal set; }

	public string Name { get; internal set; }

	public int Likes { get; internal set; }

	public int Sells { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public string LookUrl { get; internal set; }

	internal MspScoreLook()
	{
	}

	public async Task<MspLike> LikeAsync()
	{
		return await MovieStarPlanet.LikeAddAsync(Id, ActorId, LikeType.look);
	}
}
