using System;
using System.Collections;
using System.Xml;
using Dolo.PlanetAI.NET.Fluorine.Util;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class ChannelSettings : Hashtable
{
	public const string ContextRoot = "{context.root}";

	public const string PollingEnabledKey = "polling-enabled";

	public const string PollingIntervalSecondsKey = "polling-interval-seconds";

	public const string BindAddressKey = "bind-address";

	public const string PollingIntervalMillisKey = "polling-interval-millis";

	public const string WaitIntervalMillisKey = "wait-interval-millis";

	public const string MaxWaitingPollRequestsKey = "max-waiting-poll-requests";

	private UriBase _uri;

	private string _id;

	private string _endpointClass;

	private string _endpointUri;

	private int _maxWaitingPollRequests;

	private int _waitIntervalMillis;

	public string Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	public string Class => _endpointClass;

	public string Uri
	{
		set
		{
			_uri = new UriBase(value);
		}
	}

	public bool IsPollingEnabled
	{
		get
		{
			if (ContainsKey("polling-enabled"))
			{
				return System.Convert.ToBoolean(this["polling-enabled"]);
			}
			return false;
		}
	}

	public int PollingIntervalSeconds
	{
		get
		{
			if (ContainsKey("polling-interval-seconds"))
			{
				return System.Convert.ToInt32(this["polling-interval-seconds"]);
			}
			return 8;
		}
	}

	public string BindAddress
	{
		get
		{
			if (ContainsKey("bind-address"))
			{
				return this["bind-address"]!.ToString();
			}
			return null;
		}
	}

	public int PollingIntervalMillis
	{
		get
		{
			if (ContainsKey("polling-interval-millis"))
			{
				return System.Convert.ToInt32(this["polling-interval-millis"]);
			}
			return 3000;
		}
	}

	public int WaitIntervalMillis => _waitIntervalMillis;

	public int MaxWaitingPollRequests => _maxWaitingPollRequests;

	internal ChannelSettings()
	{
		_maxWaitingPollRequests = 0;
		_waitIntervalMillis = 0;
	}

	internal ChannelSettings(string id, string endpointClass, string endpointUri)
		: this()
	{
		_id = id;
		_endpointClass = endpointClass;
		_endpointUri = endpointUri;
		_uri = new UriBase(_endpointUri);
	}

	internal ChannelSettings(string id, string endpointClass)
		: this()
	{
		_id = id;
		_endpointClass = endpointClass;
	}

	internal ChannelSettings(XmlNode channelDefinitionNode)
	{
		_id = channelDefinitionNode.Attributes!["id"]!.Value;
		XmlNode xmlNode = channelDefinitionNode.SelectSingleNode("endpoint");
		_endpointClass = xmlNode.Attributes!["class"]!.Value;
		_endpointUri = xmlNode.Attributes!["uri"]!.Value;
		_uri = new UriBase(_endpointUri);
		XmlNode xmlNode2 = channelDefinitionNode.SelectSingleNode("properties");
		if (xmlNode2 != null)
		{
			foreach (XmlNode item in xmlNode2.SelectNodes("*")!)
			{
				this[item.Name] = item.InnerXml;
			}
		}
		if (ContainsKey("max-waiting-poll-requests"))
		{
			_maxWaitingPollRequests = System.Convert.ToInt32(this["max-waiting-poll-requests"]);
		}
		if (ContainsKey("wait-interval-millis"))
		{
			_waitIntervalMillis = System.Convert.ToInt32(this["wait-interval-millis"]);
		}
	}

	public UriBase GetUri()
	{
		return _uri;
	}

	internal bool Bind(string path, string contextPath)
	{
		if (_uri != null)
		{
			string text = _uri.Path;
			if (!text.StartsWith("/"))
			{
				text = "/" + text;
			}
			if (contextPath == "/")
			{
				contextPath = string.Empty;
			}
			text = ((text.IndexOf("/{context.root}") == -1) ? text.Replace("{context.root}", contextPath) : text.Replace("/{context.root}", contextPath));
			if (text.ToLower() == path.ToLower())
			{
				return true;
			}
		}
		return false;
	}

	public override string ToString()
	{
		return "Channel id = " + _id + " uri: " + _uri.Uri + " endpointPath: " + _uri.Path;
	}
}
