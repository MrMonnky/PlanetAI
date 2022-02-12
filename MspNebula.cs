namespace Dolo.PlanetAI;

public sealed class MspNebula
{
	public string AccessToken { get; internal set; }

	public string RefreshToken { get; internal set; }

	internal MspNebula()
	{
	}

	internal MspNebula(string atoken, string rtoken)
	{
		AccessToken = atoken;
		RefreshToken = rtoken;
	}
}
