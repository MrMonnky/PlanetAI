using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalBoonstersTmp
{
	[JsonProperty("ActorClickItem")]
	public InternalBoonsters ActorClickItem { get; set; }

	[JsonProperty("Price")]
	public long Price { get; set; }
}
