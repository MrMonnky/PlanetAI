namespace Dolo.PlanetAI.Entities;

public class MspServer
{
	public string[] Code { get; internal set; }

	public string Name { get; internal set; }

	public Server Server { get; internal set; }

	internal MspServer(Server server, string name, params string[] code)
	{
		Server = server;
		Name = name;
		Code = code;
	}

	public string GetCode()
	{
		return Code[0];
	}
}
