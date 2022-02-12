using System;
using System.Collections;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class ErrorResponseBody : ResponseBody
{
	private ErrorResponseBody()
	{
	}

	public ErrorResponseBody(AMFBody requestBody, string error)
		: base(requestBody)
	{
		base.IgnoreResults = requestBody.IgnoreResults;
		base.Target = requestBody.Response + "/onStatus";
		base.Response = null;
		base.Content = error;
	}

	public ErrorResponseBody(AMFBody requestBody, Exception exception)
		: base(requestBody)
	{
		base.Content = exception;
		if (requestBody.IsEmptyTarget)
		{
			object obj = requestBody.Content;
			if (obj is IList)
			{
				obj = (obj as IList)[0];
			}
			if (obj is IMessage message)
			{
				ErrorMessage errorMessage2 = (ErrorMessage)(base.Content = ErrorMessage.GetErrorMessage(message, exception));
			}
		}
		base.IgnoreResults = requestBody.IgnoreResults;
		base.Target = requestBody.Response + "/onStatus";
		base.Response = null;
	}

	public ErrorResponseBody(AMFBody requestBody, IMessage message, Exception exception)
		: base(requestBody)
	{
		ErrorMessage errorMessage2 = (ErrorMessage)(base.Content = ErrorMessage.GetErrorMessage(message, exception));
		base.Target = requestBody.Response + "/onStatus";
		base.IgnoreResults = requestBody.IgnoreResults;
		base.Response = "";
	}

	public ErrorResponseBody(AMFBody requestBody, IMessage message, ErrorMessage errorMessage)
		: base(requestBody)
	{
		base.Content = errorMessage;
		base.Target = requestBody.Response + "/onStatus";
		base.IgnoreResults = requestBody.IgnoreResults;
		base.Response = "";
	}
}
