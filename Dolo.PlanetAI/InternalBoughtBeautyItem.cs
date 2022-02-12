using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalBoughtBeautyItem
{
	[JsonProperty("InventoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int InventoryId { get; internal set; }

	[JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
	public int Type { get; internal set; }

	[JsonProperty("ItemId", NullValueHandling = NullValueHandling.Ignore)]
	public int ItemId { get; internal set; }

	[JsonProperty("Colors", NullValueHandling = NullValueHandling.Ignore)]
	public string Colors { get; internal set; }

	[JsonProperty("IsWearing", NullValueHandling = NullValueHandling.Ignore)]
	public bool IsWearing { get; internal set; }

	internal InternalBoughtBeautyItem()
	{
	}
}
