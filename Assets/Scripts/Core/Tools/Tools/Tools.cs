using UnityEngine;
using System;
using System.Collections;

public static partial class Tools
{
	public static IEnumerator WaitForSecSkippable
	(
		float duration,
		bool realtime = false,
		Func<bool> predicate = null,
		Action onSkip = null
	){
		if(predicate == null)
			predicate = ()=> Input.GetButtonDown("Cancel");
		
		float timer = 0f;
		
		while(timer < duration)
		{
			yield return null;
			
			if(realtime) timer += Time.unscaledDeltaTime;
			else timer += Time.deltaTime;
			
			if(predicate.Invoke())
			{
				onSkip?.Invoke();
				yield break;
			}
		}
	}
}