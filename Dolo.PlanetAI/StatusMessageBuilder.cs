using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dolo.PlanetAI;

public sealed class StatusMessageBuilder : MspBase
{
	private readonly StringBuilder Builder;

	public StatusColor Color { get; set; }

	public FaceAnimationType Face { get; set; }

	public FigureAnimationType Figure { get; set; }

	public StatusMessageBuilder()
	{
		Builder = new StringBuilder();
		Color = StatusColor.Black;
		Face = FaceAnimationType.neutral;
	}

	public StatusMessageBuilder WithText(string text)
	{
		Builder.Append((Builder.Length > 0) ? (" " + text) : text);
		return this;
	}

	public StatusMessageBuilder WithEmote(StatusEmote emote)
	{
		Builder.Append((Builder.Length > 0) ? (" " + emote.Emote) : emote.Emote);
		return this;
	}

	public StatusMessageBuilder WithEmote(params StatusEmote[] emote)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (StatusEmote statusEmote in emote)
		{
			stringBuilder.Append(statusEmote.Emote);
		}
		StringBuilder builder = Builder;
		object value;
		if (Builder.Length <= 0)
		{
			value = stringBuilder;
		}
		else
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(stringBuilder);
			value = defaultInterpolatedStringHandler.ToStringAndClear();
		}
		builder.Append(value);
		return this;
	}

	public void AddColor(StatusColor color)
	{
		Color = color;
	}

	public void AddFaceAnimtion(FaceAnimationType type)
	{
		Face = type;
	}

	public void AddFigureAnimation(FigureAnimationType type)
	{
		Figure = type;
	}

	internal void Build(Status status)
	{
		Figure = ((MovieStarPlanet.User.Actor.Gender == Gender.Female) ? FigureAnimationType.Girl : FigureAnimationType.Boy);
		status.ActorId = MovieStarPlanet.User.Actor.Id;
		status.Color = Color;
		status.TextLine = Builder.ToString();
		status.FaceAnimation = Enum.GetName(Face);
		status.FigureAnimation = ((Figure == FigureAnimationType.Boy) ? "Boy Pose" : "Girl Pose");
	}
}
