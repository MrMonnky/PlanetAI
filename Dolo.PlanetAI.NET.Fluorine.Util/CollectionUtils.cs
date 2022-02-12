using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Dolo.PlanetAI.NET.Fluorine.Util;

internal abstract class CollectionUtils
{
	public static bool IsNullOrEmpty(ICollection collection)
	{
		if (collection != null)
		{
			return collection.Count == 0;
		}
		return true;
	}

	public static IList CreateList(Type listType)
	{
		ValidationUtils.ArgumentNotNull(listType, "listType");
		bool flag = false;
		IList list;
		Type implementingType;
		if (listType.IsArray)
		{
			list = new List<object>();
			flag = true;
		}
		else if (ReflectionUtils.IsSubClass(listType, typeof(ReadOnlyCollection<>), out implementingType))
		{
			Type type = implementingType.GetGenericArguments()[0];
			Type type2 = ReflectionUtils.MakeGenericType(typeof(IEnumerable<>), type);
			bool flag2 = false;
			ConstructorInfo[] constructors = listType.GetConstructors();
			foreach (ConstructorInfo constructorInfo in constructors)
			{
				IList<ParameterInfo> parameters = constructorInfo.GetParameters();
				if (parameters.Count == 1 && type2.IsAssignableFrom(parameters[0].ParameterType))
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				throw new Exception($"Readonly type {listType} does not have a public constructor that takes a type that implements {type2}.");
			}
			list = (IList)CreateGenericList(type);
			flag = true;
		}
		else
		{
			if (!typeof(IList).IsAssignableFrom(listType) || !ReflectionUtils.IsInstantiatableType(listType))
			{
				throw new Exception($"Cannot create and populate list type {listType}.");
			}
			list = (IList)Activator.CreateInstance(listType);
		}
		if (flag)
		{
			if (listType.IsArray)
			{
				list = ((List<object>)list).ToArray();
			}
			else if (ReflectionUtils.IsSubClass(listType, typeof(ReadOnlyCollection<>)))
			{
				list = (IList)Activator.CreateInstance(listType, list);
			}
		}
		return list;
	}

	public static Array CreateArray(Type type, ICollection collection)
	{
		ValidationUtils.ArgumentNotNull(collection, "collection");
		if (collection is Array)
		{
			return collection as Array;
		}
		List<object> list = new List<object>();
		foreach (object item in collection)
		{
			list.Add(item);
		}
		return list.ToArray();
	}

	public static object GetSingleItem(IList list)
	{
		return GetSingleItem(list, returnDefaultIfEmpty: false);
	}

	public static object GetSingleItem(IList list, bool returnDefaultIfEmpty)
	{
		if (list.Count == 1)
		{
			return list[0];
		}
		if (returnDefaultIfEmpty && list.Count == 0)
		{
			return null;
		}
		throw new Exception(string.Format("Expected single item in list but got {1}.", list.Count));
	}

	public static List<T> CreateList<T>(params T[] values)
	{
		return new List<T>(values);
	}

	public static bool IsNullOrEmpty<T>(ICollection<T> collection)
	{
		if (collection != null)
		{
			return collection.Count == 0;
		}
		return true;
	}

	public static bool IsNullOrEmptyOrDefault<T>(IList<T> list)
	{
		if (IsNullOrEmpty(list))
		{
			return true;
		}
		return ReflectionUtils.ItemsUnitializedValue(list);
	}

	public static IList<T> Slice<T>(IList<T> list, int? start, int? end)
	{
		return Slice(list, start, end, null);
	}

	public static IList<T> Slice<T>(IList<T> list, int? start, int? end, int? step)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		if (step == 0)
		{
			throw new ArgumentException("Step cannot be zero.", "step");
		}
		List<T> list2 = new List<T>();
		if (list.Count == 0)
		{
			return list2;
		}
		int num = step ?? 1;
		int valueOrDefault = start.GetValueOrDefault();
		int num2 = end ?? list.Count;
		valueOrDefault = ((valueOrDefault < 0) ? (list.Count + valueOrDefault) : valueOrDefault);
		num2 = ((num2 < 0) ? (list.Count + num2) : num2);
		valueOrDefault = Math.Max(valueOrDefault, 0);
		num2 = Math.Min(num2, list.Count - 1);
		for (int i = valueOrDefault; i < num2; i += num)
		{
			list2.Add(list[i]);
		}
		return list2;
	}

	public static void AddRange<T>(IList<T> initial, IEnumerable<T> collection)
	{
		if (initial == null)
		{
			throw new ArgumentNullException("initial");
		}
		if (collection == null)
		{
			return;
		}
		foreach (T item in collection)
		{
			initial.Add(item);
		}
	}

	public static List<T> Distinct<T>(List<T> collection)
	{
		List<T> list = new List<T>();
		foreach (T item in collection)
		{
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public static List<List<T>> Flatten<T>(params IList<T>[] lists)
	{
		List<List<T>> list = new List<List<T>>();
		Dictionary<int, T> currentSet = new Dictionary<int, T>();
		Recurse(new List<IList<T>>(lists), 0, currentSet, list);
		return list;
	}

	private static void Recurse<T>(IList<IList<T>> global, int current, Dictionary<int, T> currentSet, List<List<T>> flattenedResult)
	{
		IList<T> list = global[current];
		for (int i = 0; i < list.Count; i++)
		{
			currentSet[current] = list[i];
			if (current == global.Count - 1)
			{
				List<T> list2 = new List<T>();
				for (int j = 0; j < currentSet.Count; j++)
				{
					list2.Add(currentSet[j]);
				}
				flattenedResult.Add(list2);
			}
			else
			{
				Recurse(global, current + 1, currentSet, flattenedResult);
			}
		}
	}

	public static List<T> CreateList<T>(ICollection collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("collection");
		}
		T[] array = new T[collection.Count];
		collection.CopyTo(array, 0);
		return new List<T>(array);
	}

	public static bool ListEquals<T>(IList<T> a, IList<T> b)
	{
		if (a == null || b == null)
		{
			return a == null && b == null;
		}
		if (a.Count != b.Count)
		{
			return false;
		}
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int i = 0; i < a.Count; i++)
		{
			if (!@default.Equals(a[i], b[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static IList<T> Minus<T>(IList<T> list, IList<T> minus)
	{
		ValidationUtils.ArgumentNotNull(list, "list");
		List<T> list2 = new List<T>(list.Count);
		foreach (T item in list)
		{
			if (minus == null || !minus.Contains(item))
			{
				list2.Add(item);
			}
		}
		return list2;
	}

	public static object CreateGenericList(Type listType)
	{
		ValidationUtils.ArgumentNotNull(listType, "listType");
		return ReflectionUtils.CreateGeneric(typeof(List<>), listType);
	}

	public static bool IsListType(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "listType");
		if (type.IsArray)
		{
			return true;
		}
		if (typeof(IList).IsAssignableFrom(type))
		{
			return true;
		}
		if (ReflectionUtils.IsSubClass(type, typeof(IList<>)))
		{
			return true;
		}
		return false;
	}
}
