using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalThemes
{
	[JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
	public List<InternalThemesTmp> Themes { get; set; }
}
