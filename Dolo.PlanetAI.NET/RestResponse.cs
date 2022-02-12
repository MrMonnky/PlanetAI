using System.Net.Http;

namespace Dolo.PlanetAI.NET;

public class RestResponse<T>
{
	public bool Success { get; internal set; }

	public bool HasIpBan { get; internal set; }

	public T Result { get; internal set; }

	public string Json { get; internal set; }

	public HttpResponseMessage Response { get; internal set; }

	public HttpRequestMessage Request { get; internal set; }

	public HttpRequestException Exception { get; internal set; }

	internal RestResponse()
	{
	}
}
