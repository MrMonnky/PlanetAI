using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class ShopClothe
{
	[JsonProperty("Clothes", NullValueHandling = NullValueHandling.Ignore)]
	public List<ClothShop> Clothes { get; internal set; }

	[JsonProperty("PageTheme", NullValueHandling = NullValueHandling.Ignore)]
	public PageTheme PageTheme { get; internal set; }

	[JsonProperty("TotalPages", NullValueHandling = NullValueHandling.Ignore)]
	public int Count { get; internal set; }

	internal ShopClothe()
	{
	}
}
