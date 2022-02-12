using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreMovie
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalScoreMovieTmp> Movies { get; set; }
}
