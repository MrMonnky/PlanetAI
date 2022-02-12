using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dolo.PlanetAI.NET.Fluorine.Context;
using Dolo.PlanetAI.NET.Fluorine.DependencyInjection;
using Dolo.PlanetAI.NET.Fluorine.Messaging;

namespace Dolo.PlanetAI.NET.Fluorine;

internal class AMFGateway
{
	internal const string AMFHttpCompressKey = "__@amfhttpcompress";

	internal const string AMFMessageServerKey = "__@amfmessageserver";

	private static string _sourceName = null;

	private static object _objLock = new object();

	private static bool _initialized = false;

	private static MessageServer messageServer;

	public static bool ReturnsDateTimeAsDateString { get; set; }

	private static string GetPageName(string requestPath)
	{
		if (requestPath.IndexOf('?') != -1)
		{
			requestPath = requestPath.Substring(0, requestPath.IndexOf('?'));
		}
		return requestPath.Remove(0, requestPath.LastIndexOf("/") + 1);
	}

	public void Init()
	{
		if (!_initialized)
		{
			lock (_objLock)
			{
				if (!_initialized)
				{
					_initialized = true;
				}
			}
		}
		AMFWebContext.Initialize();
		if (messageServer != null)
		{
			return;
		}
		lock (_objLock)
		{
			if (messageServer == null)
			{
				messageServer = new MessageServer();
				try
				{
					messageServer.Init();
					messageServer.Start();
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
		}
	}

	public void Dispose()
	{
	}

	private void CurrentDomain_DomainUnload(object sender, EventArgs e)
	{
		try
		{
			lock (_objLock)
			{
				if (messageServer != null)
				{
					messageServer.Stop();
				}
				messageServer = null;
			}
		}
		catch (Exception)
		{
		}
	}

	public async Task PreRequest(object context)
	{
		if (!(HttpContextManager.HttpContext.GetContentType() == "application/x-amf"))
		{
			return;
		}
		HttpContextManager.HttpContext.Clear(context);
		HttpContextManager.HttpContext.SetContentType("application/x-amf");
		try
		{
			AMFWebContext.Initialize();
			if (messageServer != null)
			{
				await messageServer.Service();
			}
			HttpContextManager.HttpContext.Finish(context);
		}
		catch (Exception)
		{
			HttpContextManager.HttpContext.Clear(context);
			HttpContextManager.HttpContext.Finish(context);
		}
	}

	private void WireAppDomain()
	{
		string text = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "webengine.dll");
		if (File.Exists(text))
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
			_sourceName = string.Format(CultureInfo.InvariantCulture, "ASP.NET {0}.{1}.{2}.0", versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart);
		}
		AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
	}
}
