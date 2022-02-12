using System;
using System.Threading.Tasks;

namespace Dolo.PlanetAI.Entities;

public class MspBoonster : MspBase
{
	public int Id { get; internal set; }

	public int ActorId { get; internal set; }

	public int ItemId { get; internal set; }

	public int FoodPoints { get; internal set; }

	public int Stage { get; internal set; }

	public string Name { get; internal set; }

	public DateTime LastFeedTime { get; internal set; }

	public int X { get; internal set; }

	public int Y { get; internal set; }

	public DateTime LastWashTime { get; internal set; }

	public int PlayPoints { get; internal set; }

	public DateTime AtHotelUntil { get; internal set; }

	internal MspBoonster()
	{
	}

	public async Task<MspResult<int>> LoveAsync()
	{
		return await MovieStarPlanet.LovePetAsync(Id);
	}
}
