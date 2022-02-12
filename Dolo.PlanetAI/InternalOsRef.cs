using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalOsRef
{
	[JsonProperty("RefId")]
	public string RefId { get; set; }

	[JsonProperty("TjData")]
	public string TjData { get; set; }

	[JsonProperty("DateCreated")]
	public DateTime Created { get; set; }
}
