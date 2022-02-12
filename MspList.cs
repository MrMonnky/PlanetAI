using System.Collections.Generic;
using System.Net.Http;

namespace Dolo.PlanetAI;

public sealed class MspList<T> : List<T>
{
	public bool Success { get; internal set; }

	public bool HasIpBan { get; internal set; }

	public HttpRequestMessage HttpRequest { get; internal set; }

	public HttpResponseMessage HttpResponse { get; internal set; }

	public HttpRequestException HttpException { get; internal set; }

	internal MspList()
	{
	}
}
