using System.Threading.Tasks;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal abstract class AbstractFilter : IFilter
{
	private IFilter _next;

	public virtual IFilter Next
	{
		get
		{
			return _next;
		}
		set
		{
			_next = value;
		}
	}

	public AbstractFilter()
	{
	}

	public virtual Task Invoke(AMFContext context)
	{
		return Task.FromResult<object>(null);
	}
}
