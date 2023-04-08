using UnityEngine;

namespace CurrentRunGame
{
	public class Background : MonoBehaviour
	{
		[SerializeField] Transform[] _transforms;
		[SerializeField] float _speed = 1f;
		[SerializeField] AnimationCurve _curve;
		
		public float speedMultiplier { get; set; } = 1f;
		
		void LateUpdate()
		{
			float deltaSpeed = _speed * speedMultiplier * Time.deltaTime;
			
			int count = _transforms.Length;
			float maxIndexF = (float) count - 1;
			
			for(int i = 0; i < count; i++)
			{
				var transform = _transforms[i];
				float lerp = (float) i / maxIndexF;
				
				transform.Rotate(Vector3.down * deltaSpeed * _curve.Evaluate(lerp), Space.World);
			}
		}
	}
}