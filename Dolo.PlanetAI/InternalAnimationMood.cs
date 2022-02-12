using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalAnimationMood
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("FigureAnimation", NullValueHandling = NullValueHandling.Ignore)]
	public string FigureAnimation { get; set; }

	[JsonProperty("FaceAnimation", NullValueHandling = NullValueHandling.Ignore)]
	public string FaceAnimation { get; set; }

	[JsonProperty("MouthAnimation", NullValueHandling = NullValueHandling.Ignore)]
	public string MouthAnimation { get; set; }

	[JsonProperty("TextLine", NullValueHandling = NullValueHandling.Ignore)]
	public string TextLine { get; set; }

	[JsonProperty("SpeechLine", NullValueHandling = NullValueHandling.Ignore)]
	public bool? SpeechLine { get; set; }

	[JsonProperty("IsBrag", NullValueHandling = NullValueHandling.Ignore)]
	public bool? IsBrag { get; set; }

	[JsonProperty("TextLineWhitelisted", NullValueHandling = NullValueHandling.Ignore)]
	public string TextLineWhitelisted { get; set; }

	[JsonProperty("TextLineBlacklisted", NullValueHandling = NullValueHandling.Ignore)]
	public string TextLineBlacklisted { get; set; }

	[JsonProperty("TextLineLastFiltered", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime? TextLineLastFiltered { get; set; }

	[JsonProperty("WallPostId", NullValueHandling = NullValueHandling.Ignore)]
	public ulong WallPostId { get; set; }

	[JsonProperty("Likes", NullValueHandling = NullValueHandling.Ignore)]
	public int Likes { get; set; }

	[JsonProperty("WallPostLinks", NullValueHandling = NullValueHandling.Ignore)]
	public object[] WallPostLinks { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
