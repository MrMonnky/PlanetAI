using System;
using System.Runtime.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Exceptions;

[Serializable]
internal class AMFException : ApplicationException
{
	public AMFException()
	{
	}

	public AMFException(string message)
		: base(message)
	{
	}

	public AMFException(string message, Exception inner)
		: base(message, inner)
	{
	}

	public AMFException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
