using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalStatus
{
	[JsonProperty("SpecialFriends", NullValueHandling = NullValueHandling.Ignore)]
	public InternalSpecialFriend[] SpecialFriends { get; set; }

	[JsonProperty("AnimationMood", NullValueHandling = NullValueHandling.Ignore)]
	public InternalAnimationMood AnimationMood { get; set; }

	[JsonProperty("LastUpdated", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastUpdated { get; set; }

	[JsonProperty("PetClickItemRel")]
	public object PetClickItemRel { get; set; }

	[JsonProperty("FriendsStatus", NullValueHandling = NullValueHandling.Ignore)]
	public int FriendsStatus { get; set; }

	[JsonProperty("BonsterRelItem")]
	public object BonsterRelItem { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
