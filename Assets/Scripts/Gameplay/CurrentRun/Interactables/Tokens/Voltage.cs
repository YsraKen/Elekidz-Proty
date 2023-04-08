using UnityEngine;

namespace CurrentRunGame
{
	public class Voltage : Token
	{
		[SerializeField, Range(0,1)] float _overshootProbability = 0f;
		
		[ContextMenu("Interact")]
		public override void Interact()
		{
			if(!gameManager.IsPlaying) return;
			
			float voltage = GetAmount();
			gameManager.ModifyCurrentSpeed(voltage);
			
			float random = Random.value;
			bool overshoot = random < _overshootProbability;
			
			if(overshoot)
			{
				Debug.Log(random);
				
				float rounded = Mathf.Round(voltage * 100);
				gameManager.GameOver($"The player has overshot with the additional voltage amount of {rounded}%");
				
				player.GetComponent<PlayerAnimator>()?.Overshoot();
			}
			
			Destroy(gameObject);
		}
	}
}