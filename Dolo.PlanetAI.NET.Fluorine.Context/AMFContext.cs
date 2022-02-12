using System;
using System.Collections;
using System.IO;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

namespace Dolo.PlanetAI.NET.Fluorine.Context;

internal abstract class AMFContext
{
	public const string AMFContextKey = "__@amfcontext";

	public const string AMFTicket = "amfauthticket";

	public const string AMFSessionAttribute = "__@amfsession";

	public const string AMFConnectionKey = "__@amfconnection";

	public const string AMFClientKey = "__@amfclient";

	public const string AMFStreamIdKey = "__@amfstreamid";

	public const string AMFDataServiceTransaction = "__@amfdataservicetransaction";

	public const string FlexClientIdHeader = "DSId";

	public static AMFContext Current
	{
		get
		{
			if (AMFWebContext.ItemsContext.ContainsKey("__@amfcontext"))
			{
				return AMFWebContext.ItemsContext["__@amfcontext"] as AMFContext;
			}
			return null;
		}
	}

	public abstract IDictionary Items { get; }

	public virtual string ApplicationBaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

	public virtual IConnection Connection => null;

	public abstract IClient Client { get; }

	public string ClientId
	{
		get
		{
			if (Client != null)
			{
				return Client.Id;
			}
			return null;
		}
	}

	internal AMFContext()
	{
	}

	public virtual string GetFullPath(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		string text = "";
		try
		{
			string applicationBaseDirectory = ApplicationBaseDirectory;
			if (applicationBaseDirectory != null)
			{
				Uri uri = new Uri(applicationBaseDirectory);
				if (uri.IsFile)
				{
					text = uri.LocalPath;
				}
			}
		}
		catch
		{
		}
		if (text != null && text.Length > 0)
		{
			return Path.GetFullPath(Path.Combine(text, path));
		}
		return Path.GetFullPath(path);
	}

	internal virtual void SetCurrentClient(IClient client)
	{
	}
}
