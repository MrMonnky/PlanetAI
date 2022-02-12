using System;

namespace Dolo.PlanetAI;

public sealed class MspEyeShadow : MspBeautyBase
{
	public int Id { get; internal set; }

	public string Color { get; internal set; }

	internal MspEyeShadow()
	{
		Id = 0;
		Color = "";
		base.Swf = "";
		base.LastUpdatedAt = DateTime.Now;
		base.DefaultColors = "";
		base.DiamondsPrice = 0;
		base.Discount = 0L;
		base.IsDragonBone = false;
		base.IsHidden = false;
		base.IsVip = false;
		base.IsNew = false;
		base.Price = 0;
		base.SkinId = 0;
		base.Sortorder = 0;
	}
}
