using UnityEngine;
using System;

public class Singleton<T> : Initializable where T : Singleton<T>
{
	public static T Instance { get; private set; }
		
	public override void Initialize()
	{
		var instance = this as T;
		
		if(!Instance)
			Instance = instance;
		
		else if(Instance != instance)
			Debug.LogError("There are more than one instances of " + typeof(T).ToString(), instance);
	}
}