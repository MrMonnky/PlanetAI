namespace Dolo.PlanetAI.NET.Fluorine.IO;

internal class AMFHeader
{
	public const string CredentialsHeader = "Credentials";

	public const string ServiceBrowserHeader = "DescribeService";

	public const string ClearedCredentials = "ClearedCredentials";

	public const string CredentialsIdHeader = "CredentialsId";

	public const string RequestPersistentHeader = "RequestPersistentHeader";

	private object _content;

	private bool _mustUnderstand;

	private string _name;

	public string Name => _name;

	public object Content => _content;

	public bool MustUnderstand => _mustUnderstand;

	public bool IsClearedCredentials
	{
		get
		{
			if (_content is string)
			{
				return (string)_content == "ClearedCredentials";
			}
			return false;
		}
	}

	public AMFHeader(string name, bool mustUnderstand, object content)
	{
		_name = name;
		_mustUnderstand = mustUnderstand;
		_content = content;
	}
}
