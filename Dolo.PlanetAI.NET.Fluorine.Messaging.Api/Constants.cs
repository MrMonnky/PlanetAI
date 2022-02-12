namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Api;

internal class Constants
{
	public const byte TypeUnknown = 0;

	public const byte TypeChunkSize = 1;

	public const byte TypeBytesRead = 3;

	public const byte TypePing = 4;

	public const byte TypeServerBandwidth = 5;

	public const byte TypeClientBandwidth = 6;

	public const byte TypeFlexStreamEnd = 15;

	public const byte TypeFlexSharedObject = 16;

	public const byte TypeFlexInvoke = 17;

	public const byte TypeNotify = 18;

	public const byte TypeStreamMetadata = 18;

	public const byte TypeSharedObject = 19;

	public const byte TypeInvoke = 20;

	public const string TransientPrefix = "_transient";

	public const string SharedObjectService = "SharedObjectService";

	public const string SharedObjectSecurityService = "SharedObjectSecurityService";

	public const string ProviderService = "ProviderService";

	public const string StreamSecurityService = "StreamSecurityService";

	public const string StreamableFileFactory = "StreamableFileFactory";

	public const string StreamFilenameGenerator = "StreamFilenameGenerator";

	public const int AudioChannel = 0;

	public const int VideoChannel = 1;

	public const int DataChannel = 2;

	public const int OverallChannel = 3;

	public const int MaxChannelConfigCount = 4;

	public const string BroadcastScopeType = "bs";

	public const string BroadcastScopeStreamAttribute = "_transient_publishing_stream";

	public const string StreamServiceType = "streamService";

	public const string StreamAttribute = "_transient_publishing_stream";

	public const string ClientStreamModeRead = "read";

	public const string ClientStreamModeRecord = "record";

	public const string ClientStreamModeAppend = "append";

	public const string ClientStreamModeLive = "live";
}
