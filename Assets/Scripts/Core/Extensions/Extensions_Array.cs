using UnityEngine;
using System;
using Random = UnityEngine.Random;

public static partial class Extensions
{
	/// <summary>
	/// Check if array is null or array has a size of 0
	/// </summary>
	public static bool IsNullOrEmpty(this Array array) => (array == null || array.Length == 0);
	
	/// <summary>
	/// Returns 0 if array is null
	/// </summary>
	public static int Length(this Array array) => (array == null)? 0: array.Length;
	
	/// <summary>
	/// Returns default if index is out of range
	/// </summary>
	public static T GetElement<T>(this T[] array, int index)
	{
		if(array == null)
			return default;
		
		else if(index >= 0 && index < array.Length)
			return array[index];
		
		else return default;
	}
	
	/// <summary>
	/// Get random element from array
	/// </summary>
	public static T GetRandom<T>(this T[] array) => array.GetElement(Random.Range(0, array.Length));
	
	/// <summary>
	/// Get random element from array with index
	/// </summary>
	public static T GetRandom<T>(this T[] array, out int index)
	{
		index = Random.Range(0, array.Length);
		return array.GetElement(index);
	}
}