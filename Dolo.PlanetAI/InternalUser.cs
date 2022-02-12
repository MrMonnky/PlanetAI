using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalUser
{
	private string gender;

	[JsonProperty("EventsToNudge", NullValueHandling = NullValueHandling.Ignore)]
	public long EventsToNudge { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; set; }

	[JsonProperty("SkinSWF", NullValueHandling = NullValueHandling.Ignore)]
	public string Gender
	{
		get
		{
			return (gender == "femaleskin") ? "Female" : "Male";
		}
		set
		{
			gender = value;
		}
	}

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

	[JsonProperty("Fortune", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fortune { get; set; }

	[JsonProperty("FriendCount", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCount { get; set; }

	[JsonProperty("Created", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime Created { get; set; }

	[JsonProperty("LastLogin", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastLogin { get; set; }

	[JsonProperty("Moderator", NullValueHandling = NullValueHandling.Ignore)]
	public long Moderator { get; set; }

	[JsonProperty("ProfileDisplays", NullValueHandling = NullValueHandling.Ignore)]
	public int ProfileDisplays { get; set; }

	[JsonProperty("IsExtra", NullValueHandling = NullValueHandling.Ignore)]
	public long IsExtra { get; set; }

	[JsonProperty("InvitedByActorId", NullValueHandling = NullValueHandling.Ignore)]
	public long InvitedByActorId { get; set; }

	[JsonProperty("PollTaken", NullValueHandling = NullValueHandling.Ignore)]
	public long PollTaken { get; set; }

	[JsonProperty("ValueOfGiftsReceived", NullValueHandling = NullValueHandling.Ignore)]
	public long ValueOfGiftsReceived { get; set; }

	[JsonProperty("ValueOfGiftsGiven", NullValueHandling = NullValueHandling.Ignore)]
	public long ValueOfGiftsGiven { get; set; }

	[JsonProperty("NumberOfGiftsGiven", NullValueHandling = NullValueHandling.Ignore)]
	public long NumberOfGiftsGiven { get; set; }

	[JsonProperty("NumberOfGiftsReceived", NullValueHandling = NullValueHandling.Ignore)]
	public long NumberOfGiftsReceived { get; set; }

	[JsonProperty("NumberOfAutographsReceived", NullValueHandling = NullValueHandling.Ignore)]
	public long NumberOfAutographsReceived { get; set; }

	[JsonProperty("NumberOfAutographsGiven", NullValueHandling = NullValueHandling.Ignore)]
	public long NumberOfAutographsGiven { get; set; }

	[JsonProperty("TimeOfLastAutographGiven", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime? TimeOfLastAutographGiven { get; set; }

	[JsonProperty("BoyfriendId", NullValueHandling = NullValueHandling.Ignore)]
	public object BoyfriendId { get; set; }

	[JsonProperty("BoyfriendStatus", NullValueHandling = NullValueHandling.Ignore)]
	public object BoyfriendStatus { get; set; }

	[JsonProperty("MembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedDate { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("MembershipGiftRecievedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipGiftRecievedDate { get; set; }

	[JsonProperty("BehaviourStatus", NullValueHandling = NullValueHandling.Ignore)]
	public long BehaviourStatus { get; set; }

	[JsonProperty("LockedUntil", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LockedUntil { get; set; }

	[JsonProperty("LockedText", NullValueHandling = NullValueHandling.Ignore)]
	public object LockedText { get; set; }

	[JsonProperty("BadWordCount", NullValueHandling = NullValueHandling.Ignore)]
	public long BadWordCount { get; set; }

	[JsonProperty("PurchaseTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime PurchaseTimeoutDate { get; set; }

	[JsonProperty("EmailValidated", NullValueHandling = NullValueHandling.Ignore)]
	public long EmailValidated { get; set; }

	[JsonProperty("RetentionStatus", NullValueHandling = NullValueHandling.Ignore)]
	public long RetentionStatus { get; set; }

	[JsonProperty("GiftStatus", NullValueHandling = NullValueHandling.Ignore)]
	public long GiftStatus { get; set; }

	[JsonProperty("MarketingNextStepLogins", NullValueHandling = NullValueHandling.Ignore)]
	public long MarketingNextStepLogins { get; set; }

	[JsonProperty("MarketingStep", NullValueHandling = NullValueHandling.Ignore)]
	public long MarketingStep { get; set; }

	[JsonProperty("TotalVipDays", NullValueHandling = NullValueHandling.Ignore)]
	public int TotalVipDays { get; set; }

	[JsonProperty("RecyclePoints", NullValueHandling = NullValueHandling.Ignore)]
	public ulong RecyclePoints { get; set; }

	[JsonProperty("EmailSettings", NullValueHandling = NullValueHandling.Ignore)]
	public long EmailSettings { get; set; }

	[JsonProperty("TimeOfLastAutographGivenStr", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime? TimeOfLastAutographGivenStr { get; set; }

	[JsonProperty("FriendCountVIP", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendCountVip { get; set; }

	[JsonProperty("ForceNameChange", NullValueHandling = NullValueHandling.Ignore)]
	public long ForceNameChange { get; set; }

	[JsonProperty("CreationRewardStep", NullValueHandling = NullValueHandling.Ignore)]
	public long CreationRewardStep { get; set; }

	[JsonProperty("CreationRewardLastAwardDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime CreationRewardLastAwardDate { get; set; }

	[JsonProperty("NameBeforeDeleted", NullValueHandling = NullValueHandling.Ignore)]
	public object NameBeforeDeleted { get; set; }

	[JsonProperty("LastTransactionId", NullValueHandling = NullValueHandling.Ignore)]
	public long LastTransactionId { get; set; }

	[JsonProperty("AllowCommunication", NullValueHandling = NullValueHandling.Ignore)]
	public long AllowCommunication { get; set; }

	[JsonProperty("Diamonds", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Diamonds { get; set; }

	[JsonProperty("PopUpStyleId", NullValueHandling = NullValueHandling.Ignore)]
	public long PopUpStyleId { get; set; }

	[JsonProperty("VipTier", NullValueHandling = NullValueHandling.Ignore)]
	public int? VipTier { get; set; }

	[JsonProperty("EyeShadowId", NullValueHandling = NullValueHandling.Ignore)]
	public int EyeShadowId { get; set; }

	[JsonProperty("EyeShadowColors", NullValueHandling = NullValueHandling.Ignore)]
	public string EyeShadowColors { get; set; }

	[JsonProperty("OfferActivationDate", NullValueHandling = NullValueHandling.Ignore)]
	public object OfferActivationDate { get; set; }

	[JsonProperty("OfferShownDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime? OfferShownDate { get; set; }

	[JsonProperty("OfferId", NullValueHandling = NullValueHandling.Ignore)]
	public long? OfferId { get; set; }

	[JsonProperty("RoomLikes", NullValueHandling = NullValueHandling.Ignore)]
	public int RoomLikes { get; set; }

	[JsonProperty("Email", NullValueHandling = NullValueHandling.Ignore)]
	public string Email { get; set; }

	[JsonProperty("Deleted", NullValueHandling = NullValueHandling.Ignore)]
	public object Deleted { get; set; }

	[JsonProperty("BoyFriend", NullValueHandling = NullValueHandling.Ignore)]
	public object BoyFriend { get; set; }

	[JsonProperty("ActorPersonalInfo", NullValueHandling = NullValueHandling.Ignore)]
	public object ActorPersonalInfo { get; set; }

	[JsonProperty("ActorRelationships", NullValueHandling = NullValueHandling.Ignore)]
	public object[] ActorRelationships { get; set; }

	[JsonProperty("ActorStatus", NullValueHandling = NullValueHandling.Ignore)]
	public InternalActorStatus ActorStatus { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
