using System.Collections;
using UnityEngine;
using System;

public class WaitForSecSkippable
{
	public float duration { get; set; }
	public bool realtime { get; set; }
	public Func<bool> predicate { get; set; }
	public Action onSkip { get; set; }
	
	public WaitForSecSkippable(float duration) => this.duration = duration;
	
	public IEnumerator Invoke()
	{
		if(predicate == null)
			predicate = () => Input.GetButtonDown("Cancel");
		
		float timer = 0f;
		
		while(timer < duration)
		{
			if(predicate.Invoke())
			{
				onSkip?.Invoke();
				yield break;
			}
		
			timer += realtime? Time.unscaledDeltaTime: Time.deltaTime;
			yield return null;
		}
	}
}