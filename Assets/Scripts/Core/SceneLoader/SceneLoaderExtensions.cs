using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SceneLoaderExtensions
{	
	public static void LoadScene
	(
		this int sceneIndex,
		bool isAdditive = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onLoadStage = null,
		Action<float> onProgress = null,
		Action<bool> onActivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		SceneLoader.Load
		(
			sceneIndex,
			isAdditive,
			startDelay,
			visualizerPrefab,
			onLoadStage,
			onProgress,
			onActivationStage,
			onFinish,
			exitDelay
		);
	
	public static void LoadScene
	(
		this string sceneName,
		bool isAdditive = false,
		float startDelay = 0f,
		SceneLoadProgress visualizerPrefab = null,
		Action<bool> onLoadStage = null,
		Action<float> onProgress = null,
		Action<bool> onActivationStage = null,
		Action<Scene> onFinish = null,
		float exitDelay = 0f
	)=>
		SceneLoader.Load
		(
			sceneName,
			isAdditive,
			startDelay,
			visualizerPrefab,
			onLoadStage,
			onProgress,
			onActivationStage,
			onFinish,
			exitDelay
		);
}