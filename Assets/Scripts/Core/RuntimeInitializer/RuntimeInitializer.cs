using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class RuntimeInitializer : ScriptableObject
{
	[SerializeField] List<Initializable> _initializables = new List<Initializable>();
	[SerializeField] bool _autoSearchForInitializables = true;
	
	#if UNITY_EDITOR
	static RuntimeInitializer _instance;
	const string PATH = "Assets/ScriptableObjects/Managers/Runtime Initializer.asset";
	#endif
	
	[RuntimeInitializeOnLoadMethod]
	
	#if UNITY_EDITOR
	[InitializeOnLoadMethod]
	#endif
	
	public static void CallOnLoad()
	{
		#if UNITY_EDITOR
		if(!_instance)
			_instance = (RuntimeInitializer) AssetDatabase.LoadAssetAtPath<RuntimeInitializer>(PATH);
		
		if(_instance)
		{
			if(_instance._autoSearchForInitializables)
				_instance.SearchForInitializables();
			
			_instance?.InitializeAll();
		}
		#endif
	}
	
	#if UNITY_EDITOR
	[ContextMenu("Search For Initializables")]
	void SearchForInitializables()
	{
		if(Application.isPlaying) return;
		
		string[] guids = AssetDatabase.FindAssets("t: Initializable", null);
		
		bool listUpdated = false;
		
		foreach(string guid in guids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			var asset = AssetDatabase.LoadAssetAtPath<Initializable>(path);
			
			if(!_initializables.Contains(asset))
			{
				_initializables.Add(asset);
				
				Debug.Log($"<b>{this}</b> has found <b><color=green>'{asset}'</color></b>", this);
				listUpdated = true;
			}
		}
		
		if(listUpdated)
			EditorUtility.SetDirty(this);
	}
	#endif
	
	public void InitializeAll()
	{
		foreach(var initializable in _initializables)
			initializable?.Initialize();
	}
}