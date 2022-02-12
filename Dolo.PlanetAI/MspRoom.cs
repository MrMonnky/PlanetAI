using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspRoom : MspBaseHttp
{
	public int Id { get; internal set; }

	public int ActorId { get; internal set; }

	public string Name { get; internal set; }

	public string Wallpaper { get; internal set; }

	public string Floor { get; internal set; }

	public int Likes { get; internal set; }

	public bool HasLiked { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	internal MspRoom()
	{
	}

	public async Task LikeAsync()
	{
		await MovieStarPlanet.LikeAddAsync(Id, ActorId, LikeType.room);
	}
}
