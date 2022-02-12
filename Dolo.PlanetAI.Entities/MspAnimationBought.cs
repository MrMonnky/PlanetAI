namespace Dolo.PlanetAI.Entities;

public class MspAnimationBought : MspBaseHttp
{
	public int Fame { get; internal set; }

	public int Code { get; internal set; }

	public string Description { get; internal set; }

	public bool HasBought => Code == 0;
}
