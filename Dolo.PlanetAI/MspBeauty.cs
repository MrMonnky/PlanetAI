namespace Dolo.PlanetAI;

public sealed class MspBeauty : MspBase
{
	public MspEye Eye { get; internal set; }

	public MspNose Nose { get; internal set; }

	public MspMouth Mouth { get; internal set; }

	public MspEyeShadow EyeShadow { get; internal set; }

	public object Skincolor { get; internal set; }

	internal MspBeauty()
	{
	}
}
