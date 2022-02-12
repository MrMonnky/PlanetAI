using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalActorClothesRel
{
	[JsonProperty("ActorClothesRelId", NullValueHandling = NullValueHandling.Ignore)]
	public ulong ActorClothesRelId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("ClothesId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesId { get; set; }

	[JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
	public string Color { get; set; }

	[JsonProperty("IsWearing", NullValueHandling = NullValueHandling.Ignore)]
	public int IsWearing { get; set; }

	[JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
	public int X { get; set; }

	[JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
	public int Y { get; set; }

	[JsonProperty("DesignId", NullValueHandling = NullValueHandling.Ignore)]
	public int DesignId { get; set; }

	[JsonProperty("Cloth", NullValueHandling = NullValueHandling.Ignore)]
	public InternalCloth Cloth { get; set; }

	[JsonProperty("Design", NullValueHandling = NullValueHandling.Ignore)]
	public object Design { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
