namespace Dolo.PlanetAI;

public class MspSingle : MspBaseActor
{
	public bool IsAvailable { get; internal set; }

	public int Id { get; internal set; }

	public string Username { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	internal MspSingle()
	{
	}
}
