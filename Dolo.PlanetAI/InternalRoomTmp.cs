using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalRoomTmp
{
	[JsonProperty("ActorRoomId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorRoomId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Wallpaper", NullValueHandling = NullValueHandling.Ignore)]
	public string Wallpaper { get; set; }

	[JsonProperty("Floor", NullValueHandling = NullValueHandling.Ignore)]
	public string Floor { get; set; }

	[JsonProperty("RoomLikes", NullValueHandling = NullValueHandling.Ignore)]
	public int RoomLikes { get; set; }
}
