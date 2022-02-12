using System.Threading.Tasks;

namespace Dolo.PlanetAI;

public class MspPet : MspBase
{
	public int Id { get; set; }

	public async Task<MspResult<int>> LoveAsync()
	{
		return await MovieStarPlanet.LovePetAsync(Id);
	}
}
