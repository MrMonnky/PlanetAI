using System;

namespace Dolo.PlanetAI.NET.Fluorine.Exceptions;

[Serializable]
internal class UnexpectedAMF : AMFException
{
	public UnexpectedAMF()
	{
	}

	public UnexpectedAMF(string message)
		: base(message)
	{
	}
}
