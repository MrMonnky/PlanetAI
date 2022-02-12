using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPicture
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalPictureTmp> Actors { get; set; }
}
