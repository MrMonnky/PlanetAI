using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.Invocation;

internal interface IInvocationCallback
{
	void OnInvoked(IInvocationManager invocationManager, MethodInfo methodInfo, object obj, object[] arguments, object result);
}
