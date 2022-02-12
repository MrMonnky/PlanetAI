using System.Collections;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class ResponseBody : AMFBody
{
	private AMFBody _requestBody;

	public AMFBody RequestBody
	{
		get
		{
			return _requestBody;
		}
		set
		{
			_requestBody = value;
		}
	}

	internal ResponseBody()
	{
	}

	public ResponseBody(AMFBody requestBody)
	{
		_requestBody = requestBody;
	}

	public ResponseBody(AMFBody requestBody, object content)
	{
		_requestBody = requestBody;
		_target = requestBody.Response + "/onResult";
		_content = content;
		_response = "null";
	}

	public override IList GetParameterList()
	{
		if (_requestBody == null)
		{
			return null;
		}
		return _requestBody.GetParameterList();
	}
}
