namespace Dolo.PlanetAI.Entities;

public sealed class MspBeautyItem
{
	public string Colors { get; set; }

	public int InventoryId { get; set; }

	public bool IsOwned { get; set; }

	public bool IsWearing { get; set; }

	public int ItemId { get; set; }

	public int Type { get; }

	internal MspBeautyItem()
	{
	}

	public MspBeautyItem(BeautyType BeutyType)
	{
		Type = (int)BeutyType;
	}
}
