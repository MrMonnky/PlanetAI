using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalStatusUpdate
{
	[JsonProperty("FilterTextResult", NullValueHandling = NullValueHandling.Ignore)]
	public FilterTextResult FilterTextResult { get; set; }
}
