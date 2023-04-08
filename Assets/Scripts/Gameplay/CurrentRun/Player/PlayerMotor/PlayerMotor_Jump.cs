using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace CurrentRunGame
{
	public partial class PlayerMotor
	{
		[Foldout("Jumping")]
		[SerializeField, LabelOverride("Force")] float _jumpForce = 5f;
		
		[SerializeField, LabelOverride("Buffer Duration")] float _jumpBufferDuration = 0.25f;
		[SerializeField, LabelOverride("Variable Height Counter Force"), Range(0,1)] float _jumpVariableHeightCounterForcePercent = 0.15f;
		
		
		Coroutine _jumpBufferTimer, _variableJumpHeightHandler;
		bool _isJumpBuffering;
		
		[SerializeField] UnityEvent _onJump;
		
		void HandleJumping()
		{
			if(!_gameManager.IsPlaying) return;
			
			// Can't jump if not grounded
			if(!IsGrounded)
			{
				// Can only invoke buffer detection
				if(PlayerInput.JumpButtonDown())
					RestartCoroutine(ref _jumpBufferTimer, JumpBufferTimer());
				
				return;
			}
			
			// Automatically jump if buffer is active
			if(_isJumpBuffering)
			{
				StopCoroutine(_jumpBufferTimer);
				_isJumpBuffering = false;
				
				Jump();
				HandleVariableJumpHeight();
				
				return;
			}
			
			// The actual & manual jumping from player input
			if(PlayerInput.JumpButtonDown())
			{
				Jump();
				HandleVariableJumpHeight();
			}
		}
		
		// Jump buffering allows the player to jump even if the input triggers a few milliseconds before it is grounded.
		IEnumerator JumpBufferTimer()
		{
			_isJumpBuffering = true;
			yield return new WaitForSeconds(_jumpBufferDuration);
			_isJumpBuffering = false;
		}
		
		void Jump()
		{
			_rb.velocity = new Vector2(_rb.velocity.x, 0f);
			_rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
			
			// associated calls
				OverrideGroundChecking(value: false, 0.25f); // don't update ground checking by itself for "0.25" or "n" seconds
				_onJump.Invoke();
		}
		
		// allows the player to have a variable jump height based on how long the input is held.
		void HandleVariableJumpHeight()
		{
			RestartCoroutine(ref _variableJumpHeightHandler, r());
			
			IEnumerator r()
			{
				yield return new WaitWhile(()=> PlayerInput.JumpButton());
				
				if(_rb.velocity.y < 0)
					yield break;
				
				// Counter the launch velocity (instead of totally cancelling it)				
				var force = Vector2.down * (_rb.velocity.y * _jumpVariableHeightCounterForcePercent);
				_rb.AddForce(force, ForceMode2D.Impulse);
			}
		}
	}
}