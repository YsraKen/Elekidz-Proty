using UnityEngine;

public class SceneSelectAttribute : PropertyAttribute
{
	public bool isTextField{ get; set; }
	
	/* 
		Description:
			Makes a Scene Selection field.
		
		How to use:
			Apply the SceneSelect attribute to an Integer or a String field.
		
		Example:
			[SceneSelect] public int sceneIndex;
			[SceneSelect] public string sceneName;
			
			public void LoadSceneByIndex()
			{
				SceneManager.LoadScene(sceneIndex);
			}
			
			public void LoadSceneByName()
			{
				SceneManager.LoadScene(sceneName);
			}
		
		Other
			Right click to refresh options
	*/
}