using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CurrentRunGame
{
	public class WorldObjects : MonoBehaviour
	{
		[SerializeField] GameObject[] _pool;
		[SerializeField] float _speed = 1f;
		
		public float speedMultiplier { get; set; } = 1f; // percent
		
		[SerializeField] Vector2 _spawnRateMinMax = new Vector2(1f, 3f);
		[SerializeField] float _spawnOffset = 1f;
		
		[SerializeField] Transform _spawnPoint, _despawnPoint, _instances;
		
		List<Transform> _spawnedObjects = new List<Transform>();
		
		IEnumerator Start()
		{
			var gMgr = GameManager.Instance;
			
			while(true)
			{
				if(gMgr.IsPlaying)
				{
					yield return new WaitForSeconds(Random.Range(_spawnRateMinMax.x, _spawnRateMinMax.y));
					
					var template = _pool.GetRandom();
					var offset = (Vector3) Random.insideUnitCircle * _spawnOffset;
					
					var newObject = Instantiate(template, _spawnPoint.position + offset, default, _instances);
						newObject.SetActive(true);
					
						_spawnedObjects.Add(newObject.transform);
				}
				
				else yield return null;
			}
		}
		
		void Update()
		{
			var translation = Vector3.left * _speed * speedMultiplier * Time.deltaTime;
			var toBeDespawned = new List<Transform>();
			
			foreach(var obj in _spawnedObjects)
			{
				if(!obj) continue;
				
				obj.Translate(translation);
				
				if(obj.position.x < _despawnPoint.position.x)
				{
					if(!toBeDespawned.Contains(obj))
						toBeDespawned.Add(obj);
				}
			}
			
			_spawnedObjects = _spawnedObjects.Except(toBeDespawned).ToList();
			
			toBeDespawned.ForEach(obj => Destroy(obj.gameObject));
			toBeDespawned.Clear();
		}
		
		public void OnGameOver()
		{
			enabled = false;
			
			foreach(var obj in _spawnedObjects)
				Destroy(obj.gameObject);
			
			_spawnedObjects.Clear();
		}
		
		void OnDrawGizmos()
		{
			if(!_spawnPoint)
				return;
			
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(_spawnPoint.position, _spawnOffset);
		}
	}
}