using System.IO;

namespace Dolo.PlanetAI.NET.Fluorine.DependencyInjection;

internal interface IHttpContext
{
	Stream GetInputStream();

	Stream GetOutputStream();

	void Clear(object context);

	string GetContentType();

	void SetContentType(string contentType);

	void Finish(object context);
}
