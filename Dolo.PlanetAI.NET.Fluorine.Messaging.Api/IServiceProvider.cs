using System;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal interface IServiceProvider
{
	object GetService(Type serviceType);
}
