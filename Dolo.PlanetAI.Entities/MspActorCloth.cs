namespace Dolo.PlanetAI.Entities;

public class MspActorCloth : MspBaseHttp
{
	public int Id { get; internal set; }

	public int GiftId { get; internal set; }

	public int SenderGiftId { get; internal set; }

	public int ActorId { get; internal set; }

	public int ClothId { get; internal set; }

	public string Color { get; internal set; }

	public int IsWearing { get; internal set; }

	public long X { get; internal set; }

	public long Y { get; internal set; }

	public ClothShop Cloth { get; internal set; }
}
