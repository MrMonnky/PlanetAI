using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class DeserializationFilter : AbstractFilter
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
		AMFDeserializer aMFDeserializer = new AMFDeserializer(context.InputStream);
		aMFDeserializer.UseLegacyCollection = _useLegacyCollection;
		AMFMessage aMFMessage2 = (context.AMFMessage = aMFDeserializer.ReadAMFMessage());
		context.MessageOutput = new MessageOutput(aMFMessage2.Version);
		if (aMFDeserializer.FailedAMFBodies.Length != 0)
		{
			AMFBody[] failedAMFBodies = aMFDeserializer.FailedAMFBodies;
			for (int i = 0; i < failedAMFBodies.Length; i++)
			{
				context.MessageOutput.AddBody(failedAMFBodies[i]);
			}
		}
		return Task.FromResult<object>(null);
	}
}
