namespace Dolo.PlanetAI;

public sealed class MspClothRel
{
	public ulong Id { get; internal set; }

	public int ActorId { get; internal set; }

	public int ClothId { get; internal set; }

	public string Color { get; internal set; }

	public bool IsWearing { get; internal set; }

	public MspCloth Cloth { get; internal set; }

	internal MspClothRel()
	{
	}
}
