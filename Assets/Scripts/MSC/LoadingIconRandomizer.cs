using UnityEngine;

public class LoadingIconRandomizer : MonoBehaviour
{
	[SerializeField] GameObject[] _icons;
	
	void Start()
	{
		foreach(var icon in _icons)
			icon.SetActive(false);
		
		_icons.GetRandom()?.SetActive(true);
	}
}