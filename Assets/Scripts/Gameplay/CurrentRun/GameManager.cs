using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;

namespace CurrentRunGame
{
	public class GameManager : SceneObjectSingleton<GameManager>
	{
		[Foldout("Pre-game")]
		[SerializeField] Text _infoText;
		
		[field: SerializeField, Disabled] public bool Highscore{ get; private set; }
		[field: SerializeField, Disabled] public bool Highspeed{ get; private set; }
		
		[SerializeField, Disabled] int _currentHighscore, _currentFastest;
		
		[Foldout("Game")]
		[SerializeField, Range(0,1)] float _minSpeedPacePercent = 0.1f;
		[field: SerializeField, Disabled] public float CurrentSpeedPacePercent { get; private set; } = 1f;
		[SerializeField] TextMeshProUGUI _speedTmp, _instructionTmp;
		
		[Foldout("Scoring")]
		[SerializeField] float _scoreUpdateDuration = 0.5f;
		[field: SerializeField, Space, Disabled] public int CurrentScore { get; private set; }
		// [SerializeField] Text _scoreTxt;
		[SerializeField] TextMeshProUGUI _scoreTmp;
		
		[Foldout("Post-game")]
		[SerializeField] GameObject _gameOverpanel;
		[SerializeField] Text _gameOverTxt;
		
		[Space]
		[SerializeField, SceneSelect] string _exitScene = "Home";
		
		[Foldout("Events")]
		[SerializeField] UnityEvent _onGameStarted, _onGameEnded;
		[SerializeField] UnityEvent<float> _onCurrentSpeedPaceModified;
		
		public bool IsPlaying { get; private set; }
		
		Player _player;
		
		IEnumerator Start()
		{
			_player = Player.Instance;
			
			_scoreTmp.gameObject.SetActive(false);
			_speedTmp.gameObject.SetActive(false);
			
			_gameOverpanel.SetActive(false);
			
			if(PlayerPrefs.HasKey("Score"))
			{
				_currentHighscore = PlayerPrefs.GetInt("Score");
				_currentFastest = PlayerPrefs.GetInt("Speed");
				
				_infoText.text = $"Highscore: {_currentHighscore}\nFastest speed: {_currentFastest}%";
			}
			else
				_infoText.transform.parent.gameObject.SetActive(false);
			
			var step = new WaitForSeconds(_scoreUpdateDuration);
			
			while(true)
			{
				yield return step;
				
				if(IsPlaying)
					SetScore(CurrentScore + 1);
			}
		}
		
		void LateUpdate()
		{
			if(!IsPlaying) return;
			
			float speedPercent = Mathf.Round(CurrentSpeedPacePercent * 100);
			_speedTmp.text = $"Speed: {speedPercent}%";
		}
		
		public void SetScore(int value)
		{
			CurrentScore = value;
			_scoreTmp.text = $"Score: {value}";
		}
		
		public void StartRunner()
		{
			_player.StartRunning();
			IsPlaying = true;
			
			_scoreTmp.gameObject.SetActive(true);
			_speedTmp.gameObject.SetActive(true);
			
			_instructionTmp.text = Application.isMobilePlatform?
				"Tap the screen to jump":
				"Press 'space' to jump";
			
			_onGameStarted.Invoke();
		}
		
		public void ModifyCurrentSpeed(float amount)
		{
			CurrentSpeedPacePercent += amount;
			CurrentSpeedPacePercent = Mathf.Clamp(CurrentSpeedPacePercent, _minSpeedPacePercent, float.MaxValue);
			
			_onCurrentSpeedPaceModified.Invoke(CurrentSpeedPacePercent);
		}
		
		public void GameOver(string reason = "")
		{
			IsPlaying = false;
			
			_player.StopRunning();
			
			int speedPercent = Mathf.RoundToInt(CurrentSpeedPacePercent * 100);
			
			Highscore = CurrentScore > _currentHighscore;
			Highspeed = speedPercent > _currentFastest;
			
			if(Highscore) PlayerPrefs.SetInt("Score", CurrentScore);
			if(Highspeed) PlayerPrefs.SetInt("Speed", speedPercent);
			
			_scoreTmp.gameObject.SetActive(false);
			_speedTmp.gameObject.SetActive(false);
			
			Debug.LogWarning("GAME OVER: " + reason, this);
			_gameOverTxt.text = reason;
			
			_gameOverpanel.SetActive(true);
			
			_onGameEnded.Invoke();
		}
		
		public void Reload() => SceneLoader.Current();
		public void Exit() => _exitScene.LoadScene();
	}
}

