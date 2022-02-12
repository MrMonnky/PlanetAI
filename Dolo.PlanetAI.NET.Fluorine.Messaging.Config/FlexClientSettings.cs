using System;
using System.Xml;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Config;

internal sealed class FlexClientSettings
{
	private int _timeoutMinutes = 0;

	public int TimeoutMinutes
	{
		get
		{
			return _timeoutMinutes;
		}
		set
		{
			_timeoutMinutes = value;
		}
	}

	internal FlexClientSettings()
	{
	}

	internal FlexClientSettings(XmlNode flexClientNode)
	{
		XmlNode xmlNode = flexClientNode.SelectSingleNode("timeout-minutes");
		if (xmlNode != null)
		{
			_timeoutMinutes = Convert.ToInt32(xmlNode.InnerXml);
		}
	}
}
