using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScorePetTmp
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("ActorName", NullValueHandling = NullValueHandling.Ignore)]
	public string Username { get; set; }

	[JsonProperty("BonsterId", NullValueHandling = NullValueHandling.Ignore)]
	public int BonsterId { get; set; }

	[JsonProperty("BonsterName", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("ActorBonsterRelId", NullValueHandling = NullValueHandling.Ignore)]
	public int Id { get; set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }

	[JsonProperty("Experience", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Experience { get; set; }

	[JsonProperty("ColorPalette", NullValueHandling = NullValueHandling.Ignore)]
	public string ColorPalette { get; set; }

	[JsonProperty("BonsterTemplateId", NullValueHandling = NullValueHandling.Ignore)]
	public int BonsterTemplateId { get; set; }

	[JsonProperty("ArmatureName", NullValueHandling = NullValueHandling.Ignore)]
	public string SkeletonName { get; set; }

	[JsonProperty("EvolutionStage", NullValueHandling = NullValueHandling.Ignore)]
	public int EvolutionStage { get; set; }

	[JsonProperty("SkeletonPath", NullValueHandling = NullValueHandling.Ignore)]
	public string SkeletonPath { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("VipTier", NullValueHandling = NullValueHandling.Ignore)]
	public int? VipTier { get; set; }
}
