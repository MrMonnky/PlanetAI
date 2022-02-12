using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalAdCountryMap
{
	[JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore)]
	public long Id { get; set; }

	[JsonProperty("Vendor", NullValueHandling = NullValueHandling.Ignore)]
	public long Vendor { get; set; }

	[JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
	public long Type { get; set; }

	[JsonProperty("UseVIP", NullValueHandling = NullValueHandling.Ignore)]
	public bool UseVip { get; set; }

	[JsonProperty("AppKey", NullValueHandling = NullValueHandling.Ignore)]
	public string AppKey { get; set; }

	[JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
	public Uri Url { get; set; }

	[JsonProperty("Active", NullValueHandling = NullValueHandling.Ignore)]
	public bool Active { get; set; }

	[JsonProperty("MaximumNumberOfDailyImpressions", NullValueHandling = NullValueHandling.Ignore)]
	public long MaximumNumberOfDailyImpressions { get; set; }

	[JsonProperty("StarCoins", NullValueHandling = NullValueHandling.Ignore)]
	public long StarCoins { get; set; }

	[JsonProperty("Fame", NullValueHandling = NullValueHandling.Ignore)]
	public long Fame { get; set; }

	[JsonProperty("EverythingIsAwesome", NullValueHandling = NullValueHandling.Ignore)]
	public bool EverythingIsAwesome { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
