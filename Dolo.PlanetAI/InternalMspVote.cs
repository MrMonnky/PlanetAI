using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalMspVote
{
	[JsonProperty("ThemeId", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; internal set; }

	[JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
	public int ContentType { get; internal set; }

	[JsonProperty("IdA", NullValueHandling = NullValueHandling.Ignore)]
	public int IdA { get; internal set; }

	[JsonProperty("IdB", NullValueHandling = NullValueHandling.Ignore)]
	public int IdB { get; internal set; }

	[JsonProperty("ActorIdA", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorIdA { get; internal set; }

	[JsonProperty("ActorIdB", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorIdB { get; internal set; }

	[JsonProperty("ScoreA", NullValueHandling = NullValueHandling.Ignore)]
	public ulong ScoreA { get; internal set; }

	[JsonProperty("ScoreB", NullValueHandling = NullValueHandling.Ignore)]
	public ulong ScoreB { get; internal set; }

	[JsonProperty("nameA", NullValueHandling = NullValueHandling.Ignore)]
	public string nameA { get; internal set; }

	[JsonProperty("nameB", NullValueHandling = NullValueHandling.Ignore)]
	public string nameB { get; internal set; }

	[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
	public bool data { get; internal set; }
}
