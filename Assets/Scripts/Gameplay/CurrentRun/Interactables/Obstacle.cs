using UnityEngine;

namespace CurrentRunGame
{
	public class Obstacle : Interactable
	{
		[SerializeField] Vector2 _blockingStrengthMinMax = new Vector2(1.5f, 2.5f);
		[SerializeField] int _bonusScore = 3;
		
		[ContextMenu("Interact")]
		public override void Interact()
		{
			if(!gameManager.IsPlaying) return;
			
			float blockingStrength = Random.Range(_blockingStrengthMinMax.x, _blockingStrengthMinMax.y);
			
			if(gameManager.CurrentSpeedPacePercent > blockingStrength)
			{
				// Reward
				int currentScore = gameManager.CurrentScore;
				gameManager.SetScore(currentScore + _bonusScore);
				
				// Despawn
				Destroy(gameObject);
			}
			
			else
			{
				string aAn = IsNameStartsWithVowel()? "an": "a";
				gameManager.GameOver($"The player ran into {aAn} {name}");
			}
		}
		
		bool IsNameStartsWithVowel()
		{
			char c = name.ToLower()[0];
			return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
		}
	}
}