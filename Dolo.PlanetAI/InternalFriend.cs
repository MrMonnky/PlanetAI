using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal sealed class InternalFriend
{
	[JsonProperty("actorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }

	[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
	public int Status { get; set; }

	[JsonProperty("recentlyLoggedIn", NullValueHandling = NullValueHandling.Ignore)]
	public bool RecentlyLoggedIn { get; set; }

	[JsonProperty("membershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("VipTier")]
	public int? VipTier { get; set; }

	[JsonProperty("money", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Money { get; set; }

	[JsonProperty("fame", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fame { get; set; }

	[JsonProperty("fortune", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fortune { get; set; }

	[JsonProperty("friendCount", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCount { get; set; }

	[JsonProperty("membershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedDate { get; set; }

	[JsonProperty("lastLogin", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastLogin { get; set; }

	[JsonProperty("moderator", NullValueHandling = NullValueHandling.Ignore)]
	public int Moderator { get; set; }

	[JsonProperty("nebulaProfileId", NullValueHandling = NullValueHandling.Ignore)]
	public string NebulaProfileId { get; set; }
}
