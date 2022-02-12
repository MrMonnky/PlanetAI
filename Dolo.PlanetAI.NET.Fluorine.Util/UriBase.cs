using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal class UriBase
{
	private string _user;

	private string _password;

	private string _path;

	private string _host;

	private string _protocol;

	private string _port;

	private NameValueCollection _parameters;

	public string Path
	{
		get
		{
			return _path;
		}
		set
		{
			if (_path != value)
			{
				_path = value;
			}
		}
	}

	public string Host
	{
		get
		{
			return _host;
		}
		set
		{
			if (_host != value)
			{
				_host = value;
			}
		}
	}

	public NameValueCollection Parameters
	{
		get
		{
			return _parameters;
		}
		set
		{
			if (_parameters != value)
			{
				_parameters = value;
			}
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			if (_password != value)
			{
				_password = value;
			}
		}
	}

	public string Port
	{
		get
		{
			return _port;
		}
		set
		{
			if (_port != value)
			{
				_port = value;
			}
		}
	}

	public string Protocol
	{
		get
		{
			return _protocol;
		}
		set
		{
			if (_protocol != value)
			{
				_protocol = value;
			}
		}
	}

	public string User
	{
		get
		{
			return _user;
		}
		set
		{
			if (_user != value)
			{
				_user = value;
			}
		}
	}

	public string Uri
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (Protocol != null)
			{
				stringBuilder.Append(Protocol);
				stringBuilder.Append("://");
				if (User != null && User != string.Empty)
				{
					stringBuilder.Append($"{User}:{Password}@");
				}
				if (Host != null)
				{
					stringBuilder.Append(Host);
				}
				if (Port != null)
				{
					stringBuilder.Append(":");
					stringBuilder.Append(Port);
				}
				if (Path != null && Path != string.Empty)
				{
					stringBuilder.Append($"/{Path}");
				}
				else
				{
					stringBuilder.Append("/");
				}
			}
			if (_parameters != null)
			{
				for (int i = 0; i < _parameters.Count; i++)
				{
					string key = _parameters.GetKey(i);
					string value = _parameters.Get(i);
					if (i == 0)
					{
						stringBuilder.Append("?");
					}
					else
					{
						stringBuilder.Append("&");
					}
					stringBuilder.Append(key);
					stringBuilder.Append("=");
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}
		set
		{
			ParseUri(value);
		}
	}

	public UriBase(UriBase uri)
	{
		Clear();
		ParseUri(uri.Uri);
	}

	public UriBase()
	{
		Clear();
	}

	public UriBase(string uri)
	{
		Clear();
		ParseUri(uri);
	}

	public UriBase(string user, string password, string path, string host, string protocol, string port, NameValueCollection parameters)
	{
		_user = user;
		_password = password;
		_path = path;
		_host = host;
		_protocol = protocol;
		_port = port;
		_parameters = parameters;
	}

	public UriBase(string user, string password, string path, string host, string protocol, string port)
	{
		_user = user;
		_password = password;
		_path = path;
		_host = host;
		_protocol = protocol;
		_port = port;
	}

	public void Clear()
	{
		_protocol = "";
		_host = "";
		_port = null;
		_path = "";
		_user = "";
		_password = "";
		_parameters = null;
	}

	private void InternalParseUri(string url)
	{
		try
		{
			string text = url;
			if (text.Length == 0)
			{
				text = ":///";
			}
			Regex regex = new Regex("^(?<protocol>[\\w\\%]*)://((?'username'[\\w\\%]*)(:(?'password'[\\w\\%]*))?@)?(?'host'[\\{\\}\\w\\.\\(\\)\\-\\%\\\\\\$]*)(:?(?'port'[\\{\\}\\w\\.]+))?(/(?'path'[^?]*)?(\\?(?'params'.*))?)?");
			Match match = regex.Match(text);
			if (!match.Success)
			{
				throw new ApplicationException("This Uri cannot be parsed.");
			}
			string user = HttpUtility.UrlDecode(match.Result("${username}"));
			string password = HttpUtility.UrlDecode(match.Result("${password}"));
			string path = HttpUtility.UrlDecode(match.Result("${path}"));
			string host = HttpUtility.UrlDecode(match.Result("${host}"));
			string protocol = HttpUtility.UrlDecode(match.Result("${protocol}"));
			string port = null;
			if (match.Result("${port}").Length != 0)
			{
				port = match.Result("${port}");
			}
			string text2 = match.Result("${params}");
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (text2 != null && text2 != string.Empty)
			{
				char[] separator = new char[1] { '&' };
				string[] array = text2.Split(separator);
				IEnumerator enumerator = array.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text3 = (string)enumerator.Current;
					separator = new char[1] { '=' };
					string[] array2 = text3.Split(separator, 2);
					if (array2.Length != 2)
					{
						throw new ApplicationException("This Uri cannot be parsed. Invalid parameter.");
					}
					nameValueCollection.Add(HttpUtility.UrlDecode(array2[0]), HttpUtility.UrlDecode(array2[1]));
				}
			}
			_user = user;
			_password = password;
			_path = path;
			_host = host;
			_protocol = protocol;
			_port = port;
			_parameters = nameValueCollection;
		}
		catch (Exception ex)
		{
			if (ex is ApplicationException)
			{
				throw;
			}
			throw new ApplicationException("This Uri cannot be parsed.", ex);
		}
	}

	protected void ParseUri(string uri)
	{
		InternalParseUri(uri);
	}

	public bool EqualTo(UriBase uri)
	{
		if (uri == null)
		{
			return false;
		}
		return Uri == uri.Uri;
	}

	public void CopyTo(UriBase uri)
	{
		Uri = uri.Uri;
	}
}
