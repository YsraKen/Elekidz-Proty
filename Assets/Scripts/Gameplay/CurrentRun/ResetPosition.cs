
using UnityEngine;

namespace CurrentRunGame
{
	public class ResetPosition : MonoBehaviour
	{
		[SerializeField, TagSelect] string _playerTag = "Player";
		[SerializeField] Transform _targetPoint;
		
		void OnTriggerEnter2D(Collider2D col)
		{
			if(!col.CompareTag(_playerTag)) return;
			
			var player = Player.Instance.transform;
			
			var position = _targetPoint.position;
				position.y = player.position.y;
			
			player.position = position;
		}
	}
}