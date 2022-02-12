using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dolo.PlanetAI;

internal static class Converter
{
	public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
	{
		MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
		DateParseHandling = DateParseHandling.None,
		Converters = { (JsonConverter)new IsoDateTimeConverter
		{
			DateTimeStyles = DateTimeStyles.AssumeUniversal
		} }
	};
}
