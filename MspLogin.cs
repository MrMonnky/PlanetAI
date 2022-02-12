using System;

namespace Dolo.PlanetAI;

public sealed class MspLogin : MspBaseHttp
{
	public LoginStatusCode StatusCode { get; internal set; }

	public string Status { get; internal set; }

	public bool LoggedIn { get; internal set; }

	public MspUser Actor { get; internal set; }

	public Server Server { get; internal set; }

	internal MspLogin(bool isLoggedIn = false)
	{
		LoggedIn = isLoggedIn;
	}

	public string GetGenderName()
	{
		return Enum.GetName(Actor.Gender);
	}
}
