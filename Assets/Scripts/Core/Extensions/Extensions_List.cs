using UnityEngine;
using System.Collections.Generic;

public static partial class Extensions
{
	/// <summary>
	/// Check if list is null or list has a size of 0
	/// </summary>
	public static bool IsNullOrEmpty<T>(this List<T> list) => (list == null || list.Count == 0);
	
	/// <summary>
	/// Returns 0 if list is null
	/// </summary>
	public static int Count<T>(this List<T> list) => (list == null)? 0: list.Count;
	
	/// <summary>
	/// Returns default if index is out of range
	/// </summary>
	public static T GetElement<T>(this List<T> list, int index)
	{
		if(list == null)
			return default;
		
		else if(index >= 0 && index < list.Count)
			return list[index];
		
		else return default;
	}
	
	/// <summary>
	/// Get random element from list
	/// </summary>
	public static T GetRandom<T>(this List<T> list) => list.GetElement(Random.Range(0, list.Count));
	
	/// <summary>
	/// Get random element from list with index
	/// </summary>
	public static T GetRandom<T>(this List<T> list, out int index)
	{
		index = Random.Range(0, list.Count);
		return list.GetElement(index);
	}
}