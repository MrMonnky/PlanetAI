using System.Collections;
using System.Collections.Generic;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

namespace Dolo.PlanetAI.NET.Fluorine.Context;

internal sealed class AMFWebContext : AMFContext
{
	public static Dictionary<string, object> ItemsContext = new Dictionary<string, object>();

	public override IDictionary Items => ItemsContext;

	public override IConnection Connection => Items["__@amfconnection"] as IConnection;

	public override IClient Client => Items["__@amfclient"] as IClient;

	internal AMFWebContext()
	{
	}

	internal static void Initialize()
	{
		ItemsContext["__@amfcontext"] = new AMFWebContext();
	}

	internal void SetConnection(IConnection connection)
	{
		Items["__@amfconnection"] = connection;
	}

	internal override void SetCurrentClient(IClient client)
	{
		Items["__@amfclient"] = client;
	}
}
