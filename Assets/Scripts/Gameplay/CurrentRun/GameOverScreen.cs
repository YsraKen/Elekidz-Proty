using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CurrentRunGame
{
	public class GameOverScreen : MonoBehaviour
	{
		[SerializeField] GameObject _message;
		[SerializeField] Text _scoreTxt;
		
		[Space]
		[SerializeField] GameObject _confirmationPanel;
		[SerializeField] float _delay = 2f;
		
		void OnEnable() => StartCoroutine(Animate());
		
		IEnumerator Animate()
		{
			_message.SetActive(true);
			
			var gMgr = GameManager.Instance;
			int score = gMgr.CurrentScore;
			
			string newRecord = gMgr.Highscore? "\nNEW RECORD": "";
			// string newSpeedRecord = gMgr.Highspeed? "\nNEW SPEED RECORD": "";
			
				// _scoreTxt.text = $"Score: {score}{newRecord}{newSpeedRecord}";
				_scoreTxt.text = $"Score: {score}{newRecord}";
				_scoreTxt.gameObject.SetActive(true);
			
			_confirmationPanel.SetActive(false);
			yield return Tools.WaitForSecSkippable(_delay);
			
			_confirmationPanel.SetActive(true);
			_message.SetActive(false);
		}
	}
}