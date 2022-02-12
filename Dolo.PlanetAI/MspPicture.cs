using System;

namespace Dolo.PlanetAI;

public sealed class MspPicture : MspBase
{
	public DateTime UploadedAt { get; internal set; }

	public DateTime MembershipTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public int Id { get; internal set; }

	public int ActorId { get; internal set; }

	public string Username { get; internal set; }

	public string Name { get; internal set; }

	public string Guid { get; internal set; }

	public int Likes { get; internal set; }

	public int Views { get; internal set; }

	public int Status { get; internal set; }

	public int Comments { get; internal set; }

	public bool HasPublished { get; internal set; }

	public int Level { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public string PictureUrl { get; internal set; }

	public bool IsVip { get; internal set; }

	internal MspPicture()
	{
	}
}
