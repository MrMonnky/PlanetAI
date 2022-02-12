using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspFriend : MspBaseActor
{
	public DateTime MembershipTimeoutAt { get; internal set; }

	public DateTime MembershipPurchasedAt { get; internal set; }

	public DateTime LastLoginAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public string ProfileId { get; internal set; }

	public string Username { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public int Id { get; internal set; }

	public int Friends { get; internal set; }

	public int Level { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Fame { get; internal set; }

	public ulong Fortune { get; internal set; }

	public bool IsModerator { get; internal set; }

	public bool RecentlyLoggedIn { get; internal set; }

	public bool IsVip { get; internal set; }

	internal MspFriend()
	{
	}

	public async Task<MspResult<bool>> DeleteAsync()
	{
		return await MovieStarPlanet.DeleteFriendAsync(Id);
	}
}
