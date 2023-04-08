using UnityEngine;

namespace CurrentRunGame
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] GameObject[] _randomGpx;
		[SerializeField] Vector2 _overshootForce;
		[SerializeField] float _overshootAngularVelocity;
		
		Animator _animator;
		
		static int
			_run = Animator.StringToHash("run"),
			_isGnd = Animator.StringToHash("isGrounded"),
			_jump = Animator.StringToHash("jump"),
			_die = Animator.StringToHash("die");
		
		void Awake()
		{
			_animator = _randomGpx.GetRandom(out int index).GetComponent<Animator>();
			
			for(int i = 0; i < _randomGpx.Length; i++)
				if(i != index)
					Destroy(_randomGpx[i]);
			
			_randomGpx = null;
		}
		
		public void SetRunning(bool isRunning) => _animator.SetBool(_run, isRunning);
		
		public void SetGrounded(bool isGrounded) => _animator.SetBool(_isGnd, isGrounded);
		
		public void Jump() => _animator.SetTrigger(_jump);
		
		public void Die() => _animator.SetTrigger(_die);
		
		[ContextMenu("Overshoot")]
		public void Overshoot()
		{
			var rb = GetComponent<Rigidbody2D>();
			
				rb.constraints = default;
				rb.AddForce(_overshootForce, ForceMode2D.Impulse);
				rb.angularVelocity = _overshootAngularVelocity;
		}
	}
}