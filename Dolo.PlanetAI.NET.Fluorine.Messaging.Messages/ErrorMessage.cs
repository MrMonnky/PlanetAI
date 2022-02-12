using System;
using System.Security;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class ErrorMessage : AcknowledgeMessage
{
	public const string ClientAuthenticationError = "Client.Authentication";

	public const string ClientAuthorizationError = "Client.Authorization";

	private string _faultCode;

	private string _faultString;

	private string _faultDetail;

	private object _rootCause;

	private ASObject _extendedData;

	public string faultCode
	{
		get
		{
			return _faultCode;
		}
		set
		{
			_faultCode = value;
		}
	}

	public string faultString
	{
		get
		{
			return _faultString;
		}
		set
		{
			_faultString = value;
		}
	}

	public string faultDetail
	{
		get
		{
			return _faultDetail;
		}
		set
		{
			_faultDetail = value;
		}
	}

	public object rootCause
	{
		get
		{
			return _rootCause;
		}
		set
		{
			_rootCause = value;
		}
	}

	public ASObject extendedData
	{
		get
		{
			return _extendedData;
		}
		set
		{
			_extendedData = value;
		}
	}

	internal static ErrorMessage GetErrorMessage(IMessage message, Exception exception)
	{
		MessageException ex = null;
		ex = ((!(exception is MessageException)) ? new MessageException(exception) : (exception as MessageException));
		ErrorMessage errorMessage = ex.GetErrorMessage();
		if (message.clientId != null)
		{
			errorMessage.clientId = message.clientId;
		}
		else
		{
			errorMessage.clientId = Guid.NewGuid().ToString("D");
		}
		errorMessage.correlationId = message.messageId;
		errorMessage.destination = message.destination;
		if (exception is SecurityException)
		{
			errorMessage.faultCode = "Client.Authentication";
		}
		if (exception is UnauthorizedAccessException)
		{
			errorMessage.faultCode = "Client.Authorization";
		}
		return errorMessage;
	}
}
