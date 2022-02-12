using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreArtbookUserTmp
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("MembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedAt { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutAt { get; set; }

	[JsonProperty("Moderator", NullValueHandling = NullValueHandling.Ignore)]
	public int Moderator { get; set; }

	[JsonProperty("VipTier")]
	public int? VipTier { get; set; }
}
