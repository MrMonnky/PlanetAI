using System;
using System.Collections.Generic;

namespace Dolo.PlanetAI;

public class Status
{
	internal StatusColor Color = StatusColor.Black;

	public List<object> WallPostLinks;

	public int ActorId { get; internal set; }

	public string FaceAnimation { get; internal set; }

	public string FigureAnimation { get; internal set; }

	public bool IsBrag => false;

	public int Likes => 0;

	public string MouthAnimation => "none";

	public bool SpeechLine { get; internal set; }

	public string TextLine { get; internal set; }

	public string TextLineBlacklisted => "";

	public DateTime? TextLineLastFiltered => null;

	public string TextLineWhitelisted => "";

	public int WallPostId => 0;

	internal Status()
	{
	}

	internal Status(StatusMessageBuilder Builder)
	{
		Builder.Build(this);
	}

	public int GetColor()
	{
		return (int)Color;
	}
}
