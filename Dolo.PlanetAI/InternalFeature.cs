using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalFeature
{
	[JsonProperty("FeatureId", NullValueHandling = NullValueHandling.Ignore)]
	public long FeatureId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Platforms", NullValueHandling = NullValueHandling.Ignore)]
	public long[] Platforms { get; set; }
}
