using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalEye
{
	[JsonProperty("EyeId", NullValueHandling = NullValueHandling.Ignore)]
	public int EyeId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("SWF", NullValueHandling = NullValueHandling.Ignore)]
	public string Swf { get; set; }

	[JsonProperty("SkinId", NullValueHandling = NullValueHandling.Ignore)]
	public int SkinId { get; set; }

	[JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
	public int Price { get; set; }

	[JsonProperty("Vip", NullValueHandling = NullValueHandling.Ignore)]
	public int Vip { get; set; }

	[JsonProperty("RegNewUser", NullValueHandling = NullValueHandling.Ignore)]
	public int RegNewUser { get; set; }

	[JsonProperty("sortorder", NullValueHandling = NullValueHandling.Ignore)]
	public object Sortorder { get; set; }

	[JsonProperty("isNew", NullValueHandling = NullValueHandling.Ignore)]
	public int IsNew { get; set; }

	[JsonProperty("Discount", NullValueHandling = NullValueHandling.Ignore)]
	public int Discount { get; set; }

	[JsonProperty("ThemeID", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; set; }

	[JsonProperty("DiamondsPrice", NullValueHandling = NullValueHandling.Ignore)]
	public int DiamondsPrice { get; set; }

	[JsonProperty("DragonBone", NullValueHandling = NullValueHandling.Ignore)]
	public bool DragonBone { get; set; }

	[JsonProperty("DefaultColors", NullValueHandling = NullValueHandling.Ignore)]
	public string DefaultColors { get; set; }

	[JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
	public bool Hidden { get; set; }

	[JsonProperty("lastNewTagDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastNewTagDate { get; set; }

	[JsonProperty("LastUpdated", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastUpdated { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }

	[JsonProperty("MouthId", NullValueHandling = NullValueHandling.Ignore)]
	public int MouthId { get; set; }

	[JsonProperty("NoseId", NullValueHandling = NullValueHandling.Ignore)]
	public int NoseId { get; set; }
}
