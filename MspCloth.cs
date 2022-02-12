using System;

namespace Dolo.PlanetAI;

public sealed class MspCloth : MspBase
{
	public string Name { get; internal set; }

	public string Swf { get; internal set; }

	public string Filename { get; internal set; }

	public object Sortorder { get; internal set; }

	public double Scale { get; internal set; }

	public bool IsVip { get; internal set; }

	public bool IsNew { get; internal set; }

	public int Id { get; internal set; }

	public int CategoryId { get; internal set; }

	public int Price { get; internal set; }

	public int ShopId { get; internal set; }

	public int SkinId { get; internal set; }

	public int Discount { get; internal set; }

	public int ThemeId { get; internal set; }

	public int DiamondsPrice { get; internal set; }

	public string ColorScheme { get; internal set; }

	public string SwfUrl { get; internal set; }

	public DateTime LastUpdatedAt { get; internal set; }

	public MspClothCategory Category { get; internal set; }

	internal MspCloth()
	{
	}
}
