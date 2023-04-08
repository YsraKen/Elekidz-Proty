using UnityEngine;
using UnityEngine.Events;

namespace CurrentRunGame
{
	public class Player : SceneObjectSingleton<Player>
	{
		[SerializeField] float _startSpeed = 3f;
		[field: SerializeField, Disabled] public float CurrentSpeed { get; private set; }
		
		[SerializeField] UnityEvent _onStartRunning, _onStopRunning;
		
		public void ModifyCurrentSpeed(float amount) => CurrentSpeed += amount;
		
		public void StartRunning()
		{
			CurrentSpeed = _startSpeed;
			_onStartRunning.Invoke();
		}
		
		public void StopRunning()
		{
			CurrentSpeed = 0f;
			_onStopRunning.Invoke();
		}
	}
}