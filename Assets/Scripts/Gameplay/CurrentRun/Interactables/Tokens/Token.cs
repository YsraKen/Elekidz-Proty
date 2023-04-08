using UnityEngine;

namespace CurrentRunGame
{
	public class Token : Interactable
	{
		[SerializeField] protected Vector2 _amountMinMax = new Vector2(0.1f, 2f);
		
		public float GetAmount() => Random.Range(_amountMinMax.x, _amountMinMax.y);
	}
}