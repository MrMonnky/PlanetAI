using System.Collections.Generic;

namespace Dolo.PlanetAI.Entities;

public class MspActorCreation
{
	public string ChosenActorName { get; set; }

	public string ChosenPassword { get; set; }

	public List<MspActorCreationCloth> Clothes { get; set; }

	public string EyeColors { get; set; }

	public int EyeId { get; set; }

	public int InvitedByActorId { get; set; }

	public string MouthColors { get; set; }

	public int MouthId { get; set; }

	public int NoseId { get; set; }

	public string SkinColor { get; set; }

	public bool SkinIsMale { get; set; }

	public static MspActorCreation Build(MspLogin login, string username, string password)
	{
		return new MspActorCreation
		{
			EyeColors = login.Actor.Beauty.Eye.Color,
			EyeId = login.Actor.Beauty.Eye.Id,
			MouthColors = login.Actor.Beauty.Mouth.Color,
			MouthId = login.Actor.Beauty.Mouth.Id,
			NoseId = login.Actor.Beauty.Nose.Id,
			SkinColor = login.Actor.Beauty.Skincolor.ToString(),
			SkinIsMale = (login.Actor.Gender == Gender.Male),
			ChosenActorName = username,
			ChosenPassword = password,
			InvitedByActorId = -1,
			Clothes = new List<MspActorCreationCloth>
			{
				new MspActorCreationCloth
				{
					ActorClothesRelId = -5,
					ActorId = -2,
					ClothesId = 25973,
					Color = "0x793A2A,0xC29757,0x38231D",
					IsWearing = 1,
					x = 0,
					y = 0
				},
				new MspActorCreationCloth
				{
					ActorClothesRelId = -6,
					ActorId = -2,
					ClothesId = 17989,
					Color = "0x2C2C2C,0x000000",
					IsWearing = 1,
					x = 0,
					y = 0
				},
				new MspActorCreationCloth
				{
					ActorClothesRelId = -7,
					ActorId = -2,
					ClothesId = 19214,
					Color = "0x99cccc",
					IsWearing = 1,
					x = 0,
					y = 0
				},
				new MspActorCreationCloth
				{
					ActorClothesRelId = -8,
					ActorId = -2,
					ClothesId = 18840,
					Color = "0xB6B6B6,0x53BADB,0xF5E500,0xE5004B,0x2AB054",
					IsWearing = 1,
					x = 0,
					y = 0
				}
			}
		};
	}
}
