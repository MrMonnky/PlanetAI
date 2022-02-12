using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Invocation;

internal class InvocationManager : IInvocationManager
{
	private Stack<object> _context;

	private Dictionary<object, object> _properties;

	private object _result;

	public Stack<object> Context => _context;

	public Dictionary<object, object> Properties => _properties;

	public object Result
	{
		get
		{
			return _result;
		}
		set
		{
			_result = value;
		}
	}

	public InvocationManager()
	{
		_context = new Stack<object>();
		_properties = new Dictionary<object, object>();
	}
}
