using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalRoom
{
	[JsonProperty("actorRoom", NullValueHandling = NullValueHandling.Ignore)]
	public InternalRoomTmp ActorRoom { get; set; }

	[JsonProperty("hasLiked", NullValueHandling = NullValueHandling.Ignore)]
	public int HasLiked { get; set; }
}
