using System;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api.Service;

internal interface IServiceCall
{
	bool IsSuccess { get; }

	string ServiceMethodName { get; }

	string ServiceName { get; }

	object[] Arguments { get; }

	byte Status { get; set; }

	Exception Exception { get; set; }
}
