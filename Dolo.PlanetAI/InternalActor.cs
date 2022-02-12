using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalActor
{
	[JsonProperty("NebulaProfileId", NullValueHandling = NullValueHandling.Ignore)]
	public string NebulaProfileId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }

	[JsonProperty("SkinSWF", NullValueHandling = NullValueHandling.Ignore)]
	public string SkinSwf { get; set; }

	[JsonProperty("SkinColor", NullValueHandling = NullValueHandling.Ignore)]
	public object SkinColor { get; set; }

	[JsonProperty("NoseId", NullValueHandling = NullValueHandling.Ignore)]
	public int NoseId { get; set; }

	[JsonProperty("EyeId", NullValueHandling = NullValueHandling.Ignore)]
	public int EyeId { get; set; }

	[JsonProperty("MouthId", NullValueHandling = NullValueHandling.Ignore)]
	public int MouthId { get; set; }

	[JsonProperty("Money", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Money { get; set; }

	[JsonProperty("EyeColors", NullValueHandling = NullValueHandling.Ignore)]
	public string EyeColors { get; set; }

	[JsonProperty("MouthColors", NullValueHandling = NullValueHandling.Ignore)]
	public string MouthColors { get; set; }

	[JsonProperty("Fame", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fame { get; set; }

	[JsonProperty("FriendCount", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCount { get; set; }

	[JsonProperty("IsExtra", NullValueHandling = NullValueHandling.Ignore)]
	public int IsExtra { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("TotalVipDays", NullValueHandling = NullValueHandling.Ignore)]
	public int TotalVipDays { get; set; }

	[JsonProperty("FriendCountVIP", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCountVip { get; set; }

	[JsonProperty("Diamonds", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Diamonds { get; set; }

	[JsonProperty("PopUpStyleId", NullValueHandling = NullValueHandling.Ignore)]
	public object PopUpStyleId { get; set; }

	[JsonProperty("Moderator", NullValueHandling = NullValueHandling.Ignore)]
	public int Moderator { get; set; }

	[JsonProperty("VipTier", NullValueHandling = NullValueHandling.Ignore)]
	public int VipTier { get; set; }

	[JsonProperty("EyeShadowId", NullValueHandling = NullValueHandling.Ignore)]
	public int EyeShadowId { get; set; }

	[JsonProperty("EyeShadowColors", NullValueHandling = NullValueHandling.Ignore)]
	public string EyeShadowColors { get; set; }

	[JsonProperty("LastLogin", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastLogin { get; set; }

	[JsonProperty("MembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedDate { get; set; }

	[JsonProperty("Fortune", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fortune { get; set; }

	[JsonProperty("BoyfriendId", NullValueHandling = NullValueHandling.Ignore)]
	public int BoyfriendId { get; set; }

	[JsonProperty("BoyfriendStatus", NullValueHandling = NullValueHandling.Ignore)]
	public int BoyfriendStatus { get; set; }

	[JsonProperty("ActorClothesRels", NullValueHandling = NullValueHandling.Ignore)]
	public InternalActorClothesRel[] ActorClothesRels { get; set; }

	[JsonProperty("ActorBeautyClinicItemRels", NullValueHandling = NullValueHandling.Ignore)]
	public object[] ActorBeautyClinicItemRels { get; set; }

	[JsonProperty("Nose", NullValueHandling = NullValueHandling.Ignore)]
	public InternalEye Nose { get; set; }

	[JsonProperty("Mouth", NullValueHandling = NullValueHandling.Ignore)]
	public InternalEye Mouth { get; set; }

	[JsonProperty("EyeShadow", NullValueHandling = NullValueHandling.Ignore)]
	public InternalEye EyeShadow { get; set; }

	[JsonProperty("Eye", NullValueHandling = NullValueHandling.Ignore)]
	public InternalEye Eye { get; set; }
}
