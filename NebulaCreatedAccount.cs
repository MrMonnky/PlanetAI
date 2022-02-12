using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class NebulaCreatedAccount
{
	[JsonProperty("success")]
	public bool Success { get; set; }

	[JsonProperty("error")]
	public string Error { get; set; }

	[JsonProperty("loginId")]
	public string LoginId { get; set; }

	[JsonProperty("loginName")]
	public string LoginName { get; set; }

	[JsonProperty("profileId")]
	public string ProfileId { get; set; }

	[JsonProperty("profileName")]
	public string Username { get; set; }

	[JsonProperty("isGuest")]
	public bool IsGuest { get; set; }

	public string Password { get; set; }

	public Server Server { get; set; }
}
