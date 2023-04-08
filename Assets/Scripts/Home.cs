using UnityEngine;

public class Home : MonoBehaviour
{
	[SerializeField, SceneSelect] string[] scenes;
	
	public void LoadScene(int index) => scenes.GetElement(index)?.LoadScene();
	
	public void Quit() => Application.Quit();
}