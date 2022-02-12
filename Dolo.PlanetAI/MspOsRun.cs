namespace Dolo.PlanetAI;

public sealed class MspOsRun : MspBaseHttp
{
	public string SessionId { get; internal set; }

	public bool HasSessionId => !string.IsNullOrEmpty(SessionId);

	internal MspOsRun()
	{
	}
}
