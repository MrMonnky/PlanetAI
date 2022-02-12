using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public sealed class MspAppSettings
{
	[JsonProperty("name")]
	public string Name { get; internal set; }

	[JsonProperty("value")]
	public string Value { get; internal set; }

	internal MspAppSettings()
	{
	}
}
