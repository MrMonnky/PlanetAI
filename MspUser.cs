using System;
using System.Threading.Tasks;
using Dolo.PlanetAI.Entities;

namespace Dolo.PlanetAI;

public sealed class MspUser : MspBase
{
	public DateTime LastLoginAt { get; internal set; }

	public DateTime LastAutographAt { get; internal set; }

	public DateTime CreatedAt { get; internal set; }

	public DateTime MembershiptPurchasedAt { get; internal set; }

	public DateTime MembershiptTimeoutAt { get; internal set; }

	public MspBeauty Beauty { get; internal set; }

	public MspPiggy PiggyBank { get; internal set; }

	public MspTicket Ticket { get; internal set; }

	public MspNebula Nebula { get; internal set; }

	public Gender Gender { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public Server Server { get; internal set; }

	public string WebServer { get; internal set; }

	public string Username { get; internal set; }

	public string Password { get; internal set; }

	public string ProfileId { get; internal set; }

	public string LoginId { get; internal set; }

	public string Email { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public int Id { get; internal set; }

	public int Level { get; internal set; }

	public int Friends { get; internal set; }

	public int MaxFriends { get; internal set; }

	public int FriendsVip { get; internal set; }

	public int VipDays { get; internal set; }

	public int ProfileViews { get; internal set; }

	public int RoomLikes { get; set; }

	public ulong RecyclePoints { get; internal set; }

	public ulong Fame { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Diamonds { get; internal set; }

	public ulong Fortune { get; internal set; }

	public bool IsModerator { get; internal set; }

	public bool IsVip { get; internal set; }

	public bool IsDeleted { get; internal set; }

	public bool IsJury { get; internal set; }

	public bool IsJudge { get; internal set; }

	public bool IsCeleb { get; internal set; }

	public bool IsThirdParty { get; internal set; }

	internal MspUser()
	{
	}

	public async Task<MspStatusUpdate> SetStatusAsync(Action<StatusMessageBuilder> builder)
	{
		return await MovieStarPlanet.SetStatusAsync(builder);
	}

	public async Task<MspStatus> GetStatusAsync()
	{
		return await MovieStarPlanet.GetActorStatusAsync(Id);
	}

	public async Task<MspList<MspFriend>> GetFriendsAsync()
	{
		return await MovieStarPlanet.GetFriendsAsync();
	}

	public async Task<MspList<MspActor>> GetFriendsWithActorAsync()
	{
		return await MovieStarPlanet.GetFriendsWithActorAsync();
	}

	public async Task<MspResult<MspBeautyItem>> BuyBeautyItemAsync(MspBeautyItem item)
	{
		return await MovieStarPlanet.BuyBeautyItemAsync(item);
	}

	public async Task<MspAnimationBought> BuyAnimationAsync(int id)
	{
		return await MovieStarPlanet.BuyAnimationAsync(id);
	}

	public async Task<MspList<MspPet>> GetBonsterWithHighscoreAsync()
	{
		return await MovieStarPlanet.Api.GetBonsterWithHighscoreAsync("GetBonsterWithHighscoreAsync");
	}

	public async Task<MspList<MspActorCloth>> GenerateLookAsync()
	{
		return await MovieStarPlanet.Api.GenerateLookAsync();
	}
}
