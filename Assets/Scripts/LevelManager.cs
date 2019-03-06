using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public Segment[] segments;

	[SerializeField] float stopPositionX;

	int lastSegment, activeSegment, newSegment;

	public List<GameObject> smallObjects;


	void Start()
	{
		lastSegment = 0;
		segments[lastSegment].transform.position = new Vector3(0, 0, 0);
		activeSegment = 1;
		//newSegment = 2;
	}

	void Update()
	{
		if (GameManager.Instance.gameState == GameState.Playing)
		{
			segments[lastSegment].Move(GameManager.Instance.speed * Time.deltaTime);
			segments[activeSegment].Move(GameManager.Instance.speed * Time.deltaTime);
			//segments[newSegment].Move(GameManager.Instance.speed * Time.deltaTime);

			if (segments[lastSegment].transform.position.x < stopPositionX)
				FindNextSegment();
		}
	}

	void FindNextSegment()
	{
		segments[lastSegment].StopMoving();
		int rand;
		do
		{
			rand = Random.Range(0, segments.Length);
		} while (rand == activeSegment || rand == newSegment);
		lastSegment = activeSegment;
		activeSegment = rand;
		segments[activeSegment].SetupSegment();
		//newSegment = rand;
	}
}
