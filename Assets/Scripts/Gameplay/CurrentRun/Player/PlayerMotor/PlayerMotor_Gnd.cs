using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace CurrentRunGame
{
	public partial class PlayerMotor
	{
		[Foldout("Ground Checking")]
		[SerializeField, LabelOverride("Collider")] Collider2D _gndCheckCollider;
		[SerializeField, LabelOverride("Layers")] LayerMask _gndLayers;
		
		[field: SerializeField, Disabled] public bool IsGrounded{ get; private set; }
		
		[SerializeField] UnityEvent<bool> _onGndCheckUpdate;
		
		// Handle overriding
		bool _isGndCheckOverriden, _gndCheckOverrideValue;
		
		void HandleGroundChecking()
		{
			IsGrounded = _isGndCheckOverriden?
				_gndCheckOverrideValue:
				_gndCheckCollider.IsTouchingLayers(_gndLayers);
			
			_onGndCheckUpdate.Invoke(IsGrounded);
		}
		
		// Handle overriding
		public Coroutine OverrideGroundChecking(bool value, float duration) // Optional Return: a coroutine so the user can manually stop it later
		{
			return StartCoroutine(r());
			
			IEnumerator r()
			{
				_isGndCheckOverriden = true;
				{
					_gndCheckOverrideValue = value;
					yield return new WaitForSeconds(duration);
				}
				_isGndCheckOverriden = false;
			}
		}
	}
}