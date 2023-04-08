using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RuntimeInitializerSceneObject : MonoBehaviour
{
	[FormerlySerializedAs("_assetInstance")] public RuntimeInitializer assetInstance;
	static RuntimeInitializerSceneObject _sceneInstance;
	
	void Awake()
	{
		if(!_sceneInstance)
		{
			_sceneInstance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		
		#if !UNITY_EDITOR
			assetInstance?.InitializeAll();
		#endif
	}
	
	#if UNITY_EDITOR
	public RuntimeInitializer GetAssetInstance() => assetInstance;
	#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(RuntimeInitializerSceneObject))]
public class RISO_Editor : Editor
{
	RuntimeInitializerSceneObject _script;
	bool _foldout;
	
	void OnEnable() => _script = target as RuntimeInitializerSceneObject;
	
	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical("box");
		{
			GUILayout.BeginHorizontal();
			{
				_foldout = EditorGUILayout.Foldout(_foldout, "Instance", true);
				_script.assetInstance = EditorGUILayout.ObjectField(_script.assetInstance, typeof(RuntimeInitializer), true) as RuntimeInitializer;
			}
			GUILayout.EndHorizontal();
			
			var asset = _script.GetAssetInstance();
			var editor  = CreateEditor(asset);
			if(!editor) return;
			
			if(_foldout)
			{
				GUILayout.BeginVertical("box");
				EditorGUI.indentLevel ++;
				
					editor.OnInspectorGUI();
				
				EditorGUI.indentLevel --;
				GUILayout.EndVertical();
			}
			
			if(GUI.changed)
				EditorUtility.SetDirty(asset);
		}
		GUILayout.EndVertical();
	}
}
#endif