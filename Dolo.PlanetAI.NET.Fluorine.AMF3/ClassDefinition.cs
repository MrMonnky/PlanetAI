namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal sealed class ClassDefinition
{
	private string _className;

	private ClassMember[] _members;

	private bool _externalizable;

	private bool _dynamic;

	internal static ClassMember[] EmptyClassMembers = new ClassMember[0];

	public string ClassName => _className;

	public int MemberCount
	{
		get
		{
			if (_members == null)
			{
				return 0;
			}
			return _members.Length;
		}
	}

	public ClassMember[] Members => _members;

	public bool IsExternalizable => _externalizable;

	public bool IsDynamic => _dynamic;

	public bool IsTypedObject => _className != null && _className != string.Empty;

	internal ClassDefinition(string className, ClassMember[] members, bool externalizable, bool dynamic)
	{
		_className = className;
		_members = members;
		_externalizable = externalizable;
		_dynamic = dynamic;
	}
}
