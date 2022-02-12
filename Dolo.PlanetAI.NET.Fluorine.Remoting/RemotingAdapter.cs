using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Invocation;
using Dolo.PlanetAI.NET.Fluorine.Messaging;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

namespace Dolo.PlanetAI.NET.Fluorine.Remoting;

internal class RemotingAdapter : ServiceAdapter
{
	public override async Task<object> Invoke(IMessage message)
	{
		RemotingMessage remotingMessage = message as RemotingMessage;
		string operation = remotingMessage.operation;
		string className = base.DestinationSettings.Properties["source"] as string;
		if (remotingMessage.source != null && remotingMessage.source != string.Empty)
		{
			if (className == "*")
			{
				className = remotingMessage.source;
			}
			if (className != remotingMessage.source)
			{
				string msg2 = __Res.GetString("Type_MismatchMissingSource", remotingMessage.source, base.DestinationSettings.Properties["source"] as string);
				throw new MessageException(msg2, new TypeLoadException(msg2));
			}
		}
		if (className == null)
		{
			throw new TypeInitializationException("null", null);
		}
		_ = className + "." + operation;
		IList parameterList = remotingMessage.body as IList;
		FactoryInstance factoryInstance = base.Destination.GetFactoryInstance();
		factoryInstance.Source = className;
		object instance = factoryInstance.Lookup();
		if (instance != null)
		{
			Type type = instance.GetType();
			if (!TypeHelper.GetTypeIsAccessible(type))
			{
				string msg = __Res.GetString("Type_InitError", type.FullName);
				throw new MessageException(msg, new TypeLoadException(msg));
			}
			Task<object> result;
			try
			{
				MethodInfo mi = MethodHandler.GetMethod(type, operation, parameterList);
				if (!(mi != null))
				{
					throw new MessageException(new MissingMethodException(className, operation));
				}
				ParameterInfo[] parameterInfos = mi.GetParameters();
				object[] args = new object[parameterInfos.Length];
				parameterList.CopyTo(args, 0);
				TypeHelper.NarrowValues(args, parameterInfos);
				InvocationHandler invocationHandler = new InvocationHandler(mi);
				result = invocationHandler.Invoke(instance, args);
				await result;
			}
			catch (TargetInvocationException exception2)
			{
				MessageException messageException2 = ((!(exception2.InnerException is MessageException)) ? new MessageException(exception2.InnerException) : (exception2.InnerException as MessageException));
				throw messageException2;
			}
			catch (Exception exception)
			{
				MessageException messageException = new MessageException(exception);
				throw messageException;
			}
			return result.Result;
		}
		throw new MessageException(new TypeInitializationException(className, null));
	}
}
