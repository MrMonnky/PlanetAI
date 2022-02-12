using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dolo.PlanetAI.Entities;

namespace Dolo.PlanetAI;

public sealed class MspActor : MspBaseActor
{
	public DateTime LastLoginAt { get; internal set; }

	public DateTime MembershiptPurchasedAt { get; internal set; }

	public DateTime MembershiptTimeoutAt { get; internal set; }

	public VipTierType VipTier { get; internal set; }

	public Gender Gender { get; internal set; }

	public MspBeauty Beauty { get; internal set; }

	public List<MspClothRel> ClothRel { get; internal set; }

	public string ProfileId { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	public string Username { get; internal set; }

	public int Id { get; internal set; }

	public int Level { get; internal set; }

	public int Friends { get; internal set; }

	public int FriendsVip { get; internal set; }

	public int MaxFriends { get; internal set; }

	public ulong Fame { get; internal set; }

	public ulong FameUntilNextLevel { get; internal set; }

	public ulong StarCoins { get; internal set; }

	public ulong Diamonds { get; internal set; }

	public ulong Fortune { get; internal set; }

	public bool IsModerator { get; internal set; }

	public bool IsVip { get; internal set; }

	public bool IsDeleted { get; internal set; }

	public bool IsJury { get; internal set; }

	public bool IsJudge { get; internal set; }

	public bool IsCeleb { get; internal set; }

	public bool IsAvailable { get; internal set; }

	public string OutfitPrice
	{
		get
		{
			object result;
			if (ClothRel.Count != 0)
			{
				if (ClothRel.Sum((MspClothRel a) => a.Cloth.Price) == 0 || ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice) != 0)
				{
					if (ClothRel.Sum((MspClothRel a) => a.Cloth.Price) != 0 || ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice) == 0)
					{
						if (ClothRel.Sum((MspClothRel a) => a.Cloth.Price) == 0 || ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice) == 0)
						{
							result = "0 StarCoins &' 0 Diamonds";
						}
						else
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
							defaultInterpolatedStringHandler.AppendFormatted(ClothRel.Sum((MspClothRel a) => a.Cloth.Price));
							defaultInterpolatedStringHandler.AppendLiteral(" StarCoins &' ");
							defaultInterpolatedStringHandler.AppendFormatted(ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice));
							defaultInterpolatedStringHandler.AppendLiteral(" Diamonds");
							result = defaultInterpolatedStringHandler.ToStringAndClear();
						}
					}
					else
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
						defaultInterpolatedStringHandler.AppendFormatted(ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice));
						defaultInterpolatedStringHandler.AppendLiteral(" Diamonds");
						result = defaultInterpolatedStringHandler.ToStringAndClear();
					}
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
					defaultInterpolatedStringHandler.AppendFormatted(ClothRel.Sum((MspClothRel a) => a.Cloth.Price));
					defaultInterpolatedStringHandler.AppendLiteral(" StarCoins");
					result = defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			else
			{
				result = "0 StarCoins &' 0 Diamonds";
			}
			return (string)result;
		}
	}

	public int OutfitDiamondPrice => (ClothRel.Count != 0) ? ClothRel.Sum((MspClothRel a) => a.Cloth.DiamondsPrice) : 0;

	public int OutfitStarCoinPrice => (ClothRel.Count != 0) ? ClothRel.Sum((MspClothRel a) => a.Cloth.Price) : 0;

	internal MspActor()
	{
	}

	public async Task<MspResult<DateTime>> GetCreatedAtAsync()
	{
		return await MovieStarPlanet.GetActorCreatedAtAsync(Id);
	}

	public async Task<MspStatus> GetStatusAsync()
	{
		return await MovieStarPlanet.GetActorStatusAsync(Id);
	}

	public async Task<MspAutograph> SendAutographAsync()
	{
		return await MovieStarPlanet.SendAutographAsync(Id);
	}

	public async Task<MspResult<bool>> DeleteAsync()
	{
		return await MovieStarPlanet.DeleteFriendAsync(Id);
	}

	public async Task<MspList<MspBoonster>> GetBoonstersAsync()
	{
		return await MovieStarPlanet.GetActorBoonstersAsync(Id);
	}

	public string GetGenderName()
	{
		return Enum.GetName(Gender);
	}
}
