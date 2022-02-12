using System.Text.RegularExpressions;
using Dolo.PlanetAI.NET.Fluorine.Exceptions;

namespace Dolo.PlanetAI.NET.Fluorine.Messaging.Services.Messaging;

internal class Subtopic
{
	public const string SubtopicWildcard = "*";

	public const string SubtopicSeparator = ".";

	private string _subtopic;

	private string[] _subtopicItems;

	private const string SubtopicCheckExpression = "^([\\w][\\w\\-]*)(\\.(([\\w][\\w\\-]*)|\\*))*$|^\\*$";

	private static Regex _regex;

	internal string[] SubtopicItems => _subtopicItems;

	public bool IsHierarchical => _subtopicItems != null && _subtopicItems.Length > 1;

	public string Separator => ".";

	public string Value => _subtopic;

	static Subtopic()
	{
		_regex = new Regex("^([\\w][\\w\\-]*)(\\.(([\\w][\\w\\-]*)|\\*))*$|^\\*$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
	}

	public Subtopic(string subtopic)
		: this(subtopic, ".")
	{
	}

	private Subtopic(string subtopic, string separator)
	{
		if (subtopic == null || subtopic.Length == 0)
		{
			throw new AMFException(__Res.GetString("Subtopic_Invalid", string.Empty));
		}
		if (!_regex.IsMatch(subtopic))
		{
			throw new AMFException(__Res.GetString("Subtopic_Invalid", subtopic));
		}
		_subtopic = subtopic;
		_subtopicItems = subtopic.Split(new char[1] { separator[0] });
	}

	public bool Matches(Subtopic subtopic)
	{
		if (Value == subtopic.Value)
		{
			return true;
		}
		string[] subtopicItems = SubtopicItems;
		string[] subtopicItems2 = subtopic.SubtopicItems;
		for (int i = 0; i < subtopicItems.Length; i++)
		{
			string text = subtopicItems[i];
			if (!(text == "*"))
			{
				if (i >= subtopicItems2.Length)
				{
					return true;
				}
				string text2 = subtopicItems2[i];
				if (text != text2)
				{
					return false;
				}
			}
		}
		return true;
	}
}
