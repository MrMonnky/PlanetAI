namespace Dolo.PlanetAI;

public class MspSpecialFriend : MspBaseActor
{
	public Gender Gender { get; internal set; }

	public int Id { get; internal set; }

	public string Name { get; internal set; }

	public string ProfileId { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	internal MspSpecialFriend()
	{
	}
}
