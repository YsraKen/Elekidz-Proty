using UnityEngine;

namespace CurrentRunGame
{
	public class Resistance : Token
	{
		[ContextMenu("Interact")]
		public override void Interact()
		{
			if(!gameManager.IsPlaying) return;
			
			float resistance = -GetAmount();
			
			gameManager.ModifyCurrentSpeed(resistance);
			Destroy(gameObject);
		}
	}
}