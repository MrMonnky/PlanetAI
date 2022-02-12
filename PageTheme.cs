using System;
using Newtonsoft.Json;

namespace Dolo.PlanetAI;

public class PageTheme
{
	[JsonProperty("ThemeID", NullValueHandling = NullValueHandling.Ignore)]
	public int ThemeID { get; internal set; }

	[JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
	public string Name { get; internal set; }

	[JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
	public int Type { get; internal set; }

	[JsonProperty("CreatedDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime CreatedDate { get; internal set; }

	[JsonProperty("PublishableDate", NullValueHandling = NullValueHandling.Ignore)]
	public DateTime PublishableDate { get; internal set; }

	[JsonProperty("KeyBackgroundId", NullValueHandling = NullValueHandling.Ignore)]
	public int KeyBackgroundId { get; internal set; }

	[JsonProperty("LogoFilename", NullValueHandling = NullValueHandling.Ignore)]
	public string LogoFilename { get; internal set; }

	[JsonProperty("KeyBoyLookData", NullValueHandling = NullValueHandling.Ignore)]
	public object KeyBoyLookData { get; internal set; }

	[JsonProperty("KeyGirlLookData", NullValueHandling = NullValueHandling.Ignore)]
	public string KeyGirlLookData { get; internal set; }

	[JsonProperty("KeyItemId", NullValueHandling = NullValueHandling.Ignore)]
	public int KeyItemId { get; internal set; }

	[JsonProperty("SnapshotPath", NullValueHandling = NullValueHandling.Ignore)]
	public string SnapshotPath { get; internal set; }

	[JsonProperty("Discount", NullValueHandling = NullValueHandling.Ignore)]
	public int Discount { get; internal set; }

	internal PageTheme()
	{
	}
}
