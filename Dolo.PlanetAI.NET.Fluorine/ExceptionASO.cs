using System;

namespace Dolo.PlanetAI.NET.Fluorine;

internal sealed class ExceptionASO : ASObject
{
	public ExceptionASO(Exception exception)
	{
		Add("code", "SERVER.PROCESSING");
		Add("level", "error");
		Add("description", exception.Message);
		Add("details", exception.StackTrace);
		Add("type", exception.GetType().FullName);
		if (exception.InnerException != null)
		{
			Add("rootcause", new ExceptionASO(exception.InnerException));
		}
	}
}
