using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class InternalActorClothPageTmp
{
	[JsonProperty("ActorClothesRelId", NullValueHandling = NullValueHandling.Ignore)]
	public object Id { get; internal set; }

	[JsonProperty("GiftId", NullValueHandling = NullValueHandling.Ignore)]
	public int GiftId { get; internal set; }

	[JsonProperty("LinkedGiftId", NullValueHandling = NullValueHandling.Ignore)]
	public int SenderGiftId { get; internal set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; internal set; }

	[JsonProperty("ClothesId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothId { get; internal set; }

	[JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
	public string Color { get; internal set; }

	[JsonProperty("IsWearing", NullValueHandling = NullValueHandling.Ignore)]
	public int IsWearing { get; internal set; }

	[JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
	public long X { get; internal set; }

	[JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
	public long Y { get; internal set; }

	[JsonProperty("Cloth", NullValueHandling = NullValueHandling.Ignore)]
	public ClothShop Cloth { get; internal set; }

	internal InternalActorClothPageTmp()
	{
	}
}
