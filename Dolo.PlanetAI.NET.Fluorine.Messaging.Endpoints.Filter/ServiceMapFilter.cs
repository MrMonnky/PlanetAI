using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Configuration;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class ServiceMapFilter : AbstractFilter
{
	private EndpointBase _endpoint;

	public ServiceMapFilter(EndpointBase endpoint)
	{
		_endpoint = endpoint;
	}

	public override Task Invoke(AMFContext context)
	{
		for (int i = 0; i < context.AMFMessage.BodyCount; i++)
		{
			AMFBody bodyAt = context.AMFMessage.GetBodyAt(i);
			if (!bodyAt.IsEmptyTarget && AMFConfiguration.Instance.ServiceMap != null)
			{
				string typeName = bodyAt.TypeName;
				string method = bodyAt.Method;
				if (typeName != null && AMFConfiguration.Instance.ServiceMap.Contains(typeName))
				{
					string serviceLocation = AMFConfiguration.Instance.ServiceMap.GetServiceLocation(typeName);
					method = AMFConfiguration.Instance.ServiceMap.GetMethod(typeName, method);
					string text2 = (bodyAt.Target = serviceLocation + "." + method);
				}
			}
		}
		return Task.FromResult<object>(null);
	}
}
