using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalMspAnimationBought
{
	[JsonProperty("AwardedFame", NullValueHandling = NullValueHandling.Ignore)]
	public int Fame { get; internal set; }

	[JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
	public int Code { get; internal set; }

	[JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
	public string Description { get; internal set; }

	public bool HasBought => Code == 0;

	internal InternalMspAnimationBought()
	{
	}
}
