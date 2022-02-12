using System;
using System.Collections.Generic;

namespace Dolo.PlanetAI;

public sealed class MspStatus : MspBaseHttp
{
	private string tmpText;

	public List<MspSpecialFriend> SpecialFriend { get; internal set; }

	public DateTime LastUpdatedAt { get; internal set; }

	public bool HasStatus { get; internal set; }

	public bool HasLikes { get; internal set; }

	public ulong Id { get; internal set; }

	public int ActorId { get; internal set; }

	public int Likes { get; internal set; }

	public string Text
	{
		get
		{
			return string.IsNullOrEmpty(tmpText) ? "no status." : (tmpText.Contains('\u001e') ? tmpText.Split(new string[1] { "\u001e" }, StringSplitOptions.None)[0] : tmpText);
		}
		set
		{
			tmpText = value;
		}
	}

	public string FigureAnimation { get; internal set; }

	public string FaceAnimation { get; internal set; }

	public string MouthAnimation { get; internal set; }

	public string AvatarUrl { get; internal set; }

	public string BodyUrl { get; internal set; }

	public string RoomUrl { get; internal set; }

	internal MspStatus()
	{
	}

	public void SetStatusText(string text)
	{
		Text = text;
	}
}
