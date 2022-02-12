using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dolo.PlanetAI.Entities;
using Dolo.PlanetAI.NET.Fluorine.IO;
using Dolo.PlanetAI.NET.Utils;
using Dolo.PlanetAI.Websocket;
using Dolo.PlanetAI.Websocket.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dolo.PlanetAI.NET;

internal class MspApi
{
	private string HashId;

	private readonly MspClient Client;

	private static readonly HttpClient Http = new HttpClient();

	private RestResponse<string> Endpoint;

	public static MspSocket Socket;

	public MspApi(MspClient client)
	{
		Client = client;
		HashId = Hash.HashID();
	}

	public void UpdateSessionId(string newsession)
	{
		HashId = newsession;
	}

	public static async Task<RestResponse<string>> GetWebSocketServerAsync()
	{
		return await GetStringAsync(Constant.PRESENCE_SERVER);
	}

	public static async Task<RestResponse<string>> GetWebSocketChatServerAsync(Server server, RoomType type)
	{
		return await GetStringAsync(Constant.WEBSOCKET_CHAT_SERVER.Replace("{CHATROOM}", Extension.GetChatServerSubDomain(server)).Replace("{TYPE}", Enum.GetName(type)).Replace("{SERVER}", MspClientUtil.GetServer(server)!.GetCode()));
	}

	public async Task<Task> SendAsyncSpecial(string method, bool needTicket, params object[] data)
	{
		return await Task.Factory.StartNew((Func<Task>)async delegate
		{
			await SendCustomAsync<object>(method, needTicket, data);
		});
	}

	public async Task<MspResult<object>> SendAsync(string method, bool needTicket, params object[] data)
	{
		RestResponse<object> req = await SendCustomAsync<object>(method, needTicket, data);
		return new MspResult<object>
		{
			HttpRequest = req.Request,
			HttpException = req.Exception,
			HttpResponse = req.Response,
			MovieStarPlanet = Client,
			Success = req.Success,
			Value = req.Result,
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspOsRef> CreateOsRefAsync([CallerMemberName] string mthd = "")
	{
		RestResponse<InternalOsRef> req = await SendAsync<InternalOsRef>(mthd, Array.Empty<object>());
		return new MspOsRef
		{
			MovieStarPlanet = Client,
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Created = (req.Success ? req.Result.Created : default(DateTime)),
			Data = (req.Success ? req.Result.TjData : null),
			RefId = (req.Success ? req.Result.RefId : null),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspOsRun> RunOsAsync(string refid, string histogram, [CallerMemberName] string mthd = "")
	{
		RestResponse<string> req = await SendAsync<string>(mthd, new object[2] { refid, histogram });
		return new MspOsRun
		{
			MovieStarPlanet = Client,
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			SessionId = (req.Success ? req.Result : null),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspResult<object>> VoteItemAsync(MspVote vote, [CallerMemberName] string mthd = "")
	{
		RestResponse<object> req = await SendAsync<object>(mthd, new object[5]
		{
			Client.User.Actor.Id,
			vote.ContentType,
			vote.ThemeId,
			(vote.ScoreA >= vote.ScoreB) ? vote.IdA : vote.IdB,
			(vote.ScoreA <= vote.ScoreB) ? vote.IdA : vote.IdB
		});
		return new MspResult<object>
		{
			MovieStarPlanet = Client,
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspResult<double>> AddToCompetitionAsync(int id, int actorid, [CallerMemberName] string mthd = "")
	{
		RestResponse<double> req = await SendAsync<double>(mthd, new object[3]
		{
			Client.User.Actor.Id,
			id,
			actorid
		});
		return new MspResult<double>
		{
			MovieStarPlanet = Client,
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Value = (req.Success ? req.Result : 0.0),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspAutograph> SendAutographAsync(int id, [CallerMemberName] string mthd = "")
	{
		RestResponse<object> req = await SendAsync<object>(mthd, new object[2]
		{
			Client.User.Actor.Id,
			id
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspAutograph res = new MspAutograph
		{
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		res.SendedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Convert.ToUInt64(JObject.Parse(req.Result.ToString())["Timestamp"])).ToLocalTime();
		res.Fame = Convert.ToUInt64(JObject.Parse(req.Result.ToString())["Fame"]);
		res.HasSended = res.Fame >= 20;
		return res;
	}

	public async Task<MspAnimationBought> BuyAnimationAsync(int id, [CallerMemberName] string mthd = "")
	{
		RestResponse<InternalMspAnimationBought> req = await SendAsync<InternalMspAnimationBought>(mthd, new object[2]
		{
			Client.User.Actor.Id,
			id
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspAnimationBought res = new MspAnimationBought
		{
			MovieStarPlanet = Client,
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		res.Success = true;
		res.Code = req.Result.Code;
		res.Description = req.Result.Description;
		res.Fame = req.Result.Fame;
		return res;
	}

	public async Task<MspResult<MspBeautyItem>> BuyBeautyItemAsync(MspBeautyItem item, [CallerMemberName] string mthd = "")
	{
		RestResponse<List<InternalBoughtBeautyItem>> req = await SendAsync<List<InternalBoughtBeautyItem>>(mthd, new object[2]
		{
			Client.User.Actor.Id,
			new List<MspBeautyItem> { item }.ToArray()
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspResult<MspBeautyItem> res = new MspResult<MspBeautyItem>
		{
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HasIpBan = req.HasIpBan,
			Value = new MspBeautyItem()
		};
		if (!req.Success)
		{
			return res;
		}
		InternalBoughtBeautyItem firstData = req.Result.First();
		res.Success = true;
		res.Value.Colors = firstData.Colors;
		res.Value.InventoryId = firstData.InventoryId;
		res.Value.IsWearing = firstData.IsWearing;
		res.Value.ItemId = firstData.ItemId;
		return res;
	}

	public async Task<MspList<MspBoonster>> GetActorBoonstersAsync(int id, [CallerMemberName] string mthd = "")
	{
		RestResponse<List<InternalBoonstersTmp>> req = await SendAsync<List<InternalBoonstersTmp>>(mthd, new object[1] { id }).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspBoonster> res = new MspList<MspBoonster>
		{
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		foreach (InternalBoonsters bonster in (from a in req.Result
			where a.ActorClickItem != null
			select a.ActorClickItem).ToList())
		{
			res.Add(new MspBoonster
			{
				MovieStarPlanet = Client,
				ActorId = bonster.ActorId,
				AtHotelUntil = bonster.AtHotelUntil,
				FoodPoints = bonster.FoodPoints,
				Id = bonster.Id,
				ItemId = bonster.ItemId,
				LastFeedTime = bonster.LastFeedTime,
				LastWashTime = bonster.LastWashTime,
				Name = bonster.Name,
				PlayPoints = bonster.PlayPoints,
				Stage = bonster.Stage,
				X = bonster.X,
				Y = bonster.Y
			});
		}
		return res;
	}

	public async Task<MspList<MspPet>> GetBonsterWithHighscoreAsync([CallerMemberName] string mthd = "")
	{
		MspList<MspScorePet> req = await Client.GetHighscorePetsAsync();
		MspList<MspPet> res = new MspList<MspPet>
		{
			HttpException = req.HttpException,
			HasIpBan = req.HasIpBan,
			HttpRequest = req.HttpRequest,
			HttpResponse = req.HttpResponse,
			Success = req.Success
		};
		if (!req.Success)
		{
			return res;
		}
		MspList<MspScoreActor> req2 = await Client.GetHighscoreActorsAsync();
		res.HttpException = req2.HttpException;
		res.HasIpBan = req2.HasIpBan;
		res.HttpRequest = req2.HttpRequest;
		res.HttpResponse = req2.HttpResponse;
		req.Success = req2.Success;
		if (!req2.Success)
		{
			return res;
		}
		foreach (MspScorePet pets in req)
		{
			res.Add(new MspPet
			{
				MovieStarPlanet = Client,
				Id = pets.Id
			});
		}
		foreach (MspScoreActor usr in req2)
		{
			MspList<MspBoonster> rd = await usr.GetBoonstersAsync();
			if (!rd.Success)
			{
				continue;
			}
			foreach (MspBoonster pet in rd)
			{
				res.Add(new MspPet
				{
					MovieStarPlanet = Client,
					Id = pet.Id
				});
			}
		}
		return res;
	}

	public async Task<MspResult<bool>> DeleteFriendAsync(int actorid, [CallerMemberName] string mthd = "")
	{
		RestResponse<object> req = await SendAsync<object>(mthd, new object[2]
		{
			Client.User.Actor.Id,
			actorid
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspResult<bool> res = new MspResult<bool>
		{
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = false
		};
		if (!req.Success)
		{
			return res;
		}
		MspList<MspFriend> tmpreq = await Client.GetFriendsAsync();
		res.Value = tmpreq.Success && !tmpreq.Any((MspFriend a) => a.Id == actorid);
		res.Success = true;
		return res;
	}

	public async Task<MspList<MspActor>> GetFriendsWithActorAsync([CallerMemberName] string mthd = "")
	{
		RestResponse<List<InternalFriend>> req = await SendAsync<List<InternalFriend>>(mthd, new object[1] { Client.User.Actor.Id }).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspActor> res = new MspList<MspActor>
		{
			Success = req.Success,
			HttpResponse = req.Response,
			HttpRequest = req.Request,
			HttpException = req.Exception
		};
		if (!req.Success)
		{
			return res;
		}
		return await Client.GetActorBulksAsync((from a in req.Result
			where a.ActorId != 3 && a.ActorId != 4 && a.ActorId != 1 && a.ActorId != 2 && a.ActorId != 273
			select a.ActorId).ToList());
	}

	public async Task<MspList<MspFriend>> GetFriendsAsync([CallerMemberName] string mthd = "")
	{
		RestResponse<List<InternalFriend>> req = await SendAsync<List<InternalFriend>>(mthd, new object[1] { Client.User.Actor.Id }).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspFriend> res = new MspList<MspFriend>
		{
			Success = req.Success,
			HttpResponse = req.Response,
			HttpRequest = req.Request,
			HttpException = req.Exception
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.ForEach(delegate(InternalFriend a)
		{
			res.Add(new MspFriend
			{
				BaseId = a.ActorId,
				MovieStarPlanet = Client,
				Username = a.Name,
				Fame = a.Fame,
				Fortune = a.Fortune,
				Friends = a.FriendCount,
				RecentlyLoggedIn = a.RecentlyLoggedIn,
				Id = a.ActorId,
				IsModerator = (a.Moderator != 0),
				Level = a.Level,
				IsVip = (DateTime.UtcNow < a.MembershipTimeoutDate),
				LastLoginAt = a.LastLogin,
				MembershipPurchasedAt = a.MembershipPurchasedDate,
				MembershipTimeoutAt = a.MembershipTimeoutDate,
				ProfileId = a.NebulaProfileId,
				StarCoins = a.Money,
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId),
				VipTier = ((!(DateTime.UtcNow < a.MembershipTimeoutDate) || !a.VipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)a.VipTier.Value))
			});
		});
		return res;
	}

	public async Task<MspPiggy> GetPiggyBankAsync([CallerMemberName] string mthd = "")
	{
		RestResponse<InternalPiggy> req = await SendAsync<InternalPiggy>(mthd, Array.Empty<object>());
		return new MspPiggy
		{
			MovieStarPlanet = Client,
			StarCoins = (req.Success ? req.Result.Data.StarCoins : 0),
			Diamonds = (req.Success ? req.Result.Data.Diamonds : 0),
			Fame = (req.Success ? req.Result.Data.Fame : 0),
			LastUpdatedAt = (req.Success ? DateTime.UtcNow : default(DateTime))
		};
	}

	public async Task<MspList<MspActorCloth>> GenerateLookAsync()
	{
		MspList<MspActorCloth> o = await Client.GetActorClothAsync(Client.User.Actor.Id);
		if (!o.Success)
		{
			return o;
		}
		MspList<MspActorCloth> res = new MspList<MspActorCloth>
		{
			Success = o.Success,
			HttpException = o.HttpException,
			HttpRequest = o.HttpRequest,
			HttpResponse = o.HttpResponse,
			HasIpBan = o.HasIpBan
		};
		Random xa = new Random();
		List<MspActorCloth> C = new List<MspActorCloth>();
		List<int> Data = new List<int>();
		List<MspActorCloth> rnd = new List<MspActorCloth>(o);
		MspActorCloth Head = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsHead)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsHead).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsHead).Count())]);
		MspActorCloth Accessories = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsAccessories)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsAccessories).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsAccessories).Count())]);
		MspActorCloth Bottom = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsBottom)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsBottom).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsBottom).Count())]);
		MspActorCloth Foot = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsFoot)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsFoot).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsFoot).Count())]);
		MspActorCloth Hair = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsHair)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsHair).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsHair).Count())]);
		MspActorCloth Stuff = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsStuff)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsStuff).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsStuff).Count())]);
		MspActorCloth Top = ((!rnd.Any((MspActorCloth a) => a.Cloth.IsTop)) ? null : rnd.Where((MspActorCloth a) => a.Cloth.IsTop).ToArray()[xa.Next(0, rnd.Where((MspActorCloth a) => a.Cloth.IsTop).Count())]);
		foreach (MspActorCloth dn in rnd)
		{
			Console.WriteLine(dn.Cloth.ClothesCategoryId);
		}
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsAccessories));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsBottom));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsFoot));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsHair));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsHead));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsStuff));
		Console.WriteLine(rnd.Count((MspActorCloth a) => a.Cloth.IsTop));
		if (Head != null)
		{
			C.Add(Head);
		}
		if (Accessories != null)
		{
			C.Add(Accessories);
		}
		if (Bottom != null)
		{
			C.Add(Bottom);
		}
		if (Foot != null)
		{
			C.Add(Foot);
		}
		if (Hair != null)
		{
			C.Add(Hair);
		}
		if (Stuff != null)
		{
			C.Add(Stuff);
		}
		if (Top != null)
		{
			C.Add(Top);
		}
		foreach (MspActorCloth i in C)
		{
			Data.Add(i.Id);
		}
		await Client.UpdateClothAsync(Data.ToArray()).ConfigureAwait(continueOnCapturedContext: false);
		res.AddRange(C);
		return res;
	}

	public async Task<MspList<MspActorCloth>> GetActorClothAsync(int actorid, [CallerMemberName] string mthd = "")
	{
		RestResponse<InternalActorClothPage> req = await SendAsync<InternalActorClothPage>(mthd, new object[4]
		{
			actorid,
			new int[99]
			{
				1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
				11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
				21, 22, 23, 224, 25, 26, 27, 28, 29, 30,
				31, 32, 33, 35, 36, 37, 38, 39, 40, 41,
				42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
				52, 53, 54, 55, 56, 57, 58, 59, 60, 61,
				62, 63, 64, 65, 66, 67, 68, 69, 70, 71,
				72, 73, 74, 75, 76, 77, 78, 79, 80, 81,
				82, 83, 84, 85, 86, 87, 88, 89, 90, 91,
				92, 93, 94, 95, 96, 97, 98, 99, 100
			},
			0,
			999
		});
		MspList<MspActorCloth> res = new MspList<MspActorCloth>
		{
			Success = req.Success,
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HasIpBan = req.HasIpBan,
			HttpResponse = req.Response
		};
		if (!req.Success)
		{
			return res;
		}
		InternalActorClothPageTmp[] pages = req.Result.Pages;
		foreach (InternalActorClothPageTmp data in pages)
		{
			res.Add(new MspActorCloth
			{
				ActorId = data.ActorId,
				Cloth = data.Cloth,
				ClothId = data.ClothId,
				Color = data.Color,
				GiftId = data.GiftId,
				Id = Convert.ToInt32(data.Id),
				SenderGiftId = data.SenderGiftId,
				X = data.X,
				Y = data.Y,
				IsWearing = data.IsWearing
			});
		}
		return res;
	}

	public async Task<MspVote> GetVoteItemAsync([CallerMemberName] string mthd = "")
	{
		RestResponse<InternalMspVote> req = await SendAsync<InternalMspVote>(mthd, new object[1] { Client.User.Actor.Id });
		return new MspVote
		{
			MovieStarPlanet = Client,
			HttpException = req.Exception,
			Success = req.Success,
			HttpRequest = ((!req.Success) ? null : req.Request),
			HttpResponse = ((!req.Success) ? null : req.Response),
			ActorIdA = (req.Success ? req.Result.ActorIdA : 0),
			ActorIdB = (req.Success ? req.Result.ActorIdB : 0),
			ContentType = (req.Success ? req.Result.ContentType : 0),
			IdA = (req.Success ? req.Result.IdA : 0),
			IdB = (req.Success ? req.Result.IdB : 0),
			ScoreA = ((!req.Success) ? 0 : req.Result.ScoreA),
			ScoreB = ((!req.Success) ? 0 : req.Result.ScoreB),
			ThemeId = (req.Success ? req.Result.ThemeId : 0),
			UsernameA = ((!req.Success) ? null : req.Result.nameA),
			UsernameB = ((!req.Success) ? null : req.Result.nameB),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspResult<object>> UpdateClothAsync([CallerMemberName] string mthd = "", params int[] id)
	{
		RestResponse<object> req = await SendAsync<object>(mthd, new object[2]
		{
			Client.User.Actor.Id,
			id
		});
		return new MspResult<object>
		{
			MovieStarPlanet = Client,
			HttpException = req.Exception,
			Success = req.Success,
			HttpRequest = ((!req.Success) ? null : req.Request),
			HttpResponse = ((!req.Success) ? null : req.Response),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspResult<bool>> ThirdPartySaveAsync(MspActorCreation data, byte[] img, byte[] image, string username, string password, [CallerMemberName] string mthd = "")
	{
		RestResponse<string> req = await SendAsync<string>(mthd, new object[5] { data, img, image, username, password });
		return new MspResult<bool>
		{
			HttpRequest = req.Request,
			HttpException = req.Exception,
			HttpResponse = req.Response,
			Success = req.Success,
			Value = (req.Result == "SUCCESS"),
			MovieStarPlanet = Client,
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspRoom> GetRoomAsync(int id, [CallerMemberName] string mthd = "")
	{
		RestResponse<InternalRoom> req = await SendAsync<InternalRoom>(mthd, new object[2]
		{
			id,
			Client.User.Actor.Id
		});
		return new MspRoom
		{
			HttpRequest = req.Request,
			HttpException = req.Exception,
			HttpResponse = req.Response,
			Success = req.Success,
			ActorId = (req.Success ? req.Result.ActorRoom.ActorId : 0),
			Floor = (req.Success ? req.Result.ActorRoom.Floor : null),
			HasLiked = (req.Success && req.Result.HasLiked != 0),
			Id = (req.Success ? req.Result.ActorRoom.ActorRoomId : 0),
			Likes = (req.Success ? req.Result.ActorRoom.RoomLikes : 0),
			Name = (req.Success ? req.Result.ActorRoom.Name : null),
			RoomUrl = (req.Success ? GetObfuscatedUrl(ObfuscationType.room, req.Result.ActorRoom.ActorId) : null),
			AvatarUrl = (req.Success ? GetMovieStarAvatarUrl(req.Result.ActorRoom.ActorId) : null),
			BodyUrl = (req.Success ? GetMovieStarBodyUrl(req.Result.ActorRoom.ActorId) : null),
			Wallpaper = (req.Success ? req.Result.ActorRoom.Wallpaper : null),
			MovieStarPlanet = Client,
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspLogin> LoginAsync(string username, string password, [CallerMemberName] string mthd = "")
	{
		if (!(await ScanSessionAsync()))
		{
			MspClient client = Client;
			MspLogin obj = new MspLogin
			{
				MovieStarPlanet = Client,
				StatusCode = LoginStatusCode.Connection,
				Server = Client.Config.Server,
				HttpException = null,
				Status = "You have a IP-Ban from MovieStarPlanet!",
				HasIpBan = true
			};
			MspLogin result = obj;
			client.User = obj;
			return result;
		}
		RestResponse<InternalLogin> req = await SendAsync<InternalLogin>(mthd, new object[6]
		{
			username,
			password,
			Array.Empty<object>(),
			null,
			null,
			"MSP1-Standalone:XXXXXX"
		});
		Client.User = new MspLogin
		{
			MovieStarPlanet = Client,
			StatusCode = LoginStatusCode.Connection,
			Server = Client.Config.Server,
			HttpException = ((!req.Success) ? req.Exception : null),
			Status = (req.HasIpBan ? "You have a IP-Ban from MovieStarPlanet" : (req.Success ? "" : req.Exception?.Message)),
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return Client.User;
		}
		if (req.Result.LoginStatus.Status != "Success" && req.Result.LoginStatus.Status != "ThirdPartyCreated")
		{
			switch (req.Result.LoginStatus.Status)
			{
			case "InvalidCredentials":
				Client.User.StatusCode = LoginStatusCode.InvalidCredentials;
				Client.User.Status = "Username or Password is incorrect.";
				break;
			case "Blocked":
				Client.User.StatusCode = LoginStatusCode.IpBanned;
				Client.User.Status = "Your IP address has been blocked.";
				break;
			case "LockedUntil":
				Client.User.StatusCode = LoginStatusCode.Banned;
				Client.User.Status = "Your account is temporarily blocked.";
				break;
			case "ERROR":
				Client.User.StatusCode = LoginStatusCode.Deleted;
				Client.User.Status = "Your account has been deleted.";
				break;
			case "ForceEntityChange":
				Client.User.Actor = new MspUser
				{
					Ticket = new MspTicket(req.Result.LoginStatus.Ticket)
				};
				Client.User.StatusCode = LoginStatusCode.Forced;
				Client.User.Status = "You are forced the Change the Username.";
				break;
			case "SiteMaintenance":
				Client.User.StatusCode = LoginStatusCode.Maintenace;
				Client.User.Status = "Website is currently under maintenance.";
				break;
			case "WhiteListBlocked":
				Client.User.StatusCode = LoginStatusCode.Whitelist;
				Client.User.Status = "You currently have no access to Msp";
				break;
			}
			return Client.User;
		}
		Client.User.Success = true;
		Client.User.HttpResponse = req.Response;
		Client.User.Status = "LoggedIn to " + req.Result.LoginStatus.Actor.Name;
		Client.User.StatusCode = LoginStatusCode.Success;
		Client.User.LoggedIn = true;
		Client.User.Actor = new MspUser
		{
			Server = Client.Config.Server,
			IsThirdParty = (req.Result.LoginStatus.Status == "ThirdPartyCreated"),
			Password = Client.Config.Password,
			MovieStarPlanet = Client.User.MovieStarPlanet,
			Ticket = new MspTicket(req.Result.LoginStatus.Ticket),
			PiggyBank = ((req.Result.LoginStatus.PiggyBank == null) ? new MspPiggy(0uL, 0uL, 0uL) : new MspPiggy(req.Result.LoginStatus.PiggyBank.StarCoins, req.Result.LoginStatus.PiggyBank.Fame, req.Result.LoginStatus.PiggyBank.Diamonds)
			{
				LastUpdatedAt = DateTime.UtcNow
			}),
			Nebula = new MspNebula(req.Result.LoginStatus.NebulaLoginStatus.AccessToken, req.Result.LoginStatus.NebulaLoginStatus.RefreshToken),
			Beauty = new MspBeauty
			{
				MovieStarPlanet = Client.User.MovieStarPlanet,
				Eye = new MspEye
				{
					Id = req.Result.LoginStatus.Actor.EyeId,
					Color = req.Result.LoginStatus.Actor.EyeColors
				},
				Nose = new MspNose
				{
					Id = req.Result.LoginStatus.Actor.NoseId
				},
				Mouth = new MspMouth
				{
					Id = req.Result.LoginStatus.Actor.MouthId,
					Color = req.Result.LoginStatus.Actor.MouthColors
				},
				EyeShadow = new MspEyeShadow
				{
					Id = req.Result.LoginStatus.Actor.EyeShadowId,
					Color = req.Result.LoginStatus.Actor.EyeShadowColors
				},
				Skincolor = req.Result.LoginStatus.Actor.SkinColor
			},
			WebServer = req.Result.LoginStatus?.Lbs?.FirstOrDefault()?.AbsoluteUri,
			Gender = ((!(req.Result.LoginStatus.Actor.Gender == "Female")) ? Gender.Male : Gender.Female),
			ProfileId = req.Result.LoginStatus.NebulaLoginStatus.ProfileId,
			Diamonds = req.Result.LoginStatus.Actor.Diamonds,
			Fame = req.Result.LoginStatus.Actor.Fame,
			Fortune = req.Result.LoginStatus.Actor.Fortune,
			StarCoins = req.Result.LoginStatus.Actor.Money,
			CreatedAt = req.Result.LoginStatus.Actor.Created,
			LastLoginAt = req.Result.LoginStatus.Actor.LastLogin,
			Id = req.Result.LoginStatus.Actor.ActorId,
			Username = req.Result.LoginStatus.Actor.Name,
			Level = req.Result.LoginStatus.Actor.Level,
			AvatarUrl = GetMovieStarAvatarUrl(req.Result.LoginStatus.Actor.ActorId),
			BodyUrl = GetMovieStarBodyUrl(req.Result.LoginStatus.Actor.ActorId),
			RoomUrl = GetObfuscatedUrl(ObfuscationType.room, req.Result.LoginStatus.Actor.ActorId),
			MembershiptPurchasedAt = req.Result.LoginStatus.Actor.MembershipPurchasedDate,
			MembershiptTimeoutAt = req.Result.LoginStatus.Actor.MembershipTimeoutDate,
			IsDeleted = (req.Result.LoginStatus.Actor.IsExtra != 0),
			IsVip = (DateTime.UtcNow < req.Result.LoginStatus.Actor.MembershipTimeoutDate),
			IsCeleb = (req.Result.LoginStatus.Actor.FriendCountVip >= 100),
			IsJudge = (req.Result.LoginStatus.Actor.TotalVipDays >= 180),
			IsJury = (req.Result.LoginStatus.Actor.TotalVipDays < 180 && req.Result.LoginStatus.Actor.TotalVipDays >= 90),
			IsModerator = (req.Result.LoginStatus.Actor.Moderator != 0),
			Email = req.Result.LoginStatus.Actor.Email,
			Friends = req.Result.LoginStatus.Actor.FriendCount,
			FriendsVip = req.Result.LoginStatus.Actor.FriendCountVip,
			LoginId = (req.Result.LoginStatus.NebulaLoginStatus.AccessToken.TryToSafeBase64(out var val) ? JObject.Parse(val)["loginId"]!.ToString() : null),
			VipDays = req.Result.LoginStatus.Actor.TotalVipDays,
			ProfileViews = req.Result.LoginStatus.Actor.ProfileDisplays,
			RecyclePoints = req.Result.LoginStatus.Actor.RecyclePoints,
			RoomLikes = req.Result.LoginStatus.Actor.RoomLikes,
			VipTier = ((!(DateTime.UtcNow < req.Result.LoginStatus.Actor.MembershipTimeoutDate) || !req.Result.LoginStatus.Actor.VipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)req.Result.LoginStatus.Actor.VipTier.Value))
		};
		Client.User.Actor.MaxFriends = GetMaxFriends((int)Client.User.Actor.VipTier, Client.User.Actor.Level);
		if (Client.Config.UseOriginalBehaviour)
		{
			List<Task> tasks = new List<Task>();
			List<Task> list = tasks;
			list.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.LoadActorDetailsExtended", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list2 = tasks;
			list2.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Session.AMFSessionServiceForWeb.GetLevelThresholds", false));
			List<Task> list3 = tasks;
			list3.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Moderation.AMFModeration.LoginEvent", true, req.Result.LoginStatus.Actor.Name));
			List<Task> list4 = tasks;
			list4.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Achievement.AMFAchievementWebService.CheckLoginAchievements", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list5 = tasks;
			list5.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Session.AMFSessionServiceForWeb.GetChatPermissionInfo", true));
			List<Task> list6 = tasks;
			list6.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.WorldTheme.AMFWorldThemeService.GetWorldThemeInfo", true));
			List<Task> list7 = tasks;
			list7.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Bonster.AMFBonsterService.GetBonsterCandyPrices", false));
			List<Task> list8 = tasks;
			list8.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Bonster.AMFBonsterService.GetBonsterTemplateList", false));
			List<Task> list9 = tasks;
			list9.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Spending.AMFSpendingService.GetEmoticonPackages", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list10 = tasks;
			list10.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.LoadBlockedAndBlockingActors", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list11 = tasks;
			list11.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.LoadBlockedAndBlockingActorsNeb", true, req.Result.LoginStatus.Actor.ActorId));
			RestResponse<string> ip = await GetWebSocketServerAsync();
			Socket = new MspSocket();
			await Socket.ConnectAsync(new Uri(Constant.WEBSOCKET_SERVER.Replace("{IP}", ip.Result.Replace("-", ".")).Replace("{IPSPLIT}", ip.Result)));
			await Socket.SendAsync("42[\"10\",{\"messageType\":10,\"messageContent\":{\"version\":3,\"applicationId\":\"APPLICATION_WEB\",\"country\":\"" + Client.User.Server.ToServerCode() + "\",\"username\":\"" + Client.User.Actor.ProfileId + "\",\"access_token\":\"" + Client.User.Actor.Nebula.AccessToken + "\"}}]");
			List<Task> list12 = tasks;
			list12.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.GetPostLoginBundleStandalone", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list13 = tasks;
			list13.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.MovieStar.AMFMovieStarService.LoadMovieStarListRevised", true, new int[1] { req.Result.LoginStatus.Actor.ActorId }));
			List<Task> list14 = tasks;
			list14.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetProfileTodosCount", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list15 = tasks;
			list15.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetFriendListWithNameAndScore", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list16 = tasks;
			list16.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.LoadMood", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list17 = tasks;
			list17.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Spending.AMFSpendingService.GetActiveSpecialsItems", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list18 = tasks;
			list18.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Activity.AMFActivityService.GetOfflineTodos", true, req.Result.LoginStatus.Actor.ActorId, 0, 100));
			List<Task> list19 = tasks;
			list19.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Quest.AMFQuestService.GetAllQuestStatusForDownloadableClient", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list20 = tasks;
			list20.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.LoadModeratorInformation", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list21 = tasks;
			list21.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.AnchorCharacter.AMFAnchorCharacterService.GetAnchorCharacterList", false));
			List<Task> list22 = tasks;
			list22.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.MovieStar.AMFMovieStarService.LoadMovieStarListRevised", true, new int[1] { 4 }));
			List<Task> list23 = tasks;
			list23.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Achievement.AMFAchievementWebService.GetClaimableCategories", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list24 = tasks;
			list24.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.UpdateBehaviourStatusNew", true, req.Result.LoginStatus.Actor.ActorId, 0, "", -1, -1));
			List<Task> list25 = tasks;
			list25.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.LoadModeratorInformation", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list26 = tasks;
			list26.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.ApproveDefaultAnchorFriendship", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list27 = tasks;
			list27.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.ApproveDefaultAnchorFriendship", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list28 = tasks;
			list28.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.UpdateGift", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list29 = tasks;
			list29.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.LoadActorDetailsExtended", true, req.Result.LoginStatus.Actor.ActorId));
			List<Task> list30 = tasks;
			list30.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.PiggyBank.AMFPiggyBankService.GetPiggyBank", true));
			List<Task> list31 = tasks;
			list31.Add(await SendAsyncSpecial("MovieStarPlanet.WebService.NotificationCenter.AMFNotificationCenterService.GetNotificationCount", true, req.Result.LoginStatus.Actor.ActorId));
			await Task.WhenAll(tasks.ToArray());
		}
		await Task.Factory.StartNew((Func<Task>)async delegate
		{
			while (Client != null && Client.User != null && Client.User.LoggedIn)
			{
				await Task.Delay(new TimeSpan(0, 0, 5));
				if (!(await ScanSessionAsync()))
				{
					break;
				}
			}
		});
		return Client.User;
	}

	public async Task<MspList<MspScorePet>> GetHighscorePetsAsync([CallerMemberName] string mth = "")
	{
		RestResponse<InternalScorePet> req = await Client.Api.SendAsync<InternalScorePet>(mth, new object[6]
		{
			Client.User.Actor.Id,
			false,
			true,
			"EXPERIENCE",
			0,
			2500
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspScorePet> res = new MspList<MspScorePet>
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.Pets.ForEach(delegate(InternalScorePetTmp a)
		{
			res.Add(new MspScorePet
			{
				MovieStarPlanet = Client,
				EvolutionStage = a.EvolutionStage,
				Experience = a.Experience,
				ActorId = a.ActorId,
				Username = a.Username,
				MembershipTimeoutAt = a.MembershipTimeoutDate,
				ShopId = a.BonsterId,
				ShopName = a.SkeletonName,
				SkeletonPath = a.SkeletonPath,
				Id = a.Id,
				Color = a.ColorPalette,
				Level = a.Level,
				Name = (string.IsNullOrEmpty(a.Name) ? a.Name : ((a.Name.Contains('\u001d') || a.Name.Contains('\u001f')) ? a.Name.Split(new string[2] { "\u001d", "\u001f" }, StringSplitOptions.None)[1] : a.Name)),
				TemplateId = a.BonsterTemplateId,
				VipTier = ((!(DateTime.UtcNow < a.MembershipTimeoutDate) || !a.VipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)a.VipTier.Value)),
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId)
			});
		});
		return res;
	}

	public async Task<MspList<MspScoreActor>> GetHighscoreActorsAsync([CallerMemberName] string mth = "")
	{
		RestResponse<InternalScoreActor> req = await Client.Api.SendAsync<InternalScoreActor>(mth, new object[6]
		{
			Client.User.Actor.Id,
			false,
			true,
			"LEVEL",
			0,
			5000
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspScoreActor> res = new MspList<MspScoreActor>
		{
			HttpException = req.Exception,
			HttpResponse = req.Response,
			HttpRequest = req.Request,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.Actors.ForEach(delegate(InternalScoreActorTmp a)
		{
			res.Add(new MspScoreActor
			{
				MovieStarPlanet = Client,
				BaseId = a.ActorId,
				Fame = a.Fame,
				Fortune = a.Fortune,
				IsDeleted = (a.IsExtra != 0),
				Friends = a.FriendCount,
				Id = a.ActorId,
				Level = a.Level,
				Username = a.Name,
				IsModerator = (a.Moderator != 0),
				LastLoginAt = a.LastLogin,
				MembershipPurchasedAt = a.MembershipPurchasedDate,
				MembershipTimeoutAt = a.MembershipTimeoutDate,
				RoomLikes = a.RoomLikes,
				StarCoins = a.Money,
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId),
				VipTier = ((!(DateTime.UtcNow < a.MembershipTimeoutDate) || !a.VipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)a.VipTier.Value))
			});
		});
		return res;
	}

	public async Task<MspList<MspScoreMovie>> GetHighscoreMoviesAsync([CallerMemberName] string mth = "")
	{
		RestResponse<InternalScoreMovie> req = await Client.Api.SendAsync<InternalScoreMovie>(mth, new object[6]
		{
			Client.User.Actor.Id,
			false,
			true,
			"TOTALWATCHED",
			0,
			2500
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspScoreMovie> res = new MspList<MspScoreMovie>
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.Movies.ForEach(delegate(InternalScoreMovieTmp a)
		{
			res.Add(new MspScoreMovie
			{
				MovieStarPlanet = Client,
				ActorId = a.ActorId,
				StarCoinsEarned = a.StarCoinsEarned,
				Username = a.ActorName,
				State = a.State,
				Guid = a.Guid,
				HasRated = a.Rated,
				HasWatched = a.Watched,
				MembershipPurchasedAt = a.MembershipPurchasedDate,
				MembershipTimeoutAt = a.MembershipTimeoutDate,
				Name = (string.IsNullOrEmpty(a.Name) ? a.Name : ((a.Name.Contains("\u001d") || a.Name.Contains("\u001f")) ? a.Name.Split(new string[2] { "\u001d", "\u001f" }, StringSplitOptions.None)[1] : a.Name)),
				PublishedAt = a.PublishedDate,
				RatedCount = a.RatedCount,
				Rating = a.AverageRating,
				WatchedCount = a.WatchedActorCount,
				Id = a.MovieId,
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId),
				MovieUrl = GetObfuscatedUrl(ObfuscationType.movie, a.MovieId)
			});
		});
		return res;
	}

	public async Task<MspList<MspScoreLook>> GetHighscoreLooksAsync([CallerMemberName] string mth = "")
	{
		RestResponse<InternalScoreLook> req = await Client.Api.SendAsync<InternalScoreLook>(mth, new object[6]
		{
			Client.User.Actor.Id,
			false,
			true,
			"LIKES",
			0,
			2500
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspScoreLook> res = new MspList<MspScoreLook>
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.Look.ForEach(delegate(InternalScoreLookTmp a)
		{
			res.Add(new MspScoreLook
			{
				MovieStarPlanet = Client,
				ActorId = a.ActorId,
				CreatedAt = a.Created,
				MembershipPurchasedAt = a.CreatorMembershipPurchasedDate,
				MembershipTimeoutAt = a.CreatorMembershipTimeoutDate,
				Id = a.LookId,
				Likes = a.Likes,
				Name = (string.IsNullOrEmpty(a.Headline) ? a.Headline : ((a.Headline.Contains("\u001d") || a.Headline.Contains("\u001f")) ? a.Headline.Split(new string[2] { "\u001d", "\u001f" }, StringSplitOptions.None)[1] : a.Headline)),
				Sells = a.Sells,
				Username = a.CreatorName,
				VipTier = ((!(DateTime.UtcNow < a.CreatorMembershipTimeoutDate) || !a.CreatorVipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)a.CreatorVipTier.Value)),
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId),
				LookUrl = GetObfuscatedUrl(ObfuscationType.look, a.LookId)
			});
		});
		return res;
	}

	public async Task<MspList<MspScoreArtbook>> GetHighscoreArtbooksAsync([CallerMemberName] string mth = "")
	{
		RestResponse<InternalScoreArtbook> req = await Client.Api.SendAsync<InternalScoreArtbook>(mth, new object[6]
		{
			Client.User.Actor.Id,
			false,
			true,
			"FAMEEARNED",
			0,
			2500
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspList<MspScoreArtbook> res = new MspList<MspScoreArtbook>
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		req.Result.Artbook.ForEach(delegate(InternalScoreArtbookTmp a)
		{
			res.Add(new MspScoreArtbook
			{
				MovieStarPlanet = Client,
				ActorId = a.ActorId,
				CreatedAt = a.CreatedAt,
				MembershipPurchasedAt = a.ScrapActor.MembershipPurchasedAt,
				MembershipTimeoutAt = a.ScrapActor.MembershipTimeoutAt,
				IsDeleted = (a.Deleted != 0),
				FameEarned = a.FameEarned,
				Comments = a.CommentsCount,
				PublishedAt = a.PublishedAt,
				IsModerator = (a.ScrapActor.Moderator != 0),
				Name = a.Name,
				Id = a.ScrapBlogId,
				Username = a.ScrapActor.Name,
				Likes = a.Likes,
				Status = a.Status,
				TemplateType = a.TemplateType,
				Type = a.ScrapBlogType,
				VipTier = ((!(DateTime.UtcNow < a.ScrapActor.MembershipTimeoutAt) || !a.ScrapActor.VipTier.HasValue) ? VipTierType.NON_VIP : ((VipTierType)a.ScrapActor.VipTier.Value)),
				AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
				BodyUrl = GetMovieStarBodyUrl(a.ActorId),
				RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId),
				ArtbookUrl = GetObfuscatedUrl(ObfuscationType.scrapblog, a.ScrapBlogId)
			});
		});
		return res;
	}

	public async Task<MspResult<DateTime>> GetActorCreatedAtAsync(int id, [CallerMemberName] string mth = "")
	{
		RestResponse<string> req = await Client.Api.SendAsync<string>(mth, new object[2]
		{
			id,
			Client.User.Actor.Id
		}).ConfigureAwait(continueOnCapturedContext: false);
		return new MspResult<DateTime>
		{
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			Value = (req.Success ? JObject.Parse(req.Json)["Created"]!.ToObject<DateTime>() : default(DateTime)),
			MovieStarPlanet = Client,
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspName> GetActorNameAsync(int id, [CallerMemberName] string mth = "")
	{
		RestResponse<string> req = await Client.Api.SendAsync<string>(mth, new object[1] { id }).ConfigureAwait(continueOnCapturedContext: false);
		return new MspName
		{
			MovieStarPlanet = Client,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			IsAvailable = !string.IsNullOrEmpty(req.Result),
			Username = req.Result,
			Id = id,
			BaseId = id,
			RoomUrl = ((!string.IsNullOrEmpty(req.Result)) ? GetObfuscatedUrl(ObfuscationType.room, id) : ""),
			AvatarUrl = ((!string.IsNullOrEmpty(req.Result)) ? GetMovieStarAvatarUrl(id) : ""),
			BodyUrl = ((!string.IsNullOrEmpty(req.Result)) ? GetMovieStarBodyUrl(id) : ""),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspId> GetActorIdAsync(string name, [CallerMemberName] string mth = "")
	{
		RestResponse<ulong> req = await Client.Api.SendAsync<ulong>(mth, new object[1] { name }).ConfigureAwait(continueOnCapturedContext: false);
		return new MspId
		{
			MovieStarPlanet = Client,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			BaseId = (req.Success ? Convert.ToInt32(req.Result) : 0),
			Id = (req.Success ? Convert.ToInt32(req.Result) : 0),
			Username = name,
			IsAvailable = (req.Success && Convert.ToInt32(req.Result) != 0),
			RoomUrl = ((req.Success && Convert.ToInt32(req.Result) != 0) ? GetObfuscatedUrl(ObfuscationType.room, Convert.ToInt32(req.Result)) : ""),
			AvatarUrl = ((req.Success && Convert.ToInt32(req.Result) != 0) ? GetMovieStarAvatarUrl(Convert.ToInt32(req.Result)) : ""),
			BodyUrl = ((req.Success && Convert.ToInt32(req.Result) != 0) ? GetMovieStarBodyUrl(Convert.ToInt32(req.Result)) : ""),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspActor> GetActorAsync(string name = "", int id = 0, [CallerMemberName] string mth = "")
	{
		if (id == 0)
		{
			MspId act = await Client.GetActorIdAsync(name);
			if (!act.Success || !act.IsAvailable)
			{
				return new MspActor
				{
					MovieStarPlanet = Client,
					HttpException = act.HttpException,
					Success = act.Success,
					HttpRequest = act.HttpRequest,
					HttpResponse = act.HttpResponse,
					IsAvailable = false,
					HasIpBan = act.HasIpBan
				};
			}
			id = act.Id;
		}
		MspActor res;
		InternalActor mspActor;
		do
		{
			RestResponse<List<InternalActor>> req = await Client.Api.SendAsync<List<InternalActor>>(mth, new object[1] { new List<int> { id }.ToArray() }).ConfigureAwait(continueOnCapturedContext: false);
			res = new MspActor
			{
				MovieStarPlanet = Client,
				HttpRequest = req.Request,
				HttpResponse = req.Response,
				HttpException = req.Exception,
				Success = req.Success,
				HasIpBan = req.HasIpBan
			};
			if (!req.Success)
			{
				return res;
			}
			mspActor = req.Result.First();
		}
		while (mspActor.Level > 0 && mspActor.Fortune == 0L && mspActor.Fame == 0);
		res.ClothRel = new List<MspClothRel>();
		res.BaseId = mspActor.ActorId;
		mspActor.ActorClothesRels.ToList().ForEach(delegate(InternalActorClothesRel a)
		{
			res.ClothRel.Add(new MspClothRel
			{
				ActorId = a.ActorId,
				ClothId = a.ClothesId,
				Color = a.Color,
				Id = a.ActorClothesRelId,
				IsWearing = (a.IsWearing != 0),
				Cloth = new MspCloth
				{
					CategoryId = a.Cloth.ClothesCategoryId,
					ColorScheme = a.Cloth.ColorScheme,
					DiamondsPrice = a.Cloth.DiamondsPrice,
					Discount = a.Cloth.Discount,
					Id = a.Cloth.ClothesCategoryId,
					IsNew = (a.Cloth.IsNew != 0),
					IsVip = (a.Cloth.Vip != 0),
					LastUpdatedAt = a.Cloth.LastUpdated,
					ThemeId = a.Cloth.ThemeId,
					Name = a.Cloth.Name,
					Price = a.Cloth.Price,
					Scale = a.Cloth.Scale,
					ShopId = a.Cloth.ShopId,
					SkinId = a.Cloth.SkinId,
					Sortorder = a.Cloth.Sortorder,
					Filename = a.Cloth.Swf,
					SwfUrl = GetCategoryUrl(a.Cloth.ClothesCategoryId) + a.Cloth.Swf.Replace(" ", "%20") + ".swf",
					Category = new MspClothCategory
					{
						Id = a.Cloth.ClothesCategory.ClothesCategoryId,
						Name = a.Cloth.ClothesCategory.Name,
						SlotTypeId = a.Cloth.ClothesCategory.SlotTypeId
					}
				}
			});
		});
		res.Beauty = new MspBeauty
		{
			MovieStarPlanet = Client,
			Skincolor = mspActor.SkinColor,
			Eye = new MspEye
			{
				Id = mspActor.Eye.EyeId,
				LastUpdatedAt = mspActor.Eye.LastUpdated,
				Color = mspActor.EyeColors,
				DefaultColors = mspActor.Eye.DefaultColors,
				DiamondsPrice = mspActor.Eye.DiamondsPrice,
				Discount = mspActor.Eye.Discount,
				IsDragonBone = mspActor.Eye.DragonBone,
				IsHidden = mspActor.Eye.Hidden,
				SkinId = mspActor.Eye.SkinId,
				IsNew = (mspActor.Eye.IsNew != 0),
				IsVip = (mspActor.Eye.Vip != 0),
				Price = mspActor.Eye.Price,
				Sortorder = mspActor.Eye.Sortorder,
				Swf = mspActor.Eye.Swf
			},
			Mouth = new MspMouth
			{
				Id = mspActor.Mouth.EyeId,
				LastUpdatedAt = mspActor.Mouth.LastUpdated,
				DefaultColors = mspActor.Mouth.DefaultColors,
				DiamondsPrice = mspActor.Mouth.DiamondsPrice,
				Discount = mspActor.Mouth.Discount,
				IsDragonBone = mspActor.Mouth.DragonBone,
				IsHidden = mspActor.Mouth.Hidden,
				SkinId = mspActor.Mouth.SkinId,
				IsNew = (mspActor.Mouth.IsNew != 0),
				IsVip = (mspActor.Mouth.Vip != 0),
				Price = mspActor.Mouth.Price,
				Sortorder = mspActor.Mouth.Sortorder,
				Swf = mspActor.Mouth.Swf,
				Color = mspActor.MouthColors
			},
			Nose = new MspNose
			{
				Id = mspActor.Nose.EyeId,
				LastUpdatedAt = mspActor.Nose.LastUpdated,
				DefaultColors = mspActor.Nose.DefaultColors,
				DiamondsPrice = mspActor.Nose.DiamondsPrice,
				Discount = mspActor.Nose.Discount,
				IsDragonBone = mspActor.Nose.DragonBone,
				IsHidden = mspActor.Nose.Hidden,
				SkinId = mspActor.Nose.SkinId,
				IsNew = (mspActor.Nose.IsNew != 0),
				IsVip = (mspActor.Nose.Vip != 0),
				Price = mspActor.Nose.Price,
				Sortorder = mspActor.Nose.Sortorder,
				Swf = mspActor.Nose.Swf
			},
			EyeShadow = ((mspActor.EyeShadow == null) ? null : new MspEyeShadow
			{
				Id = mspActor.EyeShadow.EyeId,
				LastUpdatedAt = mspActor.EyeShadow.LastUpdated,
				DefaultColors = mspActor.EyeShadow.DefaultColors,
				DiamondsPrice = mspActor.EyeShadow.DiamondsPrice,
				Discount = mspActor.EyeShadow.Discount,
				IsDragonBone = mspActor.EyeShadow.DragonBone,
				IsHidden = mspActor.EyeShadow.Hidden,
				SkinId = mspActor.EyeShadow.SkinId,
				IsNew = (mspActor.EyeShadow.IsNew != 0),
				IsVip = (mspActor.EyeShadow.Vip != 0),
				Price = mspActor.EyeShadow.Price,
				Sortorder = mspActor.EyeShadow.Sortorder,
				Swf = mspActor.EyeShadow.Swf,
				Color = mspActor.EyeShadowColors
			})
		};
		res.Id = mspActor.ActorId;
		res.Username = mspActor.Name;
		res.ProfileId = mspActor.NebulaProfileId;
		res.Level = mspActor.Level;
		res.Friends = mspActor.FriendCount;
		res.FriendsVip = mspActor.FriendCountVip;
		res.MembershiptPurchasedAt = mspActor.MembershipPurchasedDate;
		res.MembershiptTimeoutAt = mspActor.MembershipTimeoutDate;
		res.LastLoginAt = mspActor.LastLogin;
		res.IsModerator = mspActor.Moderator != 0;
		res.IsVip = DateTime.UtcNow < mspActor.MembershipTimeoutDate;
		res.IsCeleb = mspActor.FriendCountVip >= 100;
		res.IsJudge = mspActor.TotalVipDays >= 180;
		res.IsJury = mspActor.TotalVipDays < 180 && mspActor.TotalVipDays >= 90;
		res.IsDeleted = mspActor.IsExtra != 0;
		res.VipTier = ((!(DateTime.UtcNow < mspActor.MembershipTimeoutDate) || mspActor.VipTier == 0) ? VipTierType.NON_VIP : ((VipTierType)mspActor.VipTier));
		res.Gender = ((!(mspActor.SkinSwf == "femaleskin")) ? Gender.Male : Gender.Female);
		res.StarCoins = mspActor.Money;
		res.Diamonds = mspActor.Diamonds;
		res.Fame = mspActor.Fame;
		res.Fortune = mspActor.Fortune;
		res.MaxFriends = GetMaxFriends((int)res.VipTier, res.Level);
		res.AvatarUrl = GetMovieStarAvatarUrl(res.Id);
		res.BodyUrl = GetMovieStarBodyUrl(res.Id);
		res.RoomUrl = GetObfuscatedUrl(ObfuscationType.room, res.Id);
		res.BaseId = mspActor.ActorId;
		return res;
	}

	public async Task<MspList<MspActor>> GetActorBulksAsync(List<int> ids, [CallerMemberName] string mth = "")
	{
		MspList<MspActor> res;
		while (true)
		{
			IL_000f:
			RestResponse<List<InternalActor>> req = await Client.Api.SendAsync<List<InternalActor>>(mth, new object[1] { ids.ToArray() }).ConfigureAwait(continueOnCapturedContext: false);
			res = new MspList<MspActor>
			{
				HttpRequest = req.Request,
				HttpResponse = req.Response,
				HttpException = req.Exception,
				Success = req.Success,
				HasIpBan = req.HasIpBan
			};
			if (!req.Success)
			{
				return res;
			}
			foreach (InternalActor mspActor in req.Result)
			{
				if (mspActor != null)
				{
					if (mspActor.Level > 0 && mspActor.Fortune == 0L && mspActor.Fame == 0)
					{
						goto IL_000f;
					}
					MspActor actor = new MspActor
					{
						ClothRel = new List<MspClothRel>(),
						BaseId = mspActor.ActorId
					};
					mspActor.ActorClothesRels.ToList().ForEach(delegate(InternalActorClothesRel a)
					{
						actor.ClothRel.Add(new MspClothRel
						{
							ActorId = a.ActorId,
							ClothId = a.ClothesId,
							Color = a.Color,
							Id = a.ActorClothesRelId,
							IsWearing = (a.IsWearing != 0),
							Cloth = new MspCloth
							{
								CategoryId = a.Cloth.ClothesCategoryId,
								ColorScheme = a.Cloth.ColorScheme,
								DiamondsPrice = a.Cloth.DiamondsPrice,
								Discount = a.Cloth.Discount,
								Id = a.Cloth.ClothesCategoryId,
								IsNew = (a.Cloth.IsNew != 0),
								IsVip = (a.Cloth.Vip != 0),
								LastUpdatedAt = a.Cloth.LastUpdated,
								ThemeId = a.Cloth.ThemeId,
								Name = a.Cloth.Name,
								Price = a.Cloth.Price,
								Scale = a.Cloth.Scale,
								ShopId = a.Cloth.ShopId,
								SkinId = a.Cloth.SkinId,
								Sortorder = a.Cloth.Sortorder,
								Filename = a.Cloth.Swf,
								SwfUrl = GetCategoryUrl(a.Cloth.ClothesCategoryId) + a.Cloth.Swf.Replace(" ", "%20") + ".swf",
								Category = new MspClothCategory
								{
									Id = a.Cloth.ClothesCategory.ClothesCategoryId,
									Name = a.Cloth.ClothesCategory.Name,
									SlotTypeId = a.Cloth.ClothesCategory.SlotTypeId
								}
							}
						});
					});
					actor.MovieStarPlanet = Client;
					actor.Beauty = new MspBeauty
					{
						MovieStarPlanet = Client,
						Skincolor = mspActor.SkinColor,
						Eye = new MspEye
						{
							Id = mspActor.Eye.EyeId,
							LastUpdatedAt = mspActor.Eye.LastUpdated,
							Color = mspActor.EyeColors,
							DefaultColors = mspActor.Eye.DefaultColors,
							DiamondsPrice = mspActor.Eye.DiamondsPrice,
							Discount = mspActor.Eye.Discount,
							IsDragonBone = mspActor.Eye.DragonBone,
							IsHidden = mspActor.Eye.Hidden,
							SkinId = mspActor.Eye.SkinId,
							IsNew = (mspActor.Eye.IsNew != 0),
							IsVip = (mspActor.Eye.Vip != 0),
							Price = mspActor.Eye.Price,
							Sortorder = mspActor.Eye.Sortorder,
							Swf = mspActor.Eye.Swf
						},
						Mouth = new MspMouth
						{
							Id = mspActor.Mouth.EyeId,
							LastUpdatedAt = mspActor.Mouth.LastUpdated,
							DefaultColors = mspActor.Mouth.DefaultColors,
							DiamondsPrice = mspActor.Mouth.DiamondsPrice,
							Discount = mspActor.Mouth.Discount,
							IsDragonBone = mspActor.Mouth.DragonBone,
							IsHidden = mspActor.Mouth.Hidden,
							SkinId = mspActor.Mouth.SkinId,
							IsNew = (mspActor.Mouth.IsNew != 0),
							IsVip = (mspActor.Mouth.Vip != 0),
							Price = mspActor.Mouth.Price,
							Sortorder = mspActor.Mouth.Sortorder,
							Swf = mspActor.Mouth.Swf,
							Color = mspActor.MouthColors
						},
						Nose = new MspNose
						{
							Id = mspActor.Nose.EyeId,
							LastUpdatedAt = mspActor.Nose.LastUpdated,
							DefaultColors = mspActor.Nose.DefaultColors,
							DiamondsPrice = mspActor.Nose.DiamondsPrice,
							Discount = mspActor.Nose.Discount,
							IsDragonBone = mspActor.Nose.DragonBone,
							IsHidden = mspActor.Nose.Hidden,
							SkinId = mspActor.Nose.SkinId,
							IsNew = (mspActor.Nose.IsNew != 0),
							IsVip = (mspActor.Nose.Vip != 0),
							Price = mspActor.Nose.Price,
							Sortorder = mspActor.Nose.Sortorder,
							Swf = mspActor.Nose.Swf
						},
						EyeShadow = ((mspActor.EyeShadow == null) ? null : new MspEyeShadow
						{
							Id = mspActor.EyeShadow.EyeId,
							LastUpdatedAt = mspActor.EyeShadow.LastUpdated,
							DefaultColors = mspActor.EyeShadow.DefaultColors,
							DiamondsPrice = mspActor.EyeShadow.DiamondsPrice,
							Discount = mspActor.EyeShadow.Discount,
							IsDragonBone = mspActor.EyeShadow.DragonBone,
							IsHidden = mspActor.EyeShadow.Hidden,
							SkinId = mspActor.EyeShadow.SkinId,
							IsNew = (mspActor.EyeShadow.IsNew != 0),
							IsVip = (mspActor.EyeShadow.Vip != 0),
							Price = mspActor.EyeShadow.Price,
							Sortorder = mspActor.EyeShadow.Sortorder,
							Swf = mspActor.EyeShadow.Swf,
							Color = mspActor.EyeShadowColors
						})
					};
					actor.Id = mspActor.ActorId;
					actor.Username = mspActor.Name;
					actor.ProfileId = mspActor.NebulaProfileId;
					actor.Level = mspActor.Level;
					actor.Friends = mspActor.FriendCount;
					actor.FriendsVip = mspActor.FriendCountVip;
					actor.MembershiptPurchasedAt = mspActor.MembershipPurchasedDate;
					actor.MembershiptTimeoutAt = mspActor.MembershipTimeoutDate;
					actor.LastLoginAt = mspActor.LastLogin;
					actor.IsModerator = mspActor.Moderator != 0;
					actor.IsVip = DateTime.UtcNow < mspActor.MembershipTimeoutDate;
					actor.IsCeleb = mspActor.FriendCountVip >= 100;
					actor.IsJudge = mspActor.TotalVipDays >= 180;
					actor.IsJury = mspActor.TotalVipDays < 180 && mspActor.TotalVipDays >= 90;
					actor.IsDeleted = mspActor.IsExtra != 0;
					actor.VipTier = ((!(DateTime.UtcNow < mspActor.MembershipTimeoutDate) || mspActor.VipTier == 0) ? VipTierType.NON_VIP : ((VipTierType)mspActor.VipTier));
					actor.Gender = ((!(mspActor.SkinSwf == "femaleskin")) ? Gender.Male : Gender.Female);
					actor.StarCoins = mspActor.Money;
					actor.Diamonds = mspActor.Diamonds;
					actor.Fame = mspActor.Fame;
					actor.Fortune = mspActor.Fortune;
					actor.MaxFriends = GetMaxFriends((int)actor.VipTier, actor.Level);
					actor.AvatarUrl = GetMovieStarAvatarUrl(actor.Id);
					actor.BodyUrl = GetMovieStarBodyUrl(actor.Id);
					actor.RoomUrl = GetObfuscatedUrl(ObfuscationType.room, actor.Id);
					actor.BaseId = mspActor.ActorId;
					res.Add(actor);
				}
			}
			break;
		}
		return res;
	}

	public async Task<MspStatus> GetActorStatusAsync(int id, [CallerMemberName] string mth = "")
	{
		RestResponse<InternalStatus> req = await Client.Api.SendAsync<InternalStatus>(mth, new object[3]
		{
			id,
			Client.User.Actor.Id,
			true
		}).ConfigureAwait(continueOnCapturedContext: false);
		MspStatus res = new MspStatus
		{
			MovieStarPlanet = Client,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			HasIpBan = req.HasIpBan
		};
		if (!req.Success)
		{
			return res;
		}
		List<MspSpecialFriend> friends = new List<MspSpecialFriend>();
		if (req.Result.SpecialFriends != null)
		{
			req.Result.SpecialFriends.ToList().ForEach(delegate(InternalSpecialFriend a)
			{
				friends.Add(new MspSpecialFriend
				{
					BaseId = a.ActorId,
					MovieStarPlanet = Client,
					Id = a.ActorId,
					Name = a.Name,
					Gender = ((!(a.SkinSwf == "femaleskin")) ? Gender.Male : Gender.Female),
					ProfileId = a.NebulaProfileId,
					AvatarUrl = GetMovieStarAvatarUrl(a.ActorId),
					BodyUrl = GetMovieStarBodyUrl(a.ActorId),
					RoomUrl = GetObfuscatedUrl(ObfuscationType.room, a.ActorId)
				});
			});
		}
		if (req.Result.AnimationMood != null)
		{
			res.ActorId = req.Result.AnimationMood.ActorId;
			res.Id = req.Result.AnimationMood.WallPostId;
			res.Text = (string.IsNullOrEmpty(req.Result.AnimationMood.TextLine) ? req.Result.AnimationMood.TextLine : ((req.Result.AnimationMood.TextLine.Contains("\u001d") || req.Result.AnimationMood.TextLine.Contains("\u001f")) ? req.Result.AnimationMood.TextLine.Split(new string[2] { "\u001d", "\u001f" }, StringSplitOptions.None)[1] : req.Result.AnimationMood.TextLine));
			res.Likes = req.Result.AnimationMood.Likes;
			res.LastUpdatedAt = req.Result.LastUpdated;
			res.MouthAnimation = req.Result.AnimationMood.MouthAnimation;
			res.FigureAnimation = req.Result.AnimationMood.FigureAnimation;
			res.FaceAnimation = req.Result.AnimationMood.FaceAnimation;
			res.AvatarUrl = GetMovieStarAvatarUrl(req.Result.AnimationMood.ActorId);
			res.BodyUrl = GetMovieStarBodyUrl(req.Result.AnimationMood.ActorId);
			res.RoomUrl = GetObfuscatedUrl(ObfuscationType.room, req.Result.AnimationMood.ActorId);
			res.HasStatus = true;
		}
		res.SpecialFriend = friends;
		return res;
	}

	public async Task<MspStatusUpdate> SetStatusAsync(StatusMessageBuilder Builder, [CallerMemberName] string mth = "")
	{
		Builder.MovieStarPlanet = Client;
		Status status = new Status(Builder);
		RestResponse<InternalStatusUpdate> req = await Client.Api.SendAsync<InternalStatusUpdate>(mth, new object[4]
		{
			status,
			Client.User.Actor.Username,
			status.GetColor(),
			false
		}).ConfigureAwait(continueOnCapturedContext: false);
		return new MspStatusUpdate
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			MovieStarPlanet = Client,
			Status = new Status(Builder),
			Success = req.Success,
			Updated = (req.Success && req.Result.FilterTextResult.IsMessageOk),
			ChangedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(req.Result.FilterTextResult.TimeStamp).ToLocalTime(),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspResult<int>> LovePetAsync(int id, [CallerMemberName] string mth = "")
	{
		RestResponse<long> req = await Client.Api.SendAsync<long>(mth, new object[2]
		{
			Client.User.Actor.Id,
			id
		}).ConfigureAwait(continueOnCapturedContext: false);
		return new MspResult<int>
		{
			HttpException = req.Exception,
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			MovieStarPlanet = Client,
			Success = req.Success,
			Value = (req.Success ? Convert.ToInt32(req.Result) : 0),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspLike> LikeAddAsync(int id, int actorid, LikeType type, [CallerMemberName] string mth = "")
	{
		RestResponse<MspLike> R = await Client.Api.SendAsync<MspLike>(mth, new object[4]
		{
			Enum.GetName(typeof(LikeType), type),
			id,
			Client.User.Actor.Id,
			actorid
		}).ConfigureAwait(continueOnCapturedContext: false);
		return new MspLike
		{
			HasLiked = (R.Success && R.Result.HasLiked),
			HttpRequest = R.Request,
			HttpResponse = R.Response,
			MovieStarPlanet = Client,
			Success = R.Success,
			StarCoins = (ulong)((!R.Success || !R.Result.HasLiked || type != LikeType.scrapblog || type != LikeType.scrapblogBig) ? 1 : 0),
			HasIpBan = R.HasIpBan
		};
	}

	public async Task<MspMovieRated> RateMovieAsync(RateMovie movie, [CallerMemberName] string mth = "")
	{
		RestResponse<object> req = await Client.Api.SendAsync<object>(mth, new object[1] { movie }).ConfigureAwait(continueOnCapturedContext: false);
		return new MspMovieRated
		{
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			HasRated = (req.Success && Convert.ToInt32(JObject.Parse(req.Result.ToString())["rateMovieId"]) != -1),
			Fame = ((!req.Success) ? 0 : Convert.ToUInt64(JObject.Parse(req.Result.ToString())["awardedFame"])),
			StarCoins = ((!req.Success) ? 0 : Convert.ToUInt64(JObject.Parse(req.Result.ToString())["awardedStarCoins"])),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<MspMovieWatched> WatchMovieAsync(int id, [CallerMemberName] string mth = "")
	{
		RestResponse<object> req = await Client.Api.SendAsync<object>(mth, new object[2]
		{
			id,
			Client.User.Actor.Id
		}).ConfigureAwait(continueOnCapturedContext: false);
		return new MspMovieWatched
		{
			HttpRequest = req.Request,
			HttpResponse = req.Response,
			HttpException = req.Exception,
			Success = req.Success,
			HasWatched = (!req.Success && Convert.ToInt32(JObject.Parse(req.Result.ToString())["returnType"]) == 2),
			Fame = ((!req.Success) ? 0 : Convert.ToUInt64(JObject.Parse(req.Result.ToString())["awardedFame"])),
			HasIpBan = req.HasIpBan
		};
	}

	public async Task<bool> ScanSessionAsync()
	{
		MspOsRun os;
		while (true)
		{
			MspOsRef r = await Client.CreateRefAsync();
			if (r.HasIpBan)
			{
				return false;
			}
			if (r.Success && Constant.MSP_SESSION_HERACHY.ContainsKey(r.Data))
			{
				os = await Client.RunOsAsync(r.RefId, Constant.MSP_SESSION_HERACHY[r.Data]);
				if (os.HasIpBan)
				{
					return false;
				}
				if (os.Success && os.HasSessionId)
				{
					break;
				}
			}
		}
		UpdateSessionId(os.SessionId);
		return true;
	}

	public static async Task<RestResponse<string>> PostAsync(string url, HttpContent content, WebProxy proxy = null)
	{
		int retry = 0;
		while (true)
		{
			retry++;
			try
			{
				new HttpRequestMessage();
				new HttpResponseMessage();
				HttpResponseMessage req;
				if (proxy != null)
				{
					HttpClient http = new HttpClient(new HttpClientHandler
					{
						UseProxy = true,
						Proxy = proxy
					});
					req = await http.PostAsync(url, content);
				}
				else
				{
					req = await Http.PostAsync(url, content);
				}
				req.EnsureSuccessStatusCode();
				RestResponse<string> restResponse = new RestResponse<string>();
				RestResponse<string> restResponse2 = restResponse;
				restResponse2.Result = await req.Content.ReadAsStringAsync();
				restResponse.Success = true;
				return restResponse;
			}
			catch (HttpRequestException ex2)
			{
				HttpRequestException ex = ex2;
				if (ex.StatusCode.HasValue && ex.StatusCode.Value.HasCode(HttpStatusCode.LoopDetected) && retry != 10)
				{
					continue;
				}
				return new RestResponse<string>
				{
					Result = null,
					Success = false,
					Exception = ex
				};
			}
		}
	}

	public static async Task<RestResponse<string>> PostAsync(string url, string content, WebProxy proxy = null)
	{
		int retry = 0;
		while (true)
		{
			retry++;
			try
			{
				new HttpRequestMessage();
				new HttpResponseMessage();
				HttpResponseMessage req;
				if (proxy != null)
				{
					HttpClient http = new HttpClient(new HttpClientHandler
					{
						UseProxy = true,
						Proxy = proxy
					});
					req = await http.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
				}
				else
				{
					req = await Http.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
				}
				req.EnsureSuccessStatusCode();
				RestResponse<string> restResponse = new RestResponse<string>();
				RestResponse<string> restResponse2 = restResponse;
				restResponse2.Result = await req.Content.ReadAsStringAsync();
				restResponse.Success = true;
				return restResponse;
			}
			catch (HttpRequestException ex2)
			{
				HttpRequestException ex = ex2;
				if (ex.StatusCode.HasValue && ex.StatusCode.Value.HasCode(HttpStatusCode.LoopDetected) && retry != 10)
				{
					continue;
				}
				return new RestResponse<string>
				{
					Result = null,
					Success = false,
					Exception = ex
				};
			}
		}
	}

	public static async Task<RestResponse<Bitmap>> GetImageAsync(string url)
	{
		int retry = 0;
		while (true)
		{
			retry++;
			try
			{
				new HttpRequestMessage();
				Stream req = await Http.GetStreamAsync(url);
				return new RestResponse<Bitmap>
				{
					Result = new Bitmap(req),
					Success = true
				};
			}
			catch (HttpRequestException ex2)
			{
				HttpRequestException ex = ex2;
				if (ex.StatusCode.HasValue && ex.StatusCode.Value.HasCode(HttpStatusCode.LoopDetected) && retry != 10)
				{
					continue;
				}
				return new RestResponse<Bitmap>
				{
					Result = null,
					Success = false,
					Exception = ex
				};
			}
		}
	}

	public static async Task<RestResponse<string>> GetStringAsync(string url)
	{
		int retry = 0;
		while (true)
		{
			retry++;
			try
			{
				new HttpRequestMessage();
				string req = await Http.GetStringAsync(url);
				return new RestResponse<string>
				{
					Result = req,
					Success = true
				};
			}
			catch (HttpRequestException ex2)
			{
				HttpRequestException ex = ex2;
				if (ex.StatusCode.HasValue && ex.StatusCode.Value.HasCode(HttpStatusCode.LoopDetected) && retry != 10)
				{
					continue;
				}
				return new RestResponse<string>
				{
					Result = null,
					Success = false,
					Exception = ex
				};
			}
		}
	}

	public async Task<RestResponse<T>> SendAsync<T>([CallerMemberName] string method = "", params object[] data)
	{
		int retry = 0;
		RestResponse<T> request;
		while (true)
		{
			retry++;
			request = await InternalSendAsync<T>(method, ticketNeeded: false, isCustom: false, data);
			if (request.Success || request.HasIpBan || !request.Response.StatusCode.HasCode(HttpStatusCode.LoopDetected) || retry == 3)
			{
				break;
			}
			if (Client.Config.UseDebug)
			{
				Console.WriteLine($"Failed to send request retrying ({retry}) for method - {method}");
			}
		}
		return request;
	}

	public async Task<RestResponse<T>> SendCustomAsync<T>(string method, bool needTicket = false, params object[] data)
	{
		int retry = 0;
		RestResponse<T> request;
		while (true)
		{
			retry++;
			request = await InternalSendAsync<T>(method, needTicket, isCustom: true, data);
			if (request.Success || request.HasIpBan || !request.Response.StatusCode.HasCode(HttpStatusCode.LoopDetected) || retry == 3)
			{
				break;
			}
			if (Client.Config.UseDebug)
			{
				Console.WriteLine($"Failed to send request retrying ({retry}) for method - {method}");
			}
		}
		return request;
	}

	private async Task<RestResponse<T>> InternalSendAsync<T>(string method = "", bool ticketNeeded = false, bool isCustom = false, params object[] data)
	{
		if (Endpoint == null)
		{
			Endpoint = await GetEndpointAsync();
		}
		if (!Endpoint.Success)
		{
			return new RestResponse<T>
			{
				Exception = Endpoint.Exception,
				Request = Endpoint.Request,
				Json = Endpoint.Json,
				Response = Endpoint.Response,
				HasIpBan = Endpoint.HasIpBan
			};
		}
		(string method, bool) atr = (isCustom ? (method, ticketNeeded) : method.GetAttribute());
		if (atr.Item2)
		{
			data = data.Prepend(GetTicket()).ToArray();
		}
		List<AMFHeader> header = new List<AMFHeader>
		{
			new AMFHeader("sessionID", mustUnderstand: false, HashId),
			new AMFHeader("id", mustUnderstand: false, Hash.HashContent(data, atr.Item2)),
			new AMFHeader("needClassName", mustUnderstand: false, true)
		};
		HttpContent httpContent = new ByteArrayContent(new AMFSerializer(AMFBuilder.Encode<T>(header, Endpoint.Result.Build(atr.method), data)).GetData());
		httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-amf");
		try
		{
			HttpResponseMessage httpResult = await ((Client.Config.Proxy != null) ? new HttpClient(new HttpClientHandler
			{
				UseProxy = true,
				Proxy = Client.Config.Proxy
			}) : Http).SendAsync(new HttpRequestMessage(HttpMethod.Post, Endpoint.Result.Build(atr.method))
			{
				Content = httpContent,
				Headers = 
				{
					{ "Referer", "app:/cache/t1.bin/[[DYNAMIC]]/1" },
					{ "Accept", "text/xml, application/xml, application/xhtml+xml, text/html;q=0.9, text/plain;q=0.8, text/css, image/png, image/jpeg, image/gif;q=0.8, application/x-shockwave-flash, video/mp4;q=0.9, flv-application/octet-stream;q=0.8, video/x-flv;q=0.7, audio/mp4, application/futuresplash, */*;q=0.5, application/x-mpegURL" },
					{ "x-flash-version", "32,0,0,100" },
					{ "Accept-Encoding", "gzip,deflate" },
					{ "User-Agent", "Mozilla/5.0 (Windows; U; en) AppleWebKit/533.19.4 (KHTML, like Gecko) AdobeAIR/32.0" }
				}
			}).ConfigureAwait(continueOnCapturedContext: false);
			if (Client.Config.UseDebug && !httpResult.IsSuccessStatusCode)
			{
				Console.WriteLine($"request failed with status code: {httpResult.StatusCode} on method {method}");
			}
			if (!httpResult.IsSuccessStatusCode)
			{
				return new RestResponse<T>
				{
					Response = httpResult,
					Request = httpResult.RequestMessage,
					HasIpBan = (httpResult.StatusCode == HttpStatusCode.Forbidden)
				};
			}
			object content = AMFBuilder.Decode(await httpResult.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false));
			string json = ((content == null) ? "" : JsonConvert.SerializeObject(content, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			}));
			return new RestResponse<T>
			{
				Result = (T)((content == null) ? ((object)default(T)) : ((object)JsonConvert.DeserializeObject<T>(json))),
				Response = httpResult,
				Request = httpResult.RequestMessage,
				Exception = null,
				Success = true,
				Json = json,
				HasIpBan = (httpResult.StatusCode == HttpStatusCode.Forbidden)
			};
		}
		catch (HttpRequestException e)
		{
			return new RestResponse<T>
			{
				Exception = e,
				Response = new HttpResponseMessage(e.StatusCode.GetValueOrDefault()),
				Success = false
			};
		}
	}

	private MspTicket GetTicket()
	{
		if (!Client.User.LoggedIn && string.IsNullOrEmpty(Client.User?.Actor?.Ticket?.Ticket))
		{
			throw new InvalidDataException("You are not logged in");
		}
		return new MspTicket(Client.User.Actor.Ticket.Ticket + Ticket.GetTicket().Ticket);
	}

	public string GetMovieStarAvatarUrl(int actorid)
	{
		return Constant.MOVIESTAR_AVATAR.Replace("{SERVER}", GetServer(Client.Config.Server).GetCode()).Replace("{ID}", actorid.ToString());
	}

	public string GetMovieStarBodyUrl(int actorid)
	{
		return Constant.MOVIESTAR_BODY.Replace("{SERVER}", GetServer(Client.Config.Server).GetCode()).Replace("{ID}", actorid.ToString());
	}

	public static string GetCategoryUrl(int id)
	{
		switch (id)
		{
		case 1:
			return Constant.SWF_SERVICE + "hair/";
		default:
			if (id != 50)
			{
				if (id == 3 || id == 8 || id == 9 || id == 60 || id == 61 || id == 86)
				{
					return Constant.SWF_SERVICE + "bottoms/";
				}
				if (id == 10 || id == 11 || id == 12 || id == 70 || id == 71 || id == 84 || id == 85)
				{
					return Constant.SWF_SERVICE + "footwear/";
				}
				if (id == 13 || id == 14 || id == 15)
				{
					return Constant.SWF_SERVICE + "headwear/";
				}
				if (id == 19 || id == 20 || id == 21 || id == 22 || id == 23 || id == 24 || id == 33 || id == 46)
				{
					return Constant.SWF_SERVICE + "stuff/";
				}
				return Constant.SWF_SERVICE + "accessories/";
			}
			goto case 2;
		case 2:
		case 6:
		case 7:
			return Constant.SWF_SERVICE + "tops/";
		}
	}

	public static CategoryType GetCatoryType(int id)
	{
		string text = GetCategoryUrl(id).Split('/')[4];
		if (1 == 0)
		{
		}
		CategoryType result = text switch
		{
			"hair" => CategoryType.Hair, 
			"tops" => CategoryType.Top, 
			"bottoms" => CategoryType.Bottom, 
			"footwear" => CategoryType.Foot, 
			"headwear" => CategoryType.Head, 
			"stuff" => CategoryType.Stuff, 
			"accessories" => CategoryType.Accessor, 
			_ => CategoryType.Accessor, 
		};
		if (1 == 0)
		{
		}
		return result;
	}

	public static string GetCategoryName(int id)
	{
		string categoryUrl = GetCategoryUrl(id);
		int length = categoryUrl.Length;
		int num = length - 32;
		string text = categoryUrl.Substring(num, length - num).TrimEnd('/');
		string text2 = text.First().ToString().ToUpper();
		string text3 = text;
		return text2 + text3.Substring(1, text3.Length - 1);
	}

	public static int GetMaxFriends(int type, int level)
	{
		type = ((type == 0) ? 1 : type);
		if (1 == 0)
		{
		}
		int result = type switch
		{
			-1 => Calculate(16 * level), 
			0 => Calculate(16 * level * 5), 
			1 => Calculate(16 * level * 6), 
			2 => Calculate(16 * level * 7), 
			3 => Calculate(16 * level * 9), 
			_ => 0, 
		};
		if (1 == 0)
		{
		}
		return result;
	}

	public static bool IsServerValid(string server)
	{
		return GetServer(server) != null;
	}

	public static Server[] GetServerArray()
	{
		return Enum.GetValues<Server>();
	}

	public static int Calculate(int level)
	{
		return (level >= 3500) ? 3500 : (level - level % 10);
	}

	public string GetObfuscatedUrl(ObfuscationType type, int id, string guid = "")
	{
		string endpoint = Constant.MOVIESTAR_OBFUSCATION.Replace("{SERVER}", GetServer(Client.Config.Server).GetCode()).Replace("{TYPE}", Enum.GetName(type.GetType(), (int)type));
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 4);
		defaultInterpolatedStringHandler.AppendFormatted(id / 1000000000);
		defaultInterpolatedStringHandler.AppendLiteral("_");
		defaultInterpolatedStringHandler.AppendFormatted(id / 1000000 % 1000);
		defaultInterpolatedStringHandler.AppendLiteral("_");
		defaultInterpolatedStringHandler.AppendFormatted(id / 1000 % 1000);
		defaultInterpolatedStringHandler.AppendLiteral("_");
		defaultInterpolatedStringHandler.AppendFormatted(id % 1000);
		defaultInterpolatedStringHandler.AppendLiteral("_");
		string text = endpoint.Add(defaultInterpolatedStringHandler.ToStringAndClear());
		string result;
		if (type != 0)
		{
			string text2 = text;
			result = text2.Substring(0, text2.Length - 1) + ".jpg";
		}
		else
		{
			result = text + guid + "-s.jpg";
		}
		return result;
	}

	public async Task<RestResponse<string>> GetEndpointAsync(Server? server = null)
	{
		RestResponse<string> endPoint = await GetStringAsync(Constant.DISCO_SERVICE.Replace("{SERVER}", GetServer(server ?? Client.Config.Server).GetCode())).ConfigureAwait(continueOnCapturedContext: false);
		if (!endPoint.Success)
		{
			return endPoint;
		}
		return new RestResponse<string>
		{
			Result = JObject.Parse(endPoint.Result)["Services"]![0]!["Endpoint"]!.ToString(),
			Exception = endPoint.Exception,
			Request = endPoint.Request,
			Success = true
		};
	}

	public static MspServer GetServer(Server server)
	{
		return Constant.MSP_SERVER_HERACHY.GetValueOrDefault(server);
	}

	public static MspServer GetServer(string server)
	{
		return Constant.MSP_SERVER_HERACHY.SingleOrDefault((KeyValuePair<Server, MspServer> a) => a.Value.Name == server || a.Value.Code.Contains(server.ToLower()))!.Value;
	}
}
