using System;
using System.Collections;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class NetworkSettings : Hashtable
{
	public bool PagingEnabled
	{
		get
		{
			if (ContainsKey("paging_enabled"))
			{
				return Convert.ToBoolean(this["paging_enabled"]);
			}
			return false;
		}
	}

	public int PagingSize
	{
		get
		{
			if (ContainsKey("paging_pageSize"))
			{
				return Convert.ToInt32(this["paging_pageSize"]);
			}
			return 0;
		}
	}

	public int SessionTimeout
	{
		get
		{
			if (ContainsKey("session-timeout"))
			{
				return Convert.ToInt32(this["session-timeout"]);
			}
			return 20;
		}
	}

	private NetworkSettings()
	{
	}

	internal NetworkSettings(XmlNode networkDefinitionNode)
	{
		foreach (XmlNode item in networkDefinitionNode.SelectNodes("*")!)
		{
			if (item.InnerXml != null && item.InnerXml != string.Empty)
			{
				this[item.Name] = item.InnerXml;
			}
			else
			{
				if (item.Attributes == null)
				{
					continue;
				}
				foreach (XmlAttribute item2 in item.Attributes!)
				{
					this[item.Name + "_" + item2.Name] = item2.Value;
				}
			}
		}
	}
}
