using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET;
using Dolo.PlanetAI.NET.Fluorine;
using Dolo.PlanetAI.Websocket.Entities;
using Dolo.PlanetAI.Websocket.NET;
using Newtonsoft.Json.Linq;

namespace Dolo.PlanetAI.Websocket;

public class MspChat
{
	internal static Random Random;

	internal readonly MspLogin User;

	internal readonly MspSocket Socket;

	internal RoomType Room;

	private bool IsConnected;

	private event MspEvent<MspChat, string> OnPingReceived;

	private event MspEvent<MspChat, string> OnConnected;

	private event MspEvent<MspChat, string> OnPlayerMessageReceived;

	private event MspEvent<MspChat, string> OnPlayerMovementReceived;

	private event MspEvent<MspChat, string> OnPlayerAnimationReceived;

	public MspChat(MspLogin user)
	{
		Random = new Random();
		Socket = new MspSocket(this);
		Socket.OnDataFailed += Socket_OnDataFailed;
		Socket.OnDataReceived += Socket_OnDataReceived;
		Socket.OnDataSent += Socket_OnDataSent;
		User = user;
	}

	public async Task ConnectAsync(string ip)
	{
		await Socket.ConnectAsync(new Uri(Constant.WEBSOCKET_SERVER.Replace("{IP}", ip ?? "").Replace("{IPSPLIT}", ip.Replace(".", "-") ?? "")));
	}

	public async Task DisconnectAsync()
	{
		await Socket.CloseAsync();
	}

	public async Task ConnectAsync(bool isChatRoom = false, bool isModded = false, RoomType? type = null)
	{
		Room = (RoomType)(((int?)type) ?? new Random().Next(System.Enum.GetNames(typeof(RoomType)).Length));
		if (isModded)
		{
			await ConnectAsync(Constant.MSP_MODDED_WEBSERVER_HERACHY[Random.Next(0, Constant.MSP_MODDED_WEBSERVER_HERACHY.Count)].Replace("-", "."));
			return;
		}
		RestResponse<string> restResponse = ((!isChatRoom) ? (await MspApi.GetWebSocketServerAsync()) : (await MspApi.GetWebSocketChatServerAsync(User.Server, Room)));
		RestResponse<string> server = restResponse;
		if (server.Success)
		{
			await ConnectAsync(server.Result.Replace("-", "."));
		}
	}

	private void Socket_OnDataFailed(object sender, string e)
	{
	}

	private void Socket_OnDataSent(object sender, string e)
	{
	}

	private async void Socket_OnDataReceived(object sender, string e)
	{
		if (!IsConnected && e.StartsWith("0"))
		{
			IsConnected = true;
			this.OnConnected?.Invoke(this, e);
			await JoinAsync();
			await Task.Factory.StartNew((Func<Task>)async delegate
			{
				while (Socket != null && IsConnected)
				{
					await PingAsync();
					await Task.Delay(2000);
				}
			});
			return;
		}
		SocketResponse content = MspSocketParser.Parse(e);
		if (content.raw_message == "41")
		{
			await Socket.CloseAsync();
			IsConnected = false;
		}
		else
		{
			switch (content.Type)
			{
			}
		}
	}

	public async Task AuthenticateAsync()
	{
		await Socket.SendAsync("42[\"10\",{\"messageType\":10,\"messageContent\":{\"country\":\"" + User.Server.ToServerCode(isUpperCase: true) + "\",\"version\":1,\"access_token\":\"" + User.Actor.Nebula.AccessToken + "\",\"username\":\"" + JObject.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(User.Actor.Nebula.AccessToken.Split('.')[1])))["profileId"]?.ToString() + "\",\"applicationId\":\"APPLICATION_WEB\"}}]");
	}

	public async Task JoinAsync()
	{
		await Socket.SendAsync("42[\"3\",{\"roomName\":\"\", \"roomType\":\"" + Room.ToString() + "\",\"isPrivateRoom\":false,\"messageType\":3,\"messageContent\":{\"posX\":1100,\"soGUID\":null,\"whitelistedMessage\":null,\"posY\":420,\"actorId\":" + User.Actor.Id + ",\"facing\":null, \"animation\":\"Boy Pose\",\"cacheId\":0,\"actorAction\":\"enter\",\"message\":null,\"trickIdx\":-1,\"senderProfileId\":null, \"effect\":null, \"clickItemIdString\":null, \"actorName\":\"" + User.Actor.Username + "\", \"bonsterAnimation\":null,\"petId\":0, \"blacklistedMessage\":null, \"compressedActorData\":null, \"petType\":0, \"client\":0, \"faceExpression\":null, \"locationIndex\":0},\"accessToken\":\"" + User.Actor.Nebula.AccessToken + "\",\"actorId\":" + User.Actor.Id + ",\"country\":\"" + MspClientUtil.GetServer(User.Server)!.GetCode() + "\"}]");
	}

	public async Task SetPositionAsync(int x, int y)
	{
		await Socket.SendAsync("42[\"2\",{\"messageContent\":{\"actorId\":" + User.Actor.Id + ",\"posY\":" + y + ",\"posX\":" + x + ",\"animation\":\"Girl Pose\"},\"messageType\":2}]");
	}

	public async Task SetPositionAsync()
	{
		await SetPositionAsync(Random.Next(10, 1300), Random.Next(300, 1300));
	}

	public async Task DanceAsync(string dance)
	{
		await Socket.SendAsync("42[\"210\",{\"messageContent\":{\"actorId\":" + User.Actor.Id + ",\"animation\":\"" + dance + "\"},\"messageType\":210}]");
	}

	public async Task PingAsync()
	{
		await Socket.SendAsync("42[\"500\",{\"messageType\":500,\"pingId\":1,\"messageContent\":null,\"lastPingDelay\":0}]");
	}

	public async Task SendMessageAsync(string message)
	{
		await Socket.SendAsync("42[\"8\",{\"messageType\":8,\"messageContent\":{\"whitelistedMessage\":\"" + message + "\\u001eSTYLE(fontcolor={" + Constant.COLOR_CODES[new Random().Next(0, Constant.COLOR_CODES.Count)] + "})\\u001e\",\"blacklistedMessage\":\"" + message + "\\u001eSTYLE(fontcolor={" + Constant.COLOR_CODES[new Random().Next(0, Constant.COLOR_CODES.Count)] + "})\\u001e\",\"message\":\"" + message + "\\u001eSTYLE(fontcolor={" + Constant.COLOR_CODES[new Random().Next(0, Constant.COLOR_CODES.Count)] + "})\\u001e\",\"actorId\":" + User.Actor.Id + "}}]");
	}

	public async Task StarCoinAsync()
	{
		await Socket.SendAsync("42[\"21\",{\"messageType\":21,\"messageContent\":{ \"webserviceUrl\":\"" + User.Actor.WebServer + "/\", \"gameType\":" + Random.Next(1, 6) + "}}]");
	}

	public async Task SendAutographA2Sync(int amount, int actorid, string username, string targetprofile)
	{
		await Socket.SendAsync(MspSocketBuilder.BuildCustom(new SocketAutographNone
		{
			messageType = 102,
			messageContent = new SocketAutographNoneObj
			{
				note = new SocketObject
				{
					applicationId = "APPLICATION_WEB",
					iconSubPath = "",
					importance = 1,
					localizedText = "",
					notificationCatetoryId = 4,
					notificationTypeId = "AUTOGRAPH_NON_FRIEND",
					userId = actorid,
					userName = username
				},
				actorId = targetprofile,
				targetUserId = targetprofile
			}
		}, 102));
	}

	public async Task SendAutographASync(int amount, int actorid, string username, string targetprofile)
	{
		await Socket.SendAsync(MspSocketBuilder.BuildCustom(new SocketAutograph
		{
			messageType = 100,
			targetUserId = targetprofile,
			messageContent = new SocketAutographContent
			{
				serializedString = MspSocketConverter.Serialize(new ASObject("NotificationAutographObject")
				{
					{ "fame", amount },
					{ "systemMessageType", 100 },
					{
						"affectedUserIds",
						new List<object>()
					},
					{ "timestamp", "" },
					{ "importance", 1 },
					{ "notificationTypeId", "AUTOGRAPH_NON_FRIEND" },
					{ "notificationCatetoryId", 4 },
					{ "iconSubPath", "" },
					{ "type", "AUTOGRAPH_NON_FRIEND" },
					{ "localizedText", "" },
					{ "userId", actorid },
					{ "actorId", actorid },
					{ "applicationId", "" },
					{ "userName", username },
					{ "eventName", null }
				}),
				type = "AUTOGRAPH_NON_FRIEND",
				rawObj = new SocketAutographRawObj
				{
					eventName = null,
					userName = username,
					importance = 1,
					notificationCatetoryId = 4,
					notificationTypeId = "AUTOGRAPH_NON_FRIEND",
					timestamp = "",
					type = "AUTOGRAPH_NON_FRIEND",
					systemMessageType = 100,
					localizedText = "",
					iconSubPath = "",
					affectedUserIds = new List<object>(),
					fame = amount,
					userId = actorid,
					actorId = actorid,
					applicationId = ""
				},
				notificationObject = new SocketAutographObject
				{
					eventName = null,
					userName = username,
					importance = 1,
					notificationCatetoryId = 4,
					notificationTypeId = "AUTOGRAPH_NON_FRIEND",
					timestamp = "",
					type = "AUTOGRAPH_NON_FRIEND",
					systemMessageType = 100,
					localizedText = "",
					iconSubPath = "",
					affectedUserIds = new List<object>(),
					fame = amount,
					userId = actorid,
					actorId = actorid,
					applicationId = ""
				},
				eventName = null,
				actorId = actorid
			}
		}, 100));
	}

	public async Task SendInvite(string targetprofileid, string username, int actorid)
	{
		MspSocket socket = Socket;
		SocketInvitation obj = new SocketInvitation
		{
			messageType = 102
		};
		SocketContentNonInvite obj2 = new SocketContentNonInvite
		{
			actorId = targetprofileid,
			targetUserId = targetprofileid
		};
		SocketInviteContent socketInviteContent = new SocketInviteContent();
		ASObject aSObject = new ASObject("NotificationChatromObject");
		aSObject.Add("roomId", actorid + "_room");
		aSObject.Add("houseOwnerName", username);
		aSObject.Add("roomIdOnlyId", actorid);
		aSObject.Add("roomSection", 0);
		aSObject.Add("applicationId", "");
		aSObject.Add("eventName", null);
		aSObject.Add("userName", username);
		aSObject.Add("notificationTypeId", "");
		aSObject.Add("notificationCatetoryId", -1);
		aSObject.Add("importance", 1);
		aSObject.Add("type", "INVITETOCHATROOM");
		aSObject.Add("iconSubPath", "img/used/world_48.png");
		aSObject.Add("localizedText", "");
		aSObject.Add("actorId", actorid);
		aSObject.Add("userId", actorid);
		socketInviteContent.serializedString = MspSocketConverter.Serialize(aSObject);
		socketInviteContent.type = "INVITETOCHATROOM";
		SocketInviteObject obj3 = new SocketInviteObject
		{
			houseOwnerName = username
		};
		obj3.roomId = actorid + "_room";
		obj3.roomIdOnlyId = actorid;
		obj3.roomSection = 0;
		obj3.userId = actorid;
		obj3.applicationId = "";
		obj3.eventName = null;
		obj3.notificationCatetoryId = -1;
		obj3.importance = 1;
		obj3.type = "INVITETOCHATROOM";
		obj3.notificationTypeId = "INVITETOCHATROOM";
		obj3.iconSubPath = "";
		obj3.localizedText = "";
		obj3.actorId = actorid;
		obj3.userName = username;
		socketInviteContent.notificationObject = obj3;
		socketInviteContent.actorId = actorid;
		socketInviteContent.eventName = null;
		SocketInviteObject obj4 = new SocketInviteObject
		{
			houseOwnerName = username
		};
		obj4.roomId = actorid + "_room";
		obj4.roomIdOnlyId = actorid;
		obj4.roomSection = 0;
		obj4.userId = actorid;
		obj4.applicationId = "";
		obj4.eventName = null;
		obj4.notificationCatetoryId = -1;
		obj4.importance = 1;
		obj4.type = "INVITETOCHATROOM";
		obj4.notificationTypeId = "INVITETOCHATROOM";
		obj4.iconSubPath = "";
		obj4.localizedText = "";
		obj4.actorId = actorid;
		obj4.userName = username;
		socketInviteContent.rawObj = obj4;
		obj2.note = socketInviteContent;
		obj.messageContent = obj2;
		await socket.SendAsync(MspSocketBuilder.BuildCustom(obj, 102));
	}
}
