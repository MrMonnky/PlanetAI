using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class FilterTextResult
{
	[JsonProperty("OriginalText", NullValueHandling = NullValueHandling.Ignore)]
	public string OriginalText { get; set; }

	[JsonProperty("TimeStamp", NullValueHandling = NullValueHandling.Ignore)]
	public double TimeStamp { get; set; }

	[JsonProperty("IsMessageOk", NullValueHandling = NullValueHandling.Ignore)]
	public bool IsMessageOk { get; set; }

	[JsonProperty("Policy", NullValueHandling = NullValueHandling.Ignore)]
	public long? Policy { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
