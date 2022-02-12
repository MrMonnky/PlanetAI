using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFBody
{
	public const string Recordset = "rs://";

	public const string OnResult = "/onResult";

	public const string OnStatus = "/onStatus";

	public const string OnDebugEvents = "/onDebugEvents";

	protected object _content;

	protected string _response;

	protected string _target;

	protected bool _ignoreResults;

	protected bool _isAuthenticationAction;

	protected bool _isDebug;

	protected bool _isDescribeService;

	public string Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	public bool IsEmptyTarget => _target == null || _target == string.Empty || _target == "null";

	public string Response
	{
		get
		{
			return _response;
		}
		set
		{
			_response = value;
		}
	}

	public object Content
	{
		get
		{
			return _content;
		}
		set
		{
			_content = value;
		}
	}

	public bool IsAuthenticationAction
	{
		get
		{
			return _isAuthenticationAction;
		}
		set
		{
			_isAuthenticationAction = value;
		}
	}

	public bool IgnoreResults
	{
		get
		{
			return _ignoreResults;
		}
		set
		{
			_ignoreResults = value;
		}
	}

	public bool IsRecordsetDelivery
	{
		get
		{
			if (_target.StartsWith("rs://"))
			{
				return true;
			}
			return false;
		}
	}

	public string TypeName
	{
		get
		{
			if (_target != "null" && _target != null && _target != string.Empty && _target.LastIndexOf('.') != -1)
			{
				string text = _target.Substring(0, _target.LastIndexOf('.'));
				if (IsRecordsetDelivery)
				{
					text = text.Substring("rs://".Length);
					text = text.Substring(text.IndexOf("/") + 1);
					text = text.Substring(0, text.LastIndexOf('.'));
				}
				return text;
			}
			return null;
		}
	}

	public string Method
	{
		get
		{
			if (_target != "null" && _target != null && _target != string.Empty && _target != null && _target.LastIndexOf('.') != -1)
			{
				string text = _target;
				if (IsRecordsetDelivery)
				{
					text = text.Substring("rs://".Length);
					text = text.Substring(text.IndexOf("/") + 1);
				}
				if (IsRecordsetDelivery)
				{
					text = text.Substring(0, text.LastIndexOf('.'));
				}
				return text.Substring(text.LastIndexOf('.') + 1);
			}
			return null;
		}
	}

	public string Call => TypeName + "." + Method;

	public AMFBody()
	{
	}

	public AMFBody(string target, string response, object content)
	{
		_target = target;
		_response = response;
		_content = content;
	}

	public string GetRecordsetArgs()
	{
		if (_target != null && IsRecordsetDelivery)
		{
			string text = _target.Substring("rs://".Length);
			return text.Substring(0, text.IndexOf("/"));
		}
		return null;
	}

	public string GetSignature()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(Target);
		IList parameterList = GetParameterList();
		for (int i = 0; i < parameterList.Count; i++)
		{
			object obj = parameterList[i];
			stringBuilder.Append(obj.GetType().FullName);
		}
		return stringBuilder.ToString();
	}

	public virtual IList GetParameterList()
	{
		IList list = null;
		if (!IsEmptyTarget)
		{
			if (!(_content is IList))
			{
				list = new List<object>();
				list.Add(_content);
			}
			else
			{
				list = _content as IList;
			}
		}
		else
		{
			object obj = Content;
			if (obj is IList)
			{
				obj = (obj as IList)[0];
			}
			if (obj is IMessage message && message is RemotingMessage)
			{
				list = message.body as IList;
			}
		}
		if (list == null)
		{
			list = new List<object>();
		}
		return list;
	}

	internal void WriteBody(ObjectEncoding objectEncoding, AMFWriter writer)
	{
		writer.Reset();
		if (Target == null)
		{
			writer.WriteUTF("null");
		}
		else
		{
			writer.WriteUTF(Target);
		}
		if (Response == null)
		{
			writer.WriteUTF("null");
		}
		else
		{
			writer.WriteUTF(Response);
		}
		writer.WriteInt32(-1);
		WriteBodyData(objectEncoding, writer);
	}

	protected virtual void WriteBodyData(ObjectEncoding objectEncoding, AMFWriter writer)
	{
		object content = Content;
		writer.WriteData(objectEncoding, content);
	}
}
