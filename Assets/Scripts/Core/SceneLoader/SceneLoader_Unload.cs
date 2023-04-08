using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static partial class SceneLoader
{
	public static void Unload
	(
		string sceneName,
		bool unloadAllEmbeddedSceneObjects = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onUnloadStage = null,
		Action<float> onProgress = null,
		Action<bool> onDeactivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		Unload(()=>
			SceneManager.UnloadSceneAsync(sceneName, GetOption(unloadAllEmbeddedSceneObjects)),
			startDelay, visualizerPrefab, onUnloadStage, onProgress, onDeactivationStage, onFinish, exitDelay
		);
	
	public static void Unload
	(
		int sceneIndex,
		bool unloadAllEmbeddedSceneObjects = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onUnloadStage = null,
		Action<float> onProgress = null,
		Action<bool> onDeactivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		Unload(()=>
			SceneManager.UnloadSceneAsync(sceneIndex, GetOption(unloadAllEmbeddedSceneObjects)),
			startDelay, visualizerPrefab, onUnloadStage, onProgress, onDeactivationStage, onFinish, exitDelay
		);
	
	static UnloadSceneOptions GetOption(bool unloadAll) => unloadAll? UnloadSceneOptions.UnloadAllEmbeddedSceneObjects: default;
	
	static void Unload
	(
		Func<AsyncOperation> unloadCall,
		float startDelay,
		SceneLoadProgress prefab,
		Action<bool> onUnloadStage,
		Action<float> onProgress,
		Action<bool> onDeactivationStage,
		Action<Scene> onFinish,
		float exitDelay
	){
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		
		Invoke(unloadCall, startDelay, prefab, onUnloadStage, onProgress, onDeactivationStage, exitDelay);
		
		void OnSceneUnloaded(Scene scene){
			onFinish?.Invoke(scene);
			SceneManager.sceneUnloaded -= OnSceneUnloaded;
		}
	}
}