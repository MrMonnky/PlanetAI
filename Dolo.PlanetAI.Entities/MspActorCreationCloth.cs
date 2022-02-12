namespace Dolo.PlanetAI.Entities;

public class MspActorCreationCloth
{
	public int ActorClothesRelId { get; set; }

	public int ActorId { get; set; }

	public int ClothesId { get; set; }

	public string Color { get; set; }

	public int IsWearing { get; set; }

	public int x { get; set; }

	public int y { get; set; }

	internal MspActorCreationCloth()
	{
	}
}
