using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalLoginStatus
{
	[JsonProperty("wheelValues", NullValueHandling = NullValueHandling.Ignore)]
	public object WheelValues { get; set; }

	[JsonProperty("wheelData", NullValueHandling = NullValueHandling.Ignore)]
	public object WheelData { get; set; }

	[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
	public string Status { get; set; }

	[JsonProperty("actor", NullValueHandling = NullValueHandling.Ignore)]
	public InternalUser Actor { get; set; }

	[JsonProperty("piggyBank", NullValueHandling = NullValueHandling.Ignore)]
	public InternalPiggyBank PiggyBank { get; set; }

	[JsonProperty("statusDetails", NullValueHandling = NullValueHandling.Ignore)]
	public object StatusDetails { get; set; }

	[JsonProperty("actorLocale", NullValueHandling = NullValueHandling.Ignore)]
	public string[] ActorLocale { get; set; }

	[JsonProperty("lbs", NullValueHandling = NullValueHandling.Ignore)]
	public Uri[] Lbs { get; set; }

	[JsonProperty("userType", NullValueHandling = NullValueHandling.Ignore)]
	public string UserType { get; set; }

	[JsonProperty("adCountryMap", NullValueHandling = NullValueHandling.Ignore)]
	public InternalAdCountryMap[] AdCountryMap { get; set; }

	[JsonProperty("postLoginSeq", NullValueHandling = NullValueHandling.Ignore)]
	public InternalPostLoginSeq PostLoginSeq { get; set; }

	[JsonProperty("previousLastLogin", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime? PreviousLastLogin { get; set; }

	[JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
	public object Version { get; set; }

	[JsonProperty("userIp", NullValueHandling = NullValueHandling.Ignore)]
	public long UserIp { get; set; }

	[JsonProperty("ticket", NullValueHandling = NullValueHandling.Ignore)]
	public string Ticket { get; set; }

	[JsonProperty("purchaseTypeId", NullValueHandling = NullValueHandling.Ignore)]
	public long PurchaseTypeId { get; set; }

	[JsonProperty("mutedUntil", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MutedUntil { get; set; }

	[JsonProperty("helpMessage", NullValueHandling = NullValueHandling.Ignore)]
	public object HelpMessage { get; set; }

	[JsonProperty("amsHash", NullValueHandling = NullValueHandling.Ignore)]
	public string AmsHash { get; set; }

	[JsonProperty("combatCategorisation", NullValueHandling = NullValueHandling.Ignore)]
	public object CombatCategorisation { get; set; }

	[JsonProperty("shouldShowSchoolChoice", NullValueHandling = NullValueHandling.Ignore)]
	public bool ShouldShowSchoolChoice { get; set; }

	[JsonProperty("boughtRespinToday", NullValueHandling = NullValueHandling.Ignore)]
	public bool BoughtRespinToday { get; set; }

	[JsonProperty("diamondRespinPrice", NullValueHandling = NullValueHandling.Ignore)]
	public long DiamondRespinPrice { get; set; }

	[JsonProperty("fameWheelSpinPrice", NullValueHandling = NullValueHandling.Ignore)]
	public long FameWheelSpinPrice { get; set; }

	[JsonProperty("worldBackground", NullValueHandling = NullValueHandling.Ignore)]
	public object WorldBackground { get; set; }

	[JsonProperty("wheelOfFortuneGfx", NullValueHandling = NullValueHandling.Ignore)]
	public object WheelOfFortuneGfx { get; set; }

	[JsonProperty("wheelDownloadableFameSpins", NullValueHandling = NullValueHandling.Ignore)]
	public long WheelDownloadableFameSpins { get; set; }

	[JsonProperty("marsCategorisation", NullValueHandling = NullValueHandling.Ignore)]
	public object MarsCategorisation { get; set; }

	[JsonProperty("nebulaLoginStatus", NullValueHandling = NullValueHandling.Ignore)]
	public InternalNebulaLoginStatus NebulaLoginStatus { get; set; }
}
