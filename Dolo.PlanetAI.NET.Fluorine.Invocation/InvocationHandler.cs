using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dolo.PlanetAI.NET.Fluorine.Invocation;

internal class InvocationHandler
{
	private MethodInfo _methodInfo;

	public InvocationHandler(MethodInfo methodInfo)
	{
		_methodInfo = methodInfo;
	}

	public async Task<object> Invoke(object obj, object[] arguments)
	{
		object result;
		if (_methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) == null)
		{
			result = _methodInfo.Invoke(obj, arguments);
		}
		else
		{
			Task task = (Task)_methodInfo.Invoke(obj, arguments);
			await task;
			result = task.GetType().GetProperty("Result")!.GetValue(task);
		}
		object[] attributes = _methodInfo.GetCustomAttributes(inherit: false);
		if (attributes != null && attributes.Length != 0)
		{
			InvocationManager invocationManager = new InvocationManager
			{
				Result = result
			};
			for (int j = 0; j < attributes.Length; j++)
			{
				Attribute attribute = attributes[j] as Attribute;
				if (attribute is IInvocationCallback)
				{
					IInvocationCallback invocationCallback = attribute as IInvocationCallback;
					invocationCallback.OnInvoked(invocationManager, _methodInfo, obj, arguments, result);
				}
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				Attribute attribute2 = attributes[i] as Attribute;
				if (attribute2 is IInvocationResultHandler)
				{
					IInvocationResultHandler invocationResultHandler = attribute2 as IInvocationResultHandler;
					invocationResultHandler.HandleResult(invocationManager, _methodInfo, obj, arguments, result);
				}
			}
			return invocationManager.Result;
		}
		return result;
	}
}
