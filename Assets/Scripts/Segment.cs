using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
	public Vector3 size;
	private Vector3 startPosition;

	private void Awake()
	{
		startPosition = transform.position;
	}

	public void Move(float speed)
	{
		transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
	}

	public void StopMoving()
	{
		transform.position = startPosition;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, size);
	}
}
