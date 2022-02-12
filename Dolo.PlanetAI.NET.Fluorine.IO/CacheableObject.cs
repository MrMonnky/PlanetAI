namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class CacheableObject
{
	private object _obj;

	private string _source;

	private string _cacheKey;

	public object Object => _obj;

	public string CacheKey => _cacheKey;

	public string Source => _source;

	public CacheableObject(string source, string cacheKey, object obj)
	{
		_source = source;
		_cacheKey = cacheKey;
		_obj = obj;
	}
}
