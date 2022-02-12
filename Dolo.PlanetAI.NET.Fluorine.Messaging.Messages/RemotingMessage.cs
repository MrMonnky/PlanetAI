namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class RemotingMessage : MessageBase
{
	private string _source;

	private string _operation;

	public string source
	{
		get
		{
			return _source;
		}
		set
		{
			_source = value;
		}
	}

	public string operation
	{
		get
		{
			return _operation;
		}
		set
		{
			_operation = value;
		}
	}
}
