using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalNebulaLoginStatus
{
	[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
	public string Status { get; set; }

	[JsonProperty("statusDetails", NullValueHandling = NullValueHandling.Ignore)]
	public object StatusDetails { get; set; }

	[JsonProperty("accessToken", NullValueHandling = NullValueHandling.Ignore)]
	public string AccessToken { get; set; }

	[JsonProperty("refreshToken", NullValueHandling = NullValueHandling.Ignore)]
	public string RefreshToken { get; set; }

	[JsonProperty("expiresIn", NullValueHandling = NullValueHandling.Ignore)]
	public long ExpiresIn { get; set; }

	[JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
	public object Error { get; set; }

	[JsonProperty("errorDescription", NullValueHandling = NullValueHandling.Ignore)]
	public object ErrorDescription { get; set; }

	[JsonProperty("profileId", NullValueHandling = NullValueHandling.Ignore)]
	public string ProfileId { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
