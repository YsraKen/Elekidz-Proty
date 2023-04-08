using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
	public Action[] actions = new Action[]{ new Action(){ name = "On Click" }};
	
	public void OnClick()
	{
		StopAllCoroutines();
		StartCoroutine(r());
		
		IEnumerator r()
		{
			foreach(var action in actions)
				yield return action.Invoke();
		}
	}
	
	[System.Serializable]
	public class Action
	{
		public string name;
		public float delay;
		public bool realtime;
		
		[Space]
		public UnityEvent call;
		
		public IEnumerator Invoke()
		{
			if(realtime) yield return new WaitForSecondsRealtime(delay);
			else yield return new WaitForSeconds(delay);
			
			call.Invoke();
		}
	}
}