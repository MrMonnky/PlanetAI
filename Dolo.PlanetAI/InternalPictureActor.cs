using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPictureActor
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("VipTier")]
	public int? VipTier { get; set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }
}
