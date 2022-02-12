using System.Net.Http;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalLogin
{
	public bool LoggedIn { get; set; }

	public HttpRequestException Exception { get; set; }

	public string Response { get; set; }

	[JsonProperty("loginStatus", NullValueHandling = NullValueHandling.Ignore)]
	public InternalLoginStatus LoginStatus { get; set; }

	[JsonProperty("hDetails", NullValueHandling = NullValueHandling.Ignore)]
	public string HDetails { get; set; }

	[JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
	public string Hash { get; set; }
}
