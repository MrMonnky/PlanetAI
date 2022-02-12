using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspScoreArtbook : MspBase
{
	public DateTime CreatedAt { get; internal set; }

	public DateTime PublishedAt { get; internal set; }

	public DateTime MembershipPurchasedAt { get; internal set; }

	public DateTime MembershipTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public int Id { get; internal set; }

	public int ActorId { get; internal set; }

	public string Name { get; internal set; }

	public int Likes { get; internal set; }

	public bool IsDeleted { get; internal set; }

	public int Status { get; internal set; }

	public ulong FameEarned { get; internal set; }

	public int Type { get; internal set; }

	public int TemplateType { get; internal set; }

	public int Comments { get; internal set; }

	public string Username { get; internal set; }

	public bool IsModerator { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public string ArtbookUrl { get; internal set; }

	internal MspScoreArtbook()
	{
	}

	public async Task<MspLike> LikeAsync()
	{
		return await MovieStarPlanet.LikeAddAsync(Id, ActorId, LikeType.scrapblog);
	}
}
