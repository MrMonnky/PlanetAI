using System.IO;
using Dolo.PlanetAI.NET.Fluorine.IO;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Endpoints;

internal class AMFContext
{
	public const string AMFCoreContextKey = "__@amfcorecontext";

	private AMFMessage _amfMessage;

	private MessageOutput _messageOutput;

	private Stream _inputStream;

	private Stream _outputStream;

	public AMFMessage AMFMessage
	{
		get
		{
			return _amfMessage;
		}
		set
		{
			_amfMessage = value;
		}
	}

	public MessageOutput MessageOutput
	{
		get
		{
			return _messageOutput;
		}
		set
		{
			_messageOutput = value;
		}
	}

	public Stream InputStream => _inputStream;

	public Stream OutputStream => _outputStream;

	public AMFContext(Stream inputStream, Stream outputStream)
	{
		_inputStream = inputStream;
		_outputStream = outputStream;
	}
}
