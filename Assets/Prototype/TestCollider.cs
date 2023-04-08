using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
	public Collider2D _collider, _collider2;
	
	public DistanceInfo distanceInfo;
	
	[ContextMenu("Test")]
	void Test()
	{
		distanceInfo = new DistanceInfo(_collider.Distance(_collider2));
	}
	
	[System.Serializable]
	public struct DistanceInfo
	{
		public float distance;
		public bool isOverlapped, isValid;
		public Vector2 normal, pointA, pointB;
		
		public DistanceInfo(ColliderDistance2D unity)
		{
			distance = unity.distance;
			isOverlapped = unity.isOverlapped;
			isValid = unity.isValid;
			normal = unity.normal;
			pointA = unity.pointA;
			pointB = unity.pointB;
		}
	}
}
