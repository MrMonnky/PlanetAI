using System.Threading.Tasks;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal interface IFilter
{
	IFilter Next { get; set; }

	Task Invoke(AMFContext context);
}
