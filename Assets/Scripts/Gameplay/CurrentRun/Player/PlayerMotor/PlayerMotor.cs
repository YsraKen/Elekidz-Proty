using UnityEngine;
using System.Collections;

namespace CurrentRunGame
{
	[RequireComponent(typeof(Player))]
	[RequireComponent(typeof(Rigidbody2D))]
	public partial class PlayerMotor : MonoBehaviour
	{
		[SerializeField] Player _player;
		[SerializeField] Rigidbody2D _rb;
		
		GameManager _gameManager;
		
		void OnValidate()
		{
			if(!_player) _player = GetComponent<Player>();
			if(!_rb) _rb = GetComponent<Rigidbody2D>();
		}
		
		void Start() => _gameManager = GameManager.Instance;
		
		void Update()
		{
			HandleGroundChecking();
			HandleJumping();
		}
		
		void RestartCoroutine(ref Coroutine variable, IEnumerator method)
		{
			if(variable != null)
				StopCoroutine(variable);
			
			variable = StartCoroutine(method);
		}
	}
}