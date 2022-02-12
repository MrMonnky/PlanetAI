using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class ThreadPoolSettings
{
	private int _minWorkerThreads;

	private int _maxWorkerThreads;

	private int _idleTimeout;

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

	[XmlAttribute(DataType = "int", AttributeName = "idleTimeout")]
	public int IdleTimeout
	{
		get
		{
			return _idleTimeout;
		}
		set
		{
			_idleTimeout = value;
		}
	}
}
