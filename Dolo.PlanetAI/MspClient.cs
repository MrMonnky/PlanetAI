using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Dolo.PlanetAI.Entities;
using Dolo.PlanetAI.NET;
using Dolo.PlanetAI.NET.Utils;

namespace Dolo.PlanetAI;

public class MspClient
{
	internal MspConfig Config;

	internal MspLogin User;

	internal MspApi Api;

	public MspClient(Action<MspConfig> config)
	{
		Config = config.GetAction();
		Api = new MspApi(this);
		User = new MspLogin();
	}

	public MspLogin GetUser()
	{
		return User;
	}

	public async void LogoutAsync()
	{
		if (Config.UseOriginalBehaviour)
		{
			await MspApi.Socket.CloseAsync();
		}
	}

	public async Task<object> SendAsync(string method, bool needTicket, params object[] data)
	{
		return await Api.SendAsync(method, needTicket, data);
	}

	[AMFMethod("MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login")]
	public async Task<MspLogin> LoginAsync()
	{
		return await Api.LoginAsync(Config.Username, Config.Password, "LoginAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Os.AMFOs.RunOsCheck")]
	public async Task<MspOsRun> RunOsAsync(string refid, string histogram)
	{
		return await Api.RunOsAsync(refid, histogram, "RunOsAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Os.AMFOs.CreateOsRef")]
	public async Task<MspOsRef> CreateRefAsync()
	{
		return await Api.CreateOsRefAsync("CreateRefAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.PiggyBank.AMFPiggyBankService.GetPiggyBank", IsTicketRequired = true)]
	internal async Task<MspPiggy> GetPiggyBankAsync()
	{
		return await Api.GetPiggyBankAsync("GetPiggyBankAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Common.AMFCommonWebService.LikeAdd", IsTicketRequired = true)]
	public async Task<MspLike> LikeAddAsync(int id, int actorid, LikeType type)
	{
		return await Api.LikeAddAsync(id, actorid, type, "LikeAddAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.SetMoodWithModerationCall", IsTicketRequired = true)]
	internal async Task<MspStatusUpdate> SetStatusAsync(Action<StatusMessageBuilder> builder)
	{
		return await Api.SetStatusAsync(builder.GetAction(), "SetStatusAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Bonster.AMFBonsterService.PetFriendBonster", IsTicketRequired = true)]
	internal async Task<MspResult<int>> LovePetAsync(int id)
	{
		return await Api.LovePetAsync(id, "LovePetAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Highscore.AMFHighscoreService.GetHighscoreBonster", IsTicketRequired = true)]
	public async Task<MspList<MspScorePet>> GetHighscorePetsAsync()
	{
		return await Api.GetHighscorePetsAsync("GetHighscorePetsAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Highscore.AMFHighscoreService.GetHighscoreLook", IsTicketRequired = true)]
	public async Task<MspList<MspScoreLook>> GetHighscoreLooksAsync()
	{
		return await Api.GetHighscoreLooksAsync("GetHighscoreLooksAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Highscore.AMFHighscoreService.GetHighscoreMovie", IsTicketRequired = true)]
	public async Task<MspList<MspScoreMovie>> GetHighscoreMoviesAsync()
	{
		return await Api.GetHighscoreMoviesAsync("GetHighscoreMoviesAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Highscore.AMFHighscoreService.GetHighscoreScrapBlog", IsTicketRequired = true)]
	public async Task<MspList<MspScoreArtbook>> GetHighscoreArtbooksAsync()
	{
		return await Api.GetHighscoreArtbooksAsync("GetHighscoreArtbooksAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Highscore.AMFHighscoreService.GetHighscoreActor", IsTicketRequired = true)]
	public async Task<MspList<MspScoreActor>> GetHighscoreActorsAsync()
	{
		return await Api.GetHighscoreActorsAsync("GetHighscoreActorsAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.AMFActorService.GetActorIdByName")]
	public async Task<MspId> GetActorIdAsync(string name)
	{
		return await Api.GetActorIdAsync(name, "GetActorIdAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.GetActorNameFromId")]
	public async Task<MspName> GetActorNameAsync(int id)
	{
		return await Api.GetActorNameAsync(id, "GetActorNameAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.AMFActorService.BulkLoadActors", IsTicketRequired = true)]
	public async Task<MspActor> GetActorAsync(int id)
	{
		return await Api.GetActorAsync("", id, "GetActorAsync");
	}

	public async Task<MspList<MspActor>> GetActorBulksAsync(List<int> ids)
	{
		return await Api.GetActorBulksAsync(ids, "GetActorAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.AMFActorService.BulkLoadActors", IsTicketRequired = true)]
	public async Task<MspActor> GetActorAsync(string name)
	{
		return await Api.GetActorAsync(name, 0, "GetActorAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.MovieService.AMFMovieService.RateMovie", IsTicketRequired = true)]
	internal async Task<MspMovieRated> RateMovieAsync(RateMovie movie)
	{
		return await Api.RateMovieAsync(movie, "RateMovieAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.MovieService.AMFMovieService.MovieWatched", IsTicketRequired = true)]
	internal async Task<MspMovieWatched> WatchMovieAsync(int id)
	{
		return await Api.WatchMovieAsync(id, "WatchMovieAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetMspActorSpecialSummary", IsTicketRequired = true)]
	internal async Task<MspStatus> GetActorStatusAsync(int id)
	{
		return await Api.GetActorStatusAsync(id, "GetActorStatusAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Profile.AMFProfileService.LoadProfileSummary", IsTicketRequired = true)]
	internal async Task<MspResult<DateTime>> GetActorCreatedAtAsync(int id)
	{
		return await Api.GetActorCreatedAtAsync(id, "GetActorCreatedAtAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.DailyCompetition.AMFDailyCompetitionService.getRandomItem", IsTicketRequired = true)]
	internal async Task<MspVote> GetVoteItemAsync()
	{
		return await Api.GetVoteItemAsync("GetVoteItemAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.DailyCompetition.AMFDailyCompetitionService.voteFor", IsTicketRequired = true)]
	internal async Task<MspResult<object>> VoteItemAsync(MspVote vote)
	{
		return await Api.VoteItemAsync(vote, "VoteItemAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.GiveAutographAndCalculateTimestamp", IsTicketRequired = true)]
	internal async Task<MspAutograph> SendAutographAsync(int id)
	{
		return await Api.SendAutographAsync(id, "SendAutographAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.AMFActorService.ThirdPartySaveAvatar", IsTicketRequired = false)]
	internal async Task<object> ThirdPartySaveAsync(MspActorCreation Data, string username, string password)
	{
		return await Api.ThirdPartySaveAsync(Data, Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/8721007.png").ToArray(), Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/8721007_face.png").ToArray(), username, password, "ThirdPartySaveAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Profile.AMFProfileService.loadActorRoom", IsTicketRequired = true)]
	internal async Task<MspRoom> GetRoomAsync(int id)
	{
		return await Api.GetRoomAsync(id, "GetRoomAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.DailyCompetition.AMFDailyCompetitionService.addToComp", IsTicketRequired = true)]
	public async Task<MspResult<double>> AddToCompetitionAsync(int id, int actorid)
	{
		return await Api.AddToCompetitionAsync(id, actorid, "AddToCompetitionAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetFriendListWithNameAndScore", IsTicketRequired = true)]
	internal async Task<MspList<MspFriend>> GetFriendsAsync()
	{
		return await Api.GetFriendsAsync("GetFriendsAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetFriendListWithNameAndScore", IsTicketRequired = true)]
	internal async Task<MspList<MspActor>> GetFriendsWithActorAsync()
	{
		return await Api.GetFriendsWithActorAsync("GetFriendsAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.AMFMobileFriendshipService.DeleteFriendship", IsTicketRequired = true)]
	internal async Task<MspResult<bool>> DeleteFriendAsync(int actorid)
	{
		return await Api.DeleteFriendAsync(actorid, "DeleteFriendAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.BeautyClinic.AMFBeautyClinicService.BuyManyBeautyClinicItems", IsTicketRequired = true)]
	internal async Task<MspResult<MspBeautyItem>> BuyBeautyItemAsync(MspBeautyItem item)
	{
		return await Api.BuyBeautyItemAsync(item, "BuyBeautyItemAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Spending.AMFSpendingService.BuyAnimation", IsTicketRequired = true)]
	internal async Task<MspAnimationBought> BuyAnimationAsync(int id)
	{
		return await Api.BuyAnimationAsync(id, "BuyAnimationAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.Pets.AMFPetService.GetClickItemsForActorWithPrice", IsTicketRequired = true)]
	internal async Task<MspList<MspBoonster>> GetActorBoonstersAsync(int id)
	{
		return await Api.GetActorBoonstersAsync(id, "GetActorBoonstersAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.MovieStar.AMFMovieStarService.GetPagedActorClothesByCategories", IsTicketRequired = false)]
	internal async Task<MspList<MspActorCloth>> GetActorClothAsync(int id)
	{
		return await Api.GetActorClothAsync(id, "GetActorClothAsync");
	}

	[AMFMethod("MovieStarPlanet.WebService.MovieStar.AMFMovieStarService.UpdateClothes", IsTicketRequired = true)]
	internal async Task<MspResult<object>> UpdateClothAsync(params int[] id)
	{
		return await Api.UpdateClothAsync("UpdateClothAsync", id);
	}
}
