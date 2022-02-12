using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.AMF3;

internal sealed class ClassMember
{
	private string _name;

	private BindingFlags _bindingFlags;

	private MemberTypes _memberType;

	public string Name => _name;

	public BindingFlags BindingFlags => _bindingFlags;

	public MemberTypes MemberType => _memberType;

	internal ClassMember(string name, BindingFlags bindingFlags, MemberTypes memberType)
	{
		_name = name;
		_bindingFlags = bindingFlags;
		_memberType = memberType;
	}
}
