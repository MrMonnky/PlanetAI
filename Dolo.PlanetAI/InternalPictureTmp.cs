using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalPictureTmp
{
	[JsonProperty("ImageUploadId", NullValueHandling = NullValueHandling.Ignore)]
	public int ImageUploadId { get; set; }

	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("Uploaded", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime Uploaded { get; set; }

	[JsonProperty("Headline", NullValueHandling = NullValueHandling.Ignore)]
	public string Headline { get; set; }

	[JsonProperty("Guid", NullValueHandling = NullValueHandling.Ignore)]
	public string Guid { get; set; }

	[JsonProperty("Likes", NullValueHandling = NullValueHandling.Ignore)]
	public int Likes { get; set; }

	[JsonProperty("Views", NullValueHandling = NullValueHandling.Ignore)]
	public int Views { get; set; }

	[JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
	public int Status { get; set; }

	[JsonProperty("CommentsCount", NullValueHandling = NullValueHandling.Ignore)]
	public int CommentsCount { get; set; }

	[JsonProperty("Published", NullValueHandling = NullValueHandling.Ignore)]
	public bool Published { get; set; }

	[JsonProperty("ActorImage", NullValueHandling = NullValueHandling.Ignore)]
	public InternalPictureActor ActorImage { get; set; }
}
