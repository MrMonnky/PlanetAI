using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalBoonsters
{
	[JsonProperty("ActorClickItemRelId", NullValueHandling = NullValueHandling.Ignore)]
	public int Id { get; internal set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; internal set; }

	[JsonProperty("ClickItemId", NullValueHandling = NullValueHandling.Ignore)]
	public int ItemId { get; internal set; }

	[JsonProperty("FoodPoints", NullValueHandling = NullValueHandling.Ignore)]
	public int FoodPoints { get; internal set; }

	[JsonProperty("Stage", NullValueHandling = NullValueHandling.Ignore)]
	public int Stage { get; internal set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; internal set; }

	[JsonProperty("LastFeedTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastFeedTime { get; internal set; }

	[JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
	public int X { get; internal set; }

	[JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
	public int Y { get; internal set; }

	[JsonProperty("LastWashTime", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime LastWashTime { get; internal set; }

	[JsonProperty("PlayPoints", NullValueHandling = NullValueHandling.Ignore)]
	public int PlayPoints { get; internal set; }

	[JsonProperty("AtHotelUntil", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime AtHotelUntil { get; internal set; }

	internal InternalBoonsters()
	{
	}
}
