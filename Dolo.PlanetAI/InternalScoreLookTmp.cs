using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreLookTmp
{
	[JsonProperty("LookId", NullValueHandling = NullValueHandling.Ignore)]
	public int LookId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("actorName", NullValueHandling = NullValueHandling.Ignore)]
	public string ActorName { get; set; }

	[JsonProperty("CreatorId", NullValueHandling = NullValueHandling.Ignore)]
	public int CreatorId { get; set; }

	[JsonProperty("creatorName", NullValueHandling = NullValueHandling.Ignore)]
	public string CreatorName { get; set; }

	[JsonProperty("Created", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime Created { get; set; }

	[JsonProperty("Headline", NullValueHandling = NullValueHandling.Ignore)]
	public string Headline { get; set; }

	[JsonProperty("Likes", NullValueHandling = NullValueHandling.Ignore)]
	public int Likes { get; set; }

	[JsonProperty("Sells", NullValueHandling = NullValueHandling.Ignore)]
	public int Sells { get; set; }

	[JsonProperty("actorMembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime ActorMembershipPurchasedDate { get; set; }

	[JsonProperty("actorMembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime ActorMembershipTimeoutDate { get; set; }

	[JsonProperty("creatorMembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime CreatorMembershipPurchasedDate { get; set; }

	[JsonProperty("creatorMembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime CreatorMembershipTimeoutDate { get; set; }

	[JsonProperty("actorVipTier", NullValueHandling = NullValueHandling.Ignore)]
	public long ActorVipTier { get; set; }

	[JsonProperty("creatorVipTier", NullValueHandling = NullValueHandling.Ignore)]
	public int? CreatorVipTier { get; set; }
}
