using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScorePet
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalScorePetTmp> Pets { get; set; }
}
