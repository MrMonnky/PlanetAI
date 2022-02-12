using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalScoreArtbookTmp
{
	[JsonProperty("ScrapBlogId", NullValueHandling = NullValueHandling.Ignore)]
	public int ScrapBlogId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Created", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime CreatedAt { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("Likes", NullValueHandling = NullValueHandling.Ignore)]
	public int Likes { get; set; }

	[JsonProperty("Deleted", NullValueHandling = NullValueHandling.Ignore)]
	public int Deleted { get; set; }

	[JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
	public int Status { get; set; }

	[JsonProperty("FameEarned", NullValueHandling = NullValueHandling.Ignore)]
	public ulong FameEarned { get; set; }

	[JsonProperty("ScrapBlogType", NullValueHandling = NullValueHandling.Ignore)]
	public int ScrapBlogType { get; set; }

	[JsonProperty("TemplateType", NullValueHandling = NullValueHandling.Ignore)]
	public int TemplateType { get; set; }

	[JsonProperty("PublishedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime PublishedAt { get; set; }

	[JsonProperty("CommentsCount", NullValueHandling = NullValueHandling.Ignore)]
	public int CommentsCount { get; set; }

	[JsonProperty("ScrapBlogActorName", NullValueHandling = NullValueHandling.Ignore)]
	public InternalScoreArtbookUserTmp ScrapActor { get; set; }
}
