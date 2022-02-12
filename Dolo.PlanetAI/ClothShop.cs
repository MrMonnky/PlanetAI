using System;
using Dolo.PlanetAI.NET;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class ClothShop
{
	[JsonProperty("ClothesId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesId { get; internal set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; internal set; }

	[JsonProperty("SWF", NullValueHandling = NullValueHandling.Ignore)]
	public string SWF { get; internal set; }

	[JsonProperty("ClothesCategoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesCategoryId { get; internal set; }

	[JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
	public int Price { get; internal set; }

	[JsonProperty("ShopId", NullValueHandling = NullValueHandling.Ignore)]
	public int ShopId { get; internal set; }

	[JsonProperty("SkinId", NullValueHandling = NullValueHandling.Ignore)]
	public int SkinId { get; internal set; }

	[JsonProperty("Filename", NullValueHandling = NullValueHandling.Ignore)]
	public string Filename { get; internal set; }

	[JsonProperty("Scale", NullValueHandling = NullValueHandling.Ignore)]
	public double Scale { get; internal set; }

	[JsonProperty("Vip", NullValueHandling = NullValueHandling.Ignore)]
	public int Vip { get; internal set; }

	[JsonProperty("RegNewUser", NullValueHandling = NullValueHandling.Ignore)]
	public int RegNewUser { get; internal set; }

	[JsonProperty("isNew", NullValueHandling = NullValueHandling.Ignore)]
	public int IsNew { get; internal set; }

	[JsonProperty("Discount", NullValueHandling = NullValueHandling.Ignore)]
	public int Discount { get; internal set; }

	[JsonProperty("MouseAction", NullValueHandling = NullValueHandling.Ignore)]
	public int MouseAction { get; internal set; }

	[JsonProperty("ThemeId", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; internal set; }

	[JsonProperty("DiamondsPrice", NullValueHandling = NullValueHandling.Ignore)]
	public int DiamondsPrice { get; internal set; }

	[JsonProperty("AvailableUntil", NullValueHandling = NullValueHandling.Ignore)]
	public object AvailableUntil { get; internal set; }

	[JsonProperty("ColorScheme", NullValueHandling = NullValueHandling.Ignore)]
	public string ColorScheme { get; internal set; }

	[JsonProperty("LastUpdated", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastUpdated { get; internal set; }

	[JsonProperty("ClothesCategory", NullValueHandling = NullValueHandling.Ignore)]
	public ClothesCategory ClothesCategory { get; internal set; }

	[JsonProperty("ThemeItem", NullValueHandling = NullValueHandling.Ignore)]
	public object ThemeItem { get; internal set; }

	public bool IsHair => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Hair;

	public bool IsHead => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Head;

	public bool IsFoot => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Foot;

	public bool IsTop => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Top;

	public bool IsStuff => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Stuff;

	public bool IsBottom => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Bottom;

	public bool IsAccessories => MspApi.GetCatoryType(ClothesCategoryId) == CategoryType.Accessor;

	public string Path => MspApi.GetCategoryUrl(ClothesCategoryId) + Filename.Replace(" ", "%20") + ".swf";

	public string CategoryName => MspApi.GetCategoryName(ClothesCategoryId);

	internal ClothShop()
	{
	}
}
