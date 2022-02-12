using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class ClothesCategory
{
	[JsonProperty("ClothesCategoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesCategoryId { get; internal set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; internal set; }

	[JsonProperty("SlotTypeId", NullValueHandling = NullValueHandling.Ignore)]
	public int SlotTypeId { get; internal set; }

	[JsonProperty("SlotType", NullValueHandling = NullValueHandling.Ignore)]
	public object SlotType { get; internal set; }

	internal ClothesCategory()
	{
	}
}
