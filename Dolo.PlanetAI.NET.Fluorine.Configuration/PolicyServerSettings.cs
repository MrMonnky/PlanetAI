using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class PolicyServerSettings
{
	private bool _enable;

	private string _policyFile;

	[XmlAttribute(DataType = "boolean", AttributeName = "enable")]
	public bool Enable
	{
		get
		{
			return _enable;
		}
		set
		{
			_enable = value;
		}
	}

	[XmlAttribute(DataType = "string", AttributeName = "policyFile")]
	public string PolicyFile
	{
		get
		{
			return _policyFile;
		}
		set
		{
			_policyFile = value;
		}
	}

	public PolicyServerSettings()
	{
		_enable = false;
		_policyFile = "clientaccesspolicy.xml";
	}
}
