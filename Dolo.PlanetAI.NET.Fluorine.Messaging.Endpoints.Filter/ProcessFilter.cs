using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.Invocation;
using Dolo.PlanetAI.NET.Fluorine.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Services;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class ProcessFilter : AbstractFilter
{
	private EndpointBase _endpoint;

	public ProcessFilter(EndpointBase endpoint)
	{
		_endpoint = endpoint;
	}

	public override Task Invoke(AMFContext context)
	{
		MessageOutput messageOutput = context.MessageOutput;
		for (int i = 0; i < context.AMFMessage.BodyCount; i++)
		{
			AMFBody bodyAt = context.AMFMessage.GetBodyAt(i);
			ResponseBody responseBody = null;
			if (bodyAt.IsEmptyTarget)
			{
				continue;
			}
			responseBody = messageOutput.GetResponse(bodyAt);
			if (responseBody != null)
			{
				continue;
			}
			try
			{
				MessageBroker messageBroker = _endpoint.GetMessageBroker();
				if (!(messageBroker.GetService("remoting-service") is RemotingService remotingService))
				{
					string @string = __Res.GetString("Service_NotFound", "remoting-service");
					responseBody = new ErrorResponseBody(bodyAt, new AMFException(@string));
					messageOutput.AddBody(responseBody);
					continue;
				}
				Destination destination = null;
				if (destination == null)
				{
					destination = remotingService.GetDestinationWithSource(bodyAt.TypeName);
				}
				if (destination == null)
				{
					destination = remotingService.DefaultDestination;
				}
				if (destination == null)
				{
					string string2 = __Res.GetString("Destination_NotFound", bodyAt.TypeName);
					responseBody = new ErrorResponseBody(bodyAt, new AMFException(string2));
					messageOutput.AddBody(responseBody);
					continue;
				}
				string text = bodyAt.TypeName + "." + bodyAt.Method;
				IList parameterList = bodyAt.GetParameterList();
				FactoryInstance factoryInstance = destination.GetFactoryInstance();
				factoryInstance.Source = bodyAt.TypeName;
				object obj = factoryInstance.Lookup();
				MethodInfo methodInfo;
				ParameterInfo[] parameters;
				object[] array;
				if (obj != null)
				{
					if (!TypeHelper.GetTypeIsAccessible(obj.GetType()))
					{
						string string3 = __Res.GetString("Type_InitError", bodyAt.TypeName);
						responseBody = new ErrorResponseBody(bodyAt, new AMFException(string3));
						messageOutput.AddBody(responseBody);
						continue;
					}
					methodInfo = null;
					methodInfo = (bodyAt.IsRecordsetDelivery ? obj.GetType().GetMethod(bodyAt.Method) : MethodHandler.GetMethod(obj.GetType(), bodyAt.Method, bodyAt.GetParameterList()));
					if (methodInfo != null)
					{
						parameters = methodInfo.GetParameters();
						array = new object[parameters.Length];
						if (!bodyAt.IsRecordsetDelivery)
						{
							if (array.Length != parameterList.Count)
							{
								string string4 = __Res.GetString("Arg_Mismatch", parameterList.Count, methodInfo.Name, array.Length);
								responseBody = new ErrorResponseBody(bodyAt, new ArgumentException(string4));
								messageOutput.AddBody(responseBody);
								continue;
							}
							parameterList.CopyTo(array, 0);
							goto IL_0380;
						}
						if (bodyAt.Target.EndsWith(".release"))
						{
							responseBody = new ResponseBody(bodyAt, null);
							messageOutput.AddBody(responseBody);
							continue;
						}
						string text2 = parameterList[0] as string;
						string recordsetArgs = bodyAt.GetRecordsetArgs();
						byte[] bytes = Convert.FromBase64String(recordsetArgs);
						recordsetArgs = Encoding.UTF8.GetString(bytes);
						if (recordsetArgs != null && recordsetArgs != string.Empty)
						{
							string[] array2 = recordsetArgs.Split(new char[1] { ',' });
							for (int j = 0; j < array2.Length; j++)
							{
								if (array2[j] == string.Empty)
								{
									array[j] = null;
								}
								else
								{
									array[j] = array2[j];
								}
							}
						}
						goto IL_0380;
					}
					responseBody = new ErrorResponseBody(bodyAt, new MissingMethodException(bodyAt.TypeName, bodyAt.Method));
					goto IL_043a;
				}
				responseBody = new ErrorResponseBody(bodyAt, new TypeInitializationException(bodyAt.TypeName, null));
				goto IL_043a;
				IL_0380:
				TypeHelper.NarrowValues(array, parameters);
				try
				{
					InvocationHandler invocationHandler = new InvocationHandler(methodInfo);
					object content = invocationHandler.Invoke(obj, array);
					responseBody = new ResponseBody(bodyAt, content);
				}
				catch (UnauthorizedAccessException exception)
				{
					responseBody = new ErrorResponseBody(bodyAt, exception);
				}
				catch (Exception ex)
				{
					responseBody = ((!(ex is TargetInvocationException) || ex.InnerException == null) ? new ErrorResponseBody(bodyAt, ex) : new ErrorResponseBody(bodyAt, ex.InnerException));
				}
				goto IL_043a;
			}
			catch (Exception exception2)
			{
				responseBody = new ErrorResponseBody(bodyAt, exception2);
				goto IL_043a;
			}
			IL_043a:
			messageOutput.AddBody(responseBody);
		}
		return Task.FromResult<object>(null);
	}
}
