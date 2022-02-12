using System;

namespace Dolo.PlanetAI;

public class MspBeautyBase
{
	public string Swf { get; internal set; }

	public string DefaultColors { get; internal set; }

	public int SkinId { get; internal set; }

	public int Price { get; internal set; }

	public int DiamondsPrice { get; internal set; }

	public bool IsVip { get; internal set; }

	public bool IsDragonBone { get; internal set; }

	public bool IsHidden { get; internal set; }

	public bool IsNew { get; internal set; }

	public object Sortorder { get; internal set; }

	public long Discount { get; internal set; }

	public DateTime LastUpdatedAt { get; internal set; }

	internal MspBeautyBase()
	{
	}
}
