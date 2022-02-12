using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class SerializationFilter : AbstractFilter
{
	private bool _useLegacyCollection = false;

	public bool UseLegacyCollection
	{
		get
		{
			return _useLegacyCollection;
		}
		set
		{
			_useLegacyCollection = value;
		}
	}

	public override Task Invoke(AMFContext context)
	{
		AMFSerializer aMFSerializer = new AMFSerializer(context.MessageOutput);
		aMFSerializer.UseLegacyCollection = _useLegacyCollection;
		return Task.FromResult<object>(null);
	}
}
