using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPostLoginSeq
{
	[JsonProperty("ShowCampaign", NullValueHandling = NullValueHandling.Ignore)]
	public bool ShowCampaign { get; set; }

	[JsonProperty("ShowVipRebuy", NullValueHandling = NullValueHandling.Ignore)]
	public bool ShowVipRebuy { get; set; }

	[JsonProperty("ShowFameLevelConvert", NullValueHandling = NullValueHandling.Ignore)]
	public bool ShowFameLevelConvert { get; set; }

	[JsonProperty("SpecialOffer", NullValueHandling = NullValueHandling.Ignore)]
	public object SpecialOffer { get; set; }

	[JsonProperty("DailyBonusType", NullValueHandling = NullValueHandling.Ignore)]
	public long DailyBonusType { get; set; }

	[JsonProperty("AnchorFriendshipAccepted", NullValueHandling = NullValueHandling.Ignore)]
	public bool AnchorFriendshipAccepted { get; set; }

	[JsonProperty("AnchorGiftsGiven", NullValueHandling = NullValueHandling.Ignore)]
	public long AnchorGiftsGiven { get; set; }

	[JsonProperty("Features", NullValueHandling = NullValueHandling.Ignore)]
	public InternalFeature[] Features { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
