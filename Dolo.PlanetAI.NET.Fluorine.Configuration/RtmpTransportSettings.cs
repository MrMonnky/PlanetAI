using System.Xml.Serialization;

namespace Dolo.PlanetAI.NET.Fluorine.Configuration;

internal sealed class RtmpTransportSettings
{
	private int _receiveBufferSize;

	private int _sendBufferSize;

	private bool _tcpNoDelay;

	[XmlAttribute(DataType = "int", AttributeName = "receiveBufferSize")]
	public int ReceiveBufferSize
	{
		get
		{
			return _receiveBufferSize;
		}
		set
		{
			_receiveBufferSize = value;
		}
	}

	[XmlAttribute(DataType = "int", AttributeName = "sendBufferSize")]
	public int SendBufferSize
	{
		get
		{
			return _sendBufferSize;
		}
		set
		{
			_sendBufferSize = value;
		}
	}

	[XmlAttribute(DataType = "boolean", AttributeName = "tcpNoDelay")]
	public bool TcpNoDelay
	{
		get
		{
			return _tcpNoDelay;
		}
		set
		{
			_tcpNoDelay = value;
		}
	}

	public RtmpTransportSettings()
	{
		_receiveBufferSize = 4096;
		_sendBufferSize = 4096;
		_tcpNoDelay = true;
	}
}
