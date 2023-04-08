using UnityEngine;
using System.Collections;
using VirtualCam = Cinemachine.CinemachineVirtualCamera;

namespace CurrentRunGame
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] VirtualCam _reference;
		
		[Foldout("Field of View Effect")]
		[SerializeField, LabelOverride("Amount")] float _fovEffectAmount = 1.25f;
		[SerializeField, LabelOverride("Duration")] float _fovEffectDuration = 5f;
		[SerializeField, LabelOverride("Transition Smooth Time")] float _fovTransitionSmoothTime = 0.25f;
		
		/* float _currentSpeed, _originalValue;
		const float MIN_SPD_THRESHOLD = 1f;
		
		float _fovCurrentSmoothValue, _fovTargetValue, _fovSmoothVelocity;
		
		void Start()
		{
			_currentSpeed = GameManager.Instance.CurrentSpeedPacePercent;
			_originalValue = _reference.m_Lens.OrthographicSize;
		}
		
		public void OnSpeedUpdate(float currentSpeed)
		{
			bool isFaster = currentSpeed < _currentSpeed;
			
			_currentSpeed = Mathf.Clamp(_currentSpeed, MIN_SPD_THRESHOLD, currentSpeed);
			
			if(!isFaster) return;
			
			StopAllCoroutines();
			StartCoroutine(r());
			
			IEnumerator r()
			{
				float timer = _fovEffectDuration;
				
				yield return new WaitWhile(()=>
				{
					timer -= Time.deltaTime;
					float lerp = timer / _fovEffectDuration;
					
					_fovTargetValue = Mathf.Lerp(_originalValue, _fovEffectAmount, Mathf.Clamp01(lerp));
					return timer < 0f;
				});
			}
		}
		
		IEnumerator InvokeFovEffect()
		{
			float timer = 0f;
			
			while(timer < _fovEffectDuration)
			{
				yield return null;
				
				timer += Time.deltaTime;
				
				float lerp = Mathf.Clamp01(timer / _fovEffectDuration);
				float curve = _fovEffectCurve.Evaluate(lerp);
				_reference.m_Lens.OrthographicSize = Mathf.Lerp(_originalValue, _fovEffectAmount, curve);
			}
			
			_reference.m_Lens.OrthographicSize = _originalValue;
		}
		
		void LateUpdate()
		{
			_fovCurrentSmoothValue = Mathf.SmoothDamp
			(
				_fovCurrentSmoothValue,
				_fovTargetValue,
				ref _fovSmoothVelocity,
				_fovTransitionSmoothTime,
				Time.deltaTime
			);
			
			_reference.m_Lens.OrthographicSize = _fovCurrentSmoothValue;
		} */
	}
}