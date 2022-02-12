using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalMspUser
{
	public static InternalMspUser FromJson(string json)
	{
		return JsonConvert.DeserializeObject<InternalMspUser>(json, Converter.Settings);
	}
}
