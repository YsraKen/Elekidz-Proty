using UnityEngine;
using System.Collections;

public class AppEntry : MonoBehaviour
{
	[SerializeField, SceneSelect] string _targetScene = "Home";
	[SerializeField] float _moveToNextSceneDelay = 1f;
	
	IEnumerator Start()
	{
		yield return new WaitForSecondsRealtime(_moveToNextSceneDelay);
		
		_targetScene.LoadScene();
	}
}