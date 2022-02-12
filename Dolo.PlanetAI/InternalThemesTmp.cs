using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalThemesTmp
{
	[JsonProperty("ThemeID", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("SnapshotPath", NullValueHandling = NullValueHandling.Ignore)]
	public string SnapshotPath { get; set; }
}
