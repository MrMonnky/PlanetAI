using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreLook
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalScoreLookTmp> Look { get; set; }
}
