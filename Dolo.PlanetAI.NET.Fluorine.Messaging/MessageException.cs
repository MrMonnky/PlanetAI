using System;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging;

internal class MessageException : AMFException
{
	private ASObject _extendedData;

	private string _faultCode = "Server.Processing";

	private object _rootCause;

	public string FaultCode
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

	public object RootCause
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

	public ASObject ExtendedData => _extendedData;

	public MessageException()
	{
		_extendedData = new ASObject();
	}

	public MessageException(ASObject extendedData)
	{
		_extendedData = extendedData;
	}

	public MessageException(Exception inner)
		: base(inner.Message, inner)
	{
		_extendedData = new ASObject();
		_rootCause = inner;
	}

	public MessageException(string message)
		: base(message)
	{
		_extendedData = new ASObject();
	}

	public MessageException(string message, Exception inner)
		: base(message, inner)
	{
		_extendedData = new ASObject();
	}

	public MessageException(Exception inner, ASObject extendedData)
		: base(inner.Message, inner)
	{
		_extendedData = extendedData;
		_rootCause = inner;
	}

	public MessageException(ASObject extendedData, string message)
		: base(message)
	{
		_extendedData = extendedData;
	}

	public MessageException(ASObject extendedData, string message, Exception inner)
		: base(message, inner)
	{
		_extendedData = extendedData;
		_rootCause = inner;
	}

	internal virtual ErrorMessage GetErrorMessage()
	{
		ErrorMessage errorMessage = new ErrorMessage();
		errorMessage.faultCode = FaultCode;
		errorMessage.faultString = Message;
		errorMessage.extendedData = ExtendedData;
		return errorMessage;
	}
}
