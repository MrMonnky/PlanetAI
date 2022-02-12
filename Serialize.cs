using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal static class Serialize
{
	public static string ToJson(this InternalMspUser self)
	{
		return JsonConvert.SerializeObject(self, Converter.Settings);
	}
}
