using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalClothesCategory
{
	[JsonProperty("ClothesCategoryId", NullValueHandling = NullValueHandling.Ignore)]
	public int ClothesCategoryId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("SlotTypeId", NullValueHandling = NullValueHandling.Ignore)]
	public int SlotTypeId { get; set; }

	[JsonProperty("SlotType", NullValueHandling = NullValueHandling.Ignore)]
	public object SlotType { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
