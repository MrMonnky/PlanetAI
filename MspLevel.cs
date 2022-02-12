namespace Dolo.PlanetAI;

public sealed class MspLevel
{
	public int Level { get; internal set; }

	public ulong Fame { get; internal set; }

	internal MspLevel(int level, ulong fame)
	{
		Level = level;
		Fame = fame;
	}
}
