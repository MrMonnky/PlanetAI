using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public sealed class MspScorePet : MspBase
{
	public DateTime MembershipTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public int ActorId { get; internal set; }

	public string Username { get; internal set; }

	public int ShopId { get; internal set; }

	public string Name { get; internal set; }

	public int Id { get; internal set; }

	public int Level { get; internal set; }

	public ulong Experience { get; internal set; }

	public string Color { get; internal set; }

	public int TemplateId { get; internal set; }

	public string ShopName { get; internal set; }

	public int EvolutionStage { get; internal set; }

	public string SkeletonPath { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	internal MspScorePet()
	{
	}

	public async Task<MspResult<int>> LoveAsync()
	{
		return await MovieStarPlanet.LovePetAsync(Id);
	}
}
