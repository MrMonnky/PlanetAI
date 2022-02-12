using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class InternalActorClothPage
{
	[JsonProperty("totalRecords", NullValueHandling = NullValueHandling.Ignore)]
	public int Count { get; internal set; }

	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public InternalActorClothPageTmp[] Pages { get; internal set; }

	internal InternalActorClothPage()
	{
	}
}
