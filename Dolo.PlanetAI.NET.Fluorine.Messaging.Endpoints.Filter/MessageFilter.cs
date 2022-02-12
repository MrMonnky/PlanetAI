using System;
using System.Collections;
using System.Security;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Context;
using Dolo.PlanetAI.NET.Fluorine.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints.Filter;

internal class MessageFilter : AbstractFilter
{
	private EndpointBase _endpoint;

	public MessageFilter(EndpointBase endpoint)
	{
		_endpoint = endpoint;
	}

	public override async Task Invoke(AMFContext context)
	{
		MessageOutput messageOutput = context.MessageOutput;
		for (int i = 0; i < context.AMFMessage.BodyCount; i++)
		{
			AMFBody amfBody = context.AMFMessage.GetBodyAt(i);
			if (!amfBody.IsEmptyTarget)
			{
				continue;
			}
			object content2 = amfBody.Content;
			if (content2 is IList)
			{
				content2 = (content2 as IList)[0];
			}
			IMessage message = content2 as IMessage;
			if (message == null)
			{
				continue;
			}
			if (Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.Client == null)
			{
				Dolo.PlanetAI.NET.Fluorine.Context.AMFContext.Current.SetCurrentClient(_endpoint.GetMessageBroker().ClientRegistry.GetClient(message));
			}
			if (message.clientId == null)
			{
				message.clientId = Guid.NewGuid().ToString("D");
			}
			ResponseBody responseBody3 = messageOutput.GetResponse(amfBody);
			if (responseBody3 != null)
			{
				continue;
			}
			try
			{
				if (context.AMFMessage.BodyCount > 1)
				{
					CommandMessage commandMessage = message as CommandMessage;
					if (commandMessage != null && commandMessage.operation == 2)
					{
						commandMessage.SetHeader(CommandMessage.AMFSuppressPollWaitHeader, null);
					}
				}
				Task<IMessage> resultMessage = _endpoint.ServiceMessage(message);
				await resultMessage;
				if (resultMessage.Result is ErrorMessage)
				{
					ErrorMessage errorMessage = resultMessage.Result as ErrorMessage;
					responseBody3 = new ErrorResponseBody(amfBody, message, resultMessage.Result as ErrorMessage);
					if (errorMessage.faultCode == "Client.Authentication")
					{
						messageOutput.AddBody(responseBody3);
						for (int j = i + 1; j < context.AMFMessage.BodyCount; j++)
						{
							amfBody = context.AMFMessage.GetBodyAt(j);
							if (amfBody.IsEmptyTarget)
							{
								content2 = amfBody.Content;
								if (content2 is IList)
								{
									content2 = (content2 as IList)[0];
								}
								message = content2 as IMessage;
								if (message != null)
								{
									responseBody3 = new ErrorResponseBody(amfBody, message, new SecurityException(errorMessage.faultString));
									messageOutput.AddBody(responseBody3);
								}
							}
						}
						break;
					}
				}
				else
				{
					responseBody3 = new ResponseBody(amfBody, resultMessage.Result);
				}
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				responseBody3 = new ErrorResponseBody(amfBody, message, exception);
			}
			messageOutput.AddBody(responseBody3);
		}
	}
}
