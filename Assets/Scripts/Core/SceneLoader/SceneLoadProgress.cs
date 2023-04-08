using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneLoadProgress : MonoBehaviour
{
	[SerializeField] Image _progressImg;
	[SerializeField] Text _progressTxt;
	[SerializeField] TextMeshProUGUI _progressTmp;
	
	[SerializeField] GameObject[] loadStageObjects, activationStageObjects;
	
	public void UpdateProgressBar(float value)
	{
		value = Mathf.Clamp01(value);
		
		if(_progressImg)
			_progressImg.fillAmount = value;
		
		UpdateProgressText(value);
	}
	
	void UpdateProgressText(float value)
	{
		float rounded = Mathf.Round(value * 100f);
		string text = rounded.ToString();
		
		if(_progressTxt) _progressTxt.text = text;
		if(_progressTmp) _progressTmp.text = text;
	}
	
	public void BeginLoadStage() => ToggleObjects(loadStageObjects, true);
	public void EndLoadStage() => ToggleObjects(loadStageObjects, false);
	
	public void BeginActivationStage() => ToggleObjects(activationStageObjects, true);
	public void EndActivationStage() => ToggleObjects(activationStageObjects, false);
	
	void ToggleObjects(GameObject[] array, bool toggle)
	{
		foreach(var obj in array)
			obj.SetActive(toggle);
	}
}