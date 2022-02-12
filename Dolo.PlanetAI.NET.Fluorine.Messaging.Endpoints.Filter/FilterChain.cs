using System.Threading.Tasks;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class FilterChain
{
	private IFilter _filter;

	public FilterChain(IFilter filter)
	{
		_filter = filter;
	}

	public async Task InvokeFilters(AMFContext context)
	{
		for (IFilter filter = _filter; filter != null; filter = filter.Next)
		{
			await filter.Invoke(context);
		}
	}
}
