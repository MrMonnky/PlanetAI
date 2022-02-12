using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreActorTmp
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }

	[JsonProperty("Money", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Money { get; set; }

	[JsonProperty("Fame", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fame { get; set; }

	[JsonProperty("Fortune", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fortune { get; set; }

	[JsonProperty("FriendCount", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCount { get; set; }

	[JsonProperty("IsExtra", NullValueHandling = NullValueHandling.Ignore)]
	public int IsExtra { get; set; }

	[JsonProperty("RoomLikes", NullValueHandling = NullValueHandling.Ignore)]
	public int RoomLikes { get; set; }

	[JsonProperty("LastLogin", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastLogin { get; set; }

	[JsonProperty("MembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedDate { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("VipTier", NullValueHandling = NullValueHandling.Ignore)]
	public int? VipTier { get; set; }

	[JsonProperty("Moderator", NullValueHandling = NullValueHandling.Ignore)]
	public int Moderator { get; set; }

	[JsonProperty("EverythingIsAwesome", NullValueHandling = NullValueHandling.Ignore)]
	public bool? EverythingIsAwesome { get; set; }
}
