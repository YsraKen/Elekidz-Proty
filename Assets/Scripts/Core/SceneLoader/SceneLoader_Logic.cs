using System.Collections;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public static partial class SceneLoader
{
	static void Invoke
	(
		Func<AsyncOperation> asyncCall,
		float startDelay,
		SceneLoadProgress prefab,
		Action<bool> onLoadStage,
		Action<float> onProgress,
		Action<bool> onActivationStage,
		float exitDelay
	){
		if(!prefab)
			prefab = Resources.Load<SceneLoadProgress>("sceneLoadProgress");
	
		var visualizer  = Object.Instantiate(prefab);
		
			visualizer.gameObject.SetActive(true);
			visualizer.StartCoroutine(r());
		
		IEnumerator r()
		{
			var operation = asyncCall.Invoke();
			float loadStageMaxValue = 0.9f;
			
			onLoadStage?.Invoke(true);
			visualizer.BeginLoadStage();
			{
				operation.allowSceneActivation = false;
				
				while(operation.progress < loadStageMaxValue)
				{
					float progress = Mathf.Clamp01(operation.progress / loadStageMaxValue);
					
					onProgress?.Invoke(progress);
					visualizer.UpdateProgressBar(progress);
					
					yield return null;
				}
			}
			visualizer.EndLoadStage();
			onLoadStage?.Invoke(false);
			
			onActivationStage?.Invoke(true);
			visualizer.BeginActivationStage();
			{
				yield return new WaitForSecondsRealtime(exitDelay);
				operation.allowSceneActivation = true;
				
				yield return new WaitUntil(() => operation.isDone);
			}
			visualizer.EndActivationStage();
			onActivationStage?.Invoke(false);
			
			yield return null;
			
			Object.Destroy(visualizer.gameObject);
		}
	}
}