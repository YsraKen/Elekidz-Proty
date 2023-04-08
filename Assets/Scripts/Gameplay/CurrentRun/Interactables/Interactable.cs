using UnityEngine;

namespace CurrentRunGame
{
	public class Interactable : MonoBehaviour
	{
		[SerializeField, TagSelect] string _playerTag = "Player";
		
		static GameManager _gameManager;
		static Player _player;
		
		protected static GameManager gameManager
		{
			get
			{
				if(!_gameManager)
					_gameManager = GameManager.Instance;
				
				return _gameManager;
			}
		}
		
		protected static Player player
		{
			get
			{
				if(!_player)
					_player = Player.Instance;
				
				return _player;
			}
		}
		
		public virtual void Interact(){}
		
		void OnTriggerEnter2D(Collider2D col)
		{
			if(col.CompareTag(_playerTag))
				Interact();
		}
	}
}