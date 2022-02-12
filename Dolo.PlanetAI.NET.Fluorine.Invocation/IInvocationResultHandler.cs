using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.Invocation;

internal interface IInvocationResultHandler
{
	void HandleResult(IInvocationManager invocationManager, MethodInfo methodInfo, object obj, object[] arguments, object result);
}
