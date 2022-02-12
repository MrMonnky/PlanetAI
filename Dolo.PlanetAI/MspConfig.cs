using System.Net;

namespace Dolo.PlanetAI;

public class MspConfig
{
	public string Username { get; set; }

	public string Password { get; set; }

	public bool UseSocket { get; set; }

	public bool UseOriginalBehaviour { get; set; }

	public Server Server { get; set; }

	public WebProxy Proxy { get; set; }

	public bool UseDebug { get; set; }
}
