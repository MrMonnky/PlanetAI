using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreMovieTmp
{
	[JsonProperty("AverageRating", NullValueHandling = NullValueHandling.Ignore)]
	public double AverageRating { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("MovieId", NullValueHandling = NullValueHandling.Ignore)]
	public int MovieId { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("PublishedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime PublishedDate { get; set; }

	[JsonProperty("RatedCount", NullValueHandling = NullValueHandling.Ignore)]
	public int RatedCount { get; set; }

	[JsonProperty("StarCoinsEarned", NullValueHandling = NullValueHandling.Ignore)]
	public int StarCoinsEarned { get; set; }

	[JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
	public int State { get; set; }

	[JsonProperty("WatchedActorCount", NullValueHandling = NullValueHandling.Ignore)]
	public int WatchedActorCount { get; set; }

	[JsonProperty("ActorName", NullValueHandling = NullValueHandling.Ignore)]
	public string ActorName { get; set; }

	[JsonProperty("MembershipPurchasedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipPurchasedDate { get; set; }

	[JsonProperty("MembershipTimeoutDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime MembershipTimeoutDate { get; set; }

	[JsonProperty("Guid", NullValueHandling = NullValueHandling.Ignore)]
	public int Guid { get; set; }

	[JsonProperty("Watched", NullValueHandling = NullValueHandling.Ignore)]
	public bool Watched { get; set; }

	[JsonProperty("Rated", NullValueHandling = NullValueHandling.Ignore)]
	public bool Rated { get; set; }
}
