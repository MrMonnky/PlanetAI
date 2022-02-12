using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreActor
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalScoreActorTmp> Actors { get; set; }
}
