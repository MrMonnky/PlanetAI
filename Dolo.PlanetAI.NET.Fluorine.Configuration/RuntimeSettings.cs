using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RuntimeSettings
{
	private bool _asyncHandler;

	private int _minWorkerThreads;

	private int _maxWorkerThreads;

	[XmlAttribute(DataType = "int", AttributeName = "minWorkerThreads")]
	public int MinWorkerThreads
	{
		get
		{
			return _minWorkerThreads;
		}
		set
		{
			_minWorkerThreads = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "maxWorkerThreads")]
	public int MaxWorkerThreads
	{
		get
		{
			return _maxWorkerThreads;
		}
		set
		{
			_maxWorkerThreads = value;
		}
	}

	[XmlAttribute(DataType = "boolean", AttributeName = "asyncHandler")]
	public bool AsyncHandler
	{
		get
		{
			return _asyncHandler;
		}
		set
		{
			_asyncHandler = value;
		}
	}

	public RuntimeSettings()
	{
		_asyncHandler = false;
		_minWorkerThreads = 0;
		_maxWorkerThreads = 0;
	}
}
