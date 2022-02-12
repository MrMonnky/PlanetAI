using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalActorStatus
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public long ActorId { get; set; }

	[JsonProperty("SoundMute", NullValueHandling = NullValueHandling.Ignore)]
	public bool SoundMute { get; set; }

	[JsonProperty("CampaignViewed", NullValueHandling = NullValueHandling.Ignore)]
	public long? CampaignViewed { get; set; }

	[JsonProperty("MobileStartAward", NullValueHandling = NullValueHandling.Ignore)]
	public long MobileStartAward { get; set; }

	[JsonProperty("FameLevelConvert", NullValueHandling = NullValueHandling.Ignore)]
	public bool FameLevelConvert { get; set; }

	[JsonProperty("NotificationActive", NullValueHandling = NullValueHandling.Ignore)]
	public bool NotificationActive { get; set; }

	[JsonProperty("PhotoShareRulesAccepted", NullValueHandling = NullValueHandling.Ignore)]
	public bool PhotoShareRulesAccepted { get; set; }

	[JsonProperty("LogOutWhenClickingExternalAppLinkAccepted", NullValueHandling = NullValueHandling.Ignore)]
	public bool LogOutWhenClickingExternalAppLinkAccepted { get; set; }

	[JsonProperty("AnchorFriendshipAccepted", NullValueHandling = NullValueHandling.Ignore)]
	public bool AnchorFriendshipAccepted { get; set; }

	[JsonProperty("AnchorGiftsGiven", NullValueHandling = NullValueHandling.Ignore)]
	public long AnchorGiftsGiven { get; set; }

	[JsonProperty("ThirdPartyCreation", NullValueHandling = NullValueHandling.Ignore)]
	public bool ThirdPartyCreation { get; set; }

	[JsonProperty("PreviousLoginDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime PreviousLoginDate { get; set; }

	[JsonProperty("ArtbookShareRulesAccepted", NullValueHandling = NullValueHandling.Ignore)]
	public bool ArtbookShareRulesAccepted { get; set; }

	[JsonProperty("HasMyRoomStarterItems", NullValueHandling = NullValueHandling.Ignore)]
	public bool HasMyRoomStarterItems { get; set; }

	[JsonProperty("MessageOverviewMigrated", NullValueHandling = NullValueHandling.Ignore)]
	public object MessageOverviewMigrated { get; set; }

	[JsonProperty("ActorDetails", NullValueHandling = NullValueHandling.Ignore)]
	public object ActorDetails { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
