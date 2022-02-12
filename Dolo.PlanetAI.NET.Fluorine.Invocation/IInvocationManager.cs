using System.Collections.Generic;

namespace Dolo.PlanetAI.NET.Fluorine.Invocation;

internal interface IInvocationManager
{
	Stack<object> Context { get; }

	Dictionary<object, object> Properties { get; }

	object Result { get; set; }
}
