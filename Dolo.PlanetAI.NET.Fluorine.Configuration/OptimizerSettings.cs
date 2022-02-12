using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class OptimizerSettings
{
	private string _provider;

	private bool _debug;

	private bool _typeCheck;

	[XmlAttribute(DataType = "string", AttributeName = "provider")]
	public string Provider
	{
		get
		{
			return _provider;
		}
		set
		{
			_provider = value;
		}
	}

	[XmlAttribute(DataType = "boolean", AttributeName = "debug")]
	public bool Debug
	{
		get
		{
			return _debug;
		}
		set
		{
			_debug = value;
		}
	}

	[XmlAttribute(DataType = "boolean", AttributeName = "typeCheck")]
	public bool TypeCheck
	{
		get
		{
			return _typeCheck;
		}
		set
		{
			_typeCheck = value;
		}
	}

	public OptimizerSettings()
	{
		_provider = "codedom";
		_debug = true;
		_typeCheck = false;
	}
}
