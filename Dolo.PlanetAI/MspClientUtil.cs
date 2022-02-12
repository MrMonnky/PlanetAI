using System;
using System.Collections.Generic;
using Dolo.PlanetAI.Entities;
using Dolo.PlanetAI.NET;

namespace Dolo.PlanetAI;

public static class MspClientUtil
{
	public static string[] GetServerList()
	{
		return Array.ConvertAll(MspApi.GetServerArray(), (Server a) => a.ToString());
	}

	public static Server? ToServer(this string server)
	{
		return MspApi.GetServer(server)?.Server;
	}

	public static string ToServerCode(this Server server, bool isUpperCase = false)
	{
		return (!isUpperCase) ? MspApi.GetServer(server).GetCode() : MspApi.GetServer(server)?.GetCode().ToUpperInvariant();
	}

	public static MspServer? GetServer(Server server)
	{
		return MspApi.GetServer(server);
	}

	public static MspServer? GetServer(string server)
	{
		return MspApi.GetServer(server);
	}

	public static List<MspLevel> GetLevel()
	{
		return Constant.GetLevel();
	}

	public static string GetServerFlag(Server server)
	{
		if (1 == 0)
		{
		}
		string result = server switch
		{
			Server.Germany => "https://cdn.discordapp.com/emojis/752634877945315369.webp?size=96&quality=lossless", 
			Server.Australia => "https://cdn.discordapp.com/emojis/752634877945446420.webp?size=96&quality=lossless", 
			Server.Canada => "https://cdn.discordapp.com/emojis/752634877811359785.webp?size=96&quality=lossless", 
			Server.Denmark => "https://cdn.discordapp.com/emojis/752634877983326279.webp?size=96&quality=lossless", 
			Server.Finland => "https://cdn.discordapp.com/emojis/752634878117543956.webp?size=96&quality=lossless", 
			Server.France => "https://cdn.discordapp.com/emojis/752634877806903337.webp?size=96&quality=lossless", 
			Server.Ireland => "https://cdn.discordapp.com/emojis/752634878180458564.webp?size=96&quality=lossless", 
			Server.Netherlands => "https://cdn.discordapp.com/emojis/752634878276927605.webp?size=96&quality=lossless", 
			Server.NewZealand => "https://cdn.discordapp.com/emojis/752634878037721089.webp?size=96&quality=lossless", 
			Server.Norway => "https://cdn.discordapp.com/emojis/752634878520197130.webp?size=96&quality=lossless", 
			Server.Poland => "https://cdn.discordapp.com/emojis/752634878226464789.webp?size=96&quality=lossless", 
			Server.Spain => "https://cdn.discordapp.com/emojis/752634878679449750.webp?size=96&quality=lossless", 
			Server.Sweden => "https://cdn.discordapp.com/emojis/752634878687969350.webp?size=96&quality=lossless", 
			Server.Turkey => "https://cdn.discordapp.com/emojis/752634878880776232.webp?size=96&quality=lossless", 
			Server.UnitedStates => "https://cdn.discordapp.com/emojis/752634878843158630.webp?size=96&quality=lossless", 
			Server.UnitedKingdom => "https://cdn.discordapp.com/emojis/752634878666735778.webp?size=96&quality=lossless", 
			_ => "https://cdn.discordapp.com/emojis/752634877945315369.webp?size=96&quality=lossless", 
		};
		if (1 == 0)
		{
		}
		return result;
	}
}
