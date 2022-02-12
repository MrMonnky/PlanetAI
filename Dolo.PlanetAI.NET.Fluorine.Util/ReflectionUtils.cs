using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal abstract class ReflectionUtils
{
	private static Type GenericIListType = Type.GetType("System.Collections.Generic.IList`1");

	private static Type GenericICollectionType = Type.GetType("System.Collections.Generic.ICollection`1");

	private static Type GenericIDictionaryType = Type.GetType("System.Collections.Generic.IDictionary`2");

	public static Type GetObjectType(object v)
	{
		return v?.GetType();
	}

	public static bool IsInstantiatableType(Type t)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		if (t.IsAbstract || t.IsInterface || t.IsArray || IsGenericTypeDefinition(t) || t == typeof(void))
		{
			return false;
		}
		if (!HasDefaultConstructor(t))
		{
			return false;
		}
		return true;
	}

	public static bool HasDefaultConstructor(Type t)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		if (t.IsValueType)
		{
			return true;
		}
		return t.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null) != null;
	}

	public static bool IsNullable(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		if (type.IsValueType)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
		return false;
	}

	public static bool IsUnitializedValue(object value)
	{
		if (value == null)
		{
			return true;
		}
		object obj = CreateUnitializedValue(value.GetType());
		return value.Equals(obj);
	}

	public static object CreateUnitializedValue(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		if (IsGenericTypeDefinition(type))
		{
			throw new ArgumentException($"Type {type} is a generic type definition and cannot be instantiated.", "type");
		}
		if (type.IsClass || type.IsInterface || type == typeof(void))
		{
			return null;
		}
		if (type.IsValueType)
		{
			return Activator.CreateInstance(type);
		}
		throw new ArgumentException($"Type {type} cannot be instantiated.", "type");
	}

	public static bool IsPropertyIndexed(PropertyInfo property)
	{
		ValidationUtils.ArgumentNotNull(property, "property");
		return !CollectionUtils.IsNullOrEmpty((ICollection<ParameterInfo>)property.GetIndexParameters());
	}

	public static bool ImplementsInterface(Type type, string interfaceName)
	{
		return type.GetInterface(interfaceName, ignoreCase: true) != null;
	}

	public static bool IsSubClass(Type type, Type check)
	{
		Type implementingType;
		return IsSubClass(type, check, out implementingType);
	}

	public static bool IsSubClass(Type type, Type check, out Type implementingType)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		ValidationUtils.ArgumentNotNull(check, "check");
		return IsSubClassInternal(type, type, check, out implementingType);
	}

	private static bool IsSubClassInternal(Type initialType, Type currentType, Type check, out Type implementingType)
	{
		if (currentType == check)
		{
			implementingType = currentType;
			return true;
		}
		if (check.IsInterface && (initialType.IsInterface || currentType == initialType))
		{
			Type[] interfaces = currentType.GetInterfaces();
			foreach (Type currentType2 in interfaces)
			{
				if (IsSubClassInternal(initialType, currentType2, check, out implementingType))
				{
					if (check == implementingType)
					{
						implementingType = currentType;
					}
					return true;
				}
			}
		}
		if (IsGenericType(currentType) && !IsGenericTypeDefinition(currentType) && IsSubClassInternal(initialType, GetGenericTypeDefinition(currentType), check, out implementingType))
		{
			implementingType = currentType;
			return true;
		}
		if (currentType.BaseType == null)
		{
			implementingType = null;
			return false;
		}
		return IsSubClassInternal(initialType, currentType.BaseType, check, out implementingType);
	}

	public static bool IsGenericTypeDefinition(Type type)
	{
		return type.IsGenericTypeDefinition;
	}

	public static bool IsGenericType(Type type)
	{
		return type.IsGenericType;
	}

	public static Type GetGenericTypeDefinition(Type type)
	{
		return type.GetGenericTypeDefinition();
	}

	public static Type[] GetGenericArguments(Type type)
	{
		return type.GetGenericArguments();
	}

	internal static Type MakeGenericType(Type genericTypeDefinition, params Type[] typeArguments)
	{
		ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
		ValidationUtils.ArgumentNotNullOrEmpty((ICollection<Type>)typeArguments, "typeArguments");
		ValidationUtils.ArgumentConditionTrue(IsGenericTypeDefinition(genericTypeDefinition), "genericTypeDefinition", $"Type {genericTypeDefinition} is not a generic type definition.");
		return genericTypeDefinition.MakeGenericType(typeArguments);
	}

	public static Type GetListItemType(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		if (type.IsArray)
		{
			return type.GetElementType();
		}
		if (IsSubClass(type, GenericIListType, out var implementingType))
		{
			if (IsGenericTypeDefinition(implementingType))
			{
				throw new Exception($"Type {type} is not a list.");
			}
			return GetGenericArguments(implementingType)[0];
		}
		if (typeof(IList).IsAssignableFrom(type))
		{
			return null;
		}
		throw new Exception($"Type {type} is not a list.");
	}

	public static Type GetDictionaryValueType(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		if (IsSubClass(type, GenericIDictionaryType, out var implementingType))
		{
			if (IsGenericTypeDefinition(implementingType))
			{
				throw new Exception($"Type {type} is not a dictionary.");
			}
			return GetGenericArguments(implementingType)[1];
		}
		if (typeof(IDictionary).IsAssignableFrom(type))
		{
			return null;
		}
		throw new Exception($"Type {type} is not a dictionary.");
	}

	public static bool ItemsUnitializedValue<T>(IList<T> list)
	{
		ValidationUtils.ArgumentNotNull(list, "list");
		Type listItemType = GetListItemType(list.GetType());
		if (listItemType.IsValueType)
		{
			object obj = CreateUnitializedValue(listItemType);
			for (int i = 0; i < list.Count; i++)
			{
				if (!list[i].Equals(obj))
				{
					return false;
				}
			}
		}
		else
		{
			if (!listItemType.IsClass)
			{
				throw new Exception($"Type {listItemType} is neither a ValueType or a Class.");
			}
			for (int j = 0; j < list.Count; j++)
			{
				object obj2 = list[j];
				if (obj2 != null)
				{
					return false;
				}
			}
		}
		return true;
	}

	public static Type GetMemberUnderlyingType(MemberInfo member)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		return member.MemberType switch
		{
			MemberTypes.Field => ((FieldInfo)member).FieldType, 
			MemberTypes.Property => ((PropertyInfo)member).PropertyType, 
			MemberTypes.Event => ((EventInfo)member).EventHandlerType, 
			_ => throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo", "member"), 
		};
	}

	public static bool IsIndexedProperty(MemberInfo member)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		PropertyInfo propertyInfo = member as PropertyInfo;
		if (propertyInfo != null)
		{
			return IsIndexedProperty(propertyInfo);
		}
		return false;
	}

	public static bool IsIndexedProperty(PropertyInfo property)
	{
		ValidationUtils.ArgumentNotNull(property, "property");
		return property.GetIndexParameters().Length != 0;
	}

	public static MemberInfo GetMember(Type type, string name, MemberTypes memberTypes)
	{
		return GetMember(type, name, memberTypes, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
	}

	public static MemberInfo GetMember(Type type, string name, MemberTypes memberTypes, BindingFlags bindingAttr)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		ValidationUtils.ArgumentNotNull(name, "name");
		MemberInfo[] member = type.GetMember(name, memberTypes, bindingAttr);
		return CollectionUtils.GetSingleItem(member) as MemberInfo;
	}

	public static object GetMemberValue(MemberInfo member, object target)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		ValidationUtils.ArgumentNotNull(target, "target");
		switch (member.MemberType)
		{
		case MemberTypes.Field:
			return ((FieldInfo)member).GetValue(target);
		case MemberTypes.Property:
			try
			{
				return ((PropertyInfo)member).GetValue(target, null);
			}
			catch (TargetParameterCountException innerException)
			{
				throw new ArgumentException($"MemberInfo '{member.Name}' has index parameters", innerException);
			}
		default:
			throw new ArgumentException($"MemberInfo '{member.Name}' is not of type FieldInfo or PropertyInfo", "member");
		}
	}

	public static void SetMemberValue(MemberInfo member, object target, object value)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		ValidationUtils.ArgumentNotNull(target, "target");
		switch (member.MemberType)
		{
		case MemberTypes.Field:
			((FieldInfo)member).SetValue(target, value);
			break;
		case MemberTypes.Property:
			((PropertyInfo)member).SetValue(target, value, null);
			break;
		default:
			throw new ArgumentException($"MemberInfo '{member.Name}' must be of type FieldInfo or PropertyInfo", "member");
		}
	}

	public static bool CanReadMemberValue(MemberInfo member)
	{
		return member.MemberType switch
		{
			MemberTypes.Field => true, 
			MemberTypes.Property => ((PropertyInfo)member).CanRead, 
			_ => false, 
		};
	}

	public static bool CanSetMemberValue(MemberInfo member)
	{
		return member.MemberType switch
		{
			MemberTypes.Field => true, 
			MemberTypes.Property => ((PropertyInfo)member).CanWrite, 
			_ => false, 
		};
	}

	public static MemberInfo[] GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
	{
		List<MemberInfo> list = new List<MemberInfo>();
		list.AddRange(type.GetFields(bindingAttr));
		list.AddRange(type.GetProperties(bindingAttr));
		return list.ToArray();
	}

	public static Attribute GetAttribute(Type type, ICustomAttributeProvider attributeProvider)
	{
		return GetAttribute(type, attributeProvider, inherit: true);
	}

	public static Attribute GetAttribute(Type type, ICustomAttributeProvider attributeProvider, bool inherit)
	{
		Attribute[] attributes = GetAttributes(type, attributeProvider, inherit);
		return (attributes != null && attributes.Length != 0) ? attributes[0] : null;
	}

	public static Attribute[] GetAttributes(Type type, ICustomAttributeProvider attributeProvider, bool inherit)
	{
		ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
		return attributeProvider.GetCustomAttributes(type, inherit) as Attribute[];
	}

	public static string GetNameAndAssemblyName(Type t)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		return t.FullName + ", " + t.Assembly.GetName().Name;
	}

	public static MemberInfo[] FindMembers(Type targetType, MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
	{
		ValidationUtils.ArgumentNotNull(targetType, "targetType");
		List<MemberInfo> list = new List<MemberInfo>(targetType.FindMembers(memberType, bindingAttr, filter, filterCriteria));
		if ((memberType & MemberTypes.Field) != 0 && (bindingAttr & BindingFlags.NonPublic) != 0)
		{
			BindingFlags bindingAttr2 = bindingAttr ^ BindingFlags.Public;
			while ((targetType = targetType.BaseType) != null)
			{
				list.AddRange(targetType.FindMembers(MemberTypes.Field, bindingAttr2, filter, filterCriteria));
			}
		}
		return list.ToArray();
	}

	public static object CreateGeneric(Type genericTypeDefinition, Type innerType, params object[] args)
	{
		return CreateGeneric(genericTypeDefinition, new Type[1] { innerType }, args);
	}

	public static object CreateGeneric(Type genericTypeDefinition, IList innerTypes, params object[] args)
	{
		ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
		ValidationUtils.ArgumentNotNullOrEmpty(innerTypes, "innerTypes");
		Type type = MakeGenericType(genericTypeDefinition, CollectionUtils.CreateArray(typeof(Type), innerTypes) as Type[]);
		return Activator.CreateInstance(type, args);
	}

	public static TypeConverter GetTypeConverter(object obj)
	{
		if (obj == null)
		{
			return null;
		}
		return TypeDescriptor.GetConverter(obj);
	}
}
