namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class MessageOutput : AMFMessage
{
	public MessageOutput()
		: this(0)
	{
	}

	public MessageOutput(ushort version)
		: base(version)
	{
	}

	public bool ContainsResponse(AMFBody requestBody)
	{
		return GetResponse(requestBody) != null;
	}

	public ResponseBody GetResponse(AMFBody requestBody)
	{
		for (int i = 0; i < _bodies.Count; i++)
		{
			ResponseBody responseBody = _bodies[i] as ResponseBody;
			if (responseBody.RequestBody == requestBody)
			{
				return responseBody;
			}
		}
		return null;
	}
}
