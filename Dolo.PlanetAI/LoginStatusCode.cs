namespace Dolo.PlanetAI;

public enum LoginStatusCode
{
	Success = 0,
	InvalidCredentials = 1,
	Banned = 3,
	IpBanned = 4,
	Deleted = 5,
	Forced = 6,
	Whitelist = 7,
	Maintenace = 8,
	Connection = 9,
	Logout = 10
}
