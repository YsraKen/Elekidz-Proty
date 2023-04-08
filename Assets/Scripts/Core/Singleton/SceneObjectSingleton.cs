using UnityEngine;
using System;

public class SceneObjectSingleton<T> : MonoBehaviour where T : SceneObjectSingleton<T>
{
	static T _instance;
	
	public static T Instance{
		get{
			if(!_instance){
				var instances = FindObjectsOfType<T>(true);
				
				if(instances == null)
					Debug.LogWarning("Can't find an instance for " + typeof(T).ToString());
				
				else if(instances.Length > 1)
					Debug.LogWarning("Multiple instances for " + typeof(T).ToString() + " has found");
				
				else
					_instance = instances[0];
			}
			
			return _instance;
		}
	}
}