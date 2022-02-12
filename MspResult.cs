namespace Dolo.PlanetAI;

public class MspResult<T> : MspBaseHttp
{
	public T Value { get; internal set; }
}
