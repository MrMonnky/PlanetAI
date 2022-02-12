using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class KeyBackground
{
	[JsonProperty("BackgroundId", NullValueHandling = NullValueHandling.Ignore)]
	public int BackgroundId { get; internal set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; internal set; }

	[JsonProperty("BackgroundCategoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int BackgroundCategoryId { get; internal set; }

	[JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
	public int Price { get; internal set; }

	[JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
	public int Level { get; internal set; }

	[JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
	public string Url { get; internal set; }

	[JsonProperty("Vip", NullValueHandling = NullValueHandling.Ignore)]
	public int Vip { get; internal set; }

	[JsonProperty("Deleted", NullValueHandling = NullValueHandling.Ignore)]
	public int Deleted { get; internal set; }

	[JsonProperty("isNew", NullValueHandling = NullValueHandling.Ignore)]
	public int IsNew { get; internal set; }

	[JsonProperty("Discount", NullValueHandling = NullValueHandling.Ignore)]
	public int Discount { get; internal set; }

	[JsonProperty("ThemeId", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; internal set; }

	[JsonProperty("DiamondsPrice", NullValueHandling = NullValueHandling.Ignore)]
	public int DiamondsPrice { get; internal set; }

	[JsonProperty("TagId", NullValueHandling = NullValueHandling.Ignore)]
	public int TagId { get; internal set; }

	[JsonProperty("LastUpdated", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastUpdated { get; internal set; }

	[JsonProperty("BackgroundCategory", NullValueHandling = NullValueHandling.Ignore)]
	public object BackgroundCategory { get; internal set; }

	internal KeyBackground()
	{
	}
}
