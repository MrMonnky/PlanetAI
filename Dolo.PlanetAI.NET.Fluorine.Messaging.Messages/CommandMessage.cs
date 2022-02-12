namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Messages;

internal class CommandMessage : AsyncMessage
{
	public const string AuthenticationMessageRefType = "flex.messaging.messages.AuthenticationMessage";

	public const int FlexPingOperation = 5;

	public const int RoyalePingOperation = 13;

	public const int SubscribeOperation = 0;

	public const int UnsubscribeOperation = 1;

	public const int UnknownOperation = 10000;

	public const int PollOperation = 2;

	public const int ClientSyncOperation = 4;

	public const int ClusterRequestOperation = 7;

	public const int SessionInvalidateOperation = 10;

	public static string SelectorHeader = "DSSelector";

	public static string SessionInvalidatedHeader = "DSSessionInvalidated";

	public static string AMFMessageClientTimeoutHeader = "AMFMessageClientTimeout";

	internal static string AMFSuppressPollWaitHeader = "AMFSuppressPollWait";

	private string _messageRefType;

	private int _operation;

	public string messageRefType
	{
		get
		{
			return _messageRefType;
		}
		set
		{
			_messageRefType = value;
		}
	}

	public int operation
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

	public CommandMessage()
	{
		_operation = 10000;
	}

	public CommandMessage(int operation)
	{
		_operation = operation;
	}
}
