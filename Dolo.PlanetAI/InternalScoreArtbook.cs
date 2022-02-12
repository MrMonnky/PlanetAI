using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreArtbook
{
	[JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalScoreArtbookTmp> Artbook { get; set; }
}
