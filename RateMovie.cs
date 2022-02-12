using System;

namespace Dolo.PlanetAI;

internal sealed class RateMovie
{
	public int ActorId { get; internal set; }

	public string Comment { get; set; }

	public int MovieId { get; set; }

	public DateTime RateDate => DateTime.Now;

	public int RateMovieId => 0;

	public int Score { get; internal set; }

	public RateMovie(RateMovieStar Stars)
	{
		Score = (int)Stars;
	}
}
