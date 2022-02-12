using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPiggy
{
	[JsonProperty("Data")]
	public InternalPiggyBank Data { get; set; }
}
