using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static partial class SceneLoader
{
	// Load by Name
	public static void Load
	(
		string sceneName,
		bool isAdditive = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onLoadStage = null,
		Action<float> onProgress = null,
		Action<bool> onActivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		Load(()=> SceneManager.LoadSceneAsync(sceneName, GetMode(isAdditive)), startDelay, visualizerPrefab, onLoadStage, onProgress, onActivationStage, onFinish, exitDelay);
	
	// Load by Index
	public static void Load
	(
		int sceneIndex,
		bool isAdditive = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onLoadStage = null,
		Action<float> onProgress = null,
		Action<bool> onActivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		Load(()=> SceneManager.LoadSceneAsync(sceneIndex, GetMode(isAdditive)), startDelay, visualizerPrefab, onLoadStage, onProgress, onActivationStage, onFinish, exitDelay);
	
	static LoadSceneMode GetMode(bool isAdditive) => isAdditive? LoadSceneMode.Additive: default;
	
	static void Load
	(
		Func<AsyncOperation> loadCall,
		float startDelay,
		SceneLoadProgress prefab,
		Action<bool> onLoadStage,
		Action<float> onProgress,
		Action<bool> onActivationStage,
		Action<Scene> onFinish,
		float exitDelay
	){
		SceneManager.sceneLoaded += OnSceneLoaded;
		
		Invoke(loadCall, startDelay, prefab, onLoadStage, onProgress, onActivationStage, exitDelay);
		
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			onFinish?.Invoke(scene);
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}
	
	public static void Current
	(
		bool isAdditive = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onLoadStage = null,
		Action<float> onProgress = null,
		Action<bool> onActivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	){
		int index = SceneManager.GetActiveScene().buildIndex;
		Load(index, isAdditive, startDelay, visualizerPrefab, onLoadStage, onProgress, onActivationStage, onFinish, exitDelay);
	}
}