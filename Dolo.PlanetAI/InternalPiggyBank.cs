using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPiggyBank
{
	[JsonProperty("StarCoins", NullValueHandling = NullValueHandling.Ignore)]
	public ulong StarCoins { get; set; }

	[JsonProperty("Fame", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Fame { get; set; }

	[JsonProperty("Diamonds", NullValueHandling = NullValueHandling.Ignore)]
	public ulong Diamonds { get; set; }

	[JsonProperty("PiggyBankState", NullValueHandling = NullValueHandling.Ignore)]
	public long PiggyBankState { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
