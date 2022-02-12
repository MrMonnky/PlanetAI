using Newtonsoft.Json;

namespace Dolo.PlanetAI;

internal class InternalSpecialFriend
{
	[JsonProperty("ActorId", NullValueHandling = NullValueHandling.Ignore)]
	public int ActorId { get; set; }

	[JsonProperty("RelationshipTypeId", NullValueHandling = NullValueHandling.Ignore)]
	public int RelationshipTypeId { get; set; }

	[JsonProperty("OrderBy", NullValueHandling = NullValueHandling.Ignore)]
	public long? OrderBy { get; set; }

	[JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
	public int Status { get; set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; set; }

	[JsonProperty("SkinSWF", NullValueHandling = NullValueHandling.Ignore)]
	public string SkinSwf { get; set; }

	[JsonProperty("NebulaProfileId", NullValueHandling = NullValueHandling.Ignore)]
	public string NebulaProfileId { get; set; }

	[JsonProperty("AMF_CLASSNAME", NullValueHandling = NullValueHandling.Ignore)]
	public string AmfClassname { get; set; }
}
