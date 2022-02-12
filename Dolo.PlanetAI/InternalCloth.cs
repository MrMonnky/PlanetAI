using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalCloth
{
	[JsonProperty("ClothesId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("SWF", NullValueHandling = NullValueHandling.Ignore)]
	public string Swf { get; set; }

	[JsonProperty("ClothesCategoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesCategoryId { get; set; }

	[JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
	public int Price { get; set; }

	[JsonProperty("ShopId", NullValueHandling = NullValueHandling.Ignore)]
	public int ShopId { get; set; }

	[JsonProperty("SkinId", NullValueHandling = NullValueHandling.Ignore)]
	public int SkinId { get; set; }

	[JsonProperty("Filename", NullValueHandling = NullValueHandling.Ignore)]
	public string Filename { get; set; }

	[JsonProperty("Scale", NullValueHandling = NullValueHandling.Ignore)]
	public double Scale { get; set; }

	[JsonProperty("Vip", NullValueHandling = NullValueHandling.Ignore)]
	public int Vip { get; set; }

	[JsonProperty("RegNewUser", NullValueHandling = NullValueHandling.Ignore)]
	public int RegNewUser { get; set; }

	[JsonProperty("sortorder", NullValueHandling = NullValueHandling.Ignore)]
	public object Sortorder { get; set; }

	[JsonProperty("isNew", NullValueHandling = NullValueHandling.Ignore)]
	public int IsNew { get; set; }

	[JsonProperty("Discount", NullValueHandling = NullValueHandling.Ignore)]
	public int Discount { get; set; }

	[JsonProperty("MouseAction", NullValueHandling = NullValueHandling.Ignore)]
	public int MouseAction { get; set; }

	[JsonProperty("ThemeId", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; set; }

	[JsonProperty("DiamondsPrice", NullValueHandling = NullValueHandling.Ignore)]
	public int DiamondsPrice { get; set; }

	[JsonProperty("AvailableUntil", NullValueHandling = NullValueHandling.Ignore)]
	public object AvailableUntil { get; set; }

	[JsonProperty("ColorScheme", NullValueHandling = NullValueHandling.Ignore)]
	public string ColorScheme { get; set; }

	[JsonProperty("LastUpdated", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastUpdated { get; set; }

	[JsonProperty("ClothesCategory", NullValueHandling = NullValueHandling.Ignore)]
	public InternalClothesCategory ClothesCategory { get; set; }

	[JsonProperty("ThemeItem", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeItem { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
