using System.Collections.Generic;

namespace Dolo.PlanetAI;

public sealed class MspThemeContent : MspBaseHttp
{
	public List<MspTheme> Themes { get; internal set; }

	public MspTheme Latest { get; internal set; }

	public MspTheme Upcoming { get; internal set; }

	public bool HasUpcoming { get; internal set; }

	internal MspThemeContent()
	{
	}
}
