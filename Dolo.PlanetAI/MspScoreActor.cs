using System;
using System.Threading.Tasks;
using Dolo.PlanetAI.Entities;

namespace Dolo.PlanetAI;

public sealed class MspScoreActor : MspBaseActor
{
	public DateTime LastLoginAt { get; internal set; }

	public DateTime MembershipPurchasedAt { get; internal set; }

	public DateTime MembershipTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public int Id { get; internal set; }

	public int Friends { get; internal set; }

	public int RoomLikes { get; internal set; }

	public int Level { get; internal set; }

	public string Username { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Fame { get; internal set; }

	public ulong Fortune { get; internal set; }

	public bool IsModerator { get; internal set; }

	public bool IsDeleted { get; internal set; }

	internal MspScoreActor()
	{
	}

	public async Task<MspResult<DateTime>> GetCreatedAtAsync()
	{
		return await MovieStarPlanet.GetActorCreatedAtAsync(Id);
	}

	public async Task<MspStatus> GetStatusAsync()
	{
		return await MovieStarPlanet.GetActorStatusAsync(Id);
	}

	public async Task<MspList<MspBoonster>> GetBoonstersAsync()
	{
		return await MovieStarPlanet.GetActorBoonstersAsync(Id);
	}
}
