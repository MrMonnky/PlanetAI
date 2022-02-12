namespace Dolo.PlanetAI.NET.Fluorine.DependencyInjection;

internal class HttpContextManager
{
	public static string ContextPath { get; private set; }

	public static bool IsSecure { get; private set; }

	public static IHttpContext HttpContext { get; private set; }

	public static void Initialize(string contextPath, bool isSecure, IHttpContext httpContext)
	{
		ContextPath = contextPath;
		IsSecure = isSecure;
		HttpContext = httpContext;
	}
}
