using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public Segment[] segments;

	[SerializeField] float stopPositionX;
	[SerializeField] int startDifficulty, difficultyIncrementValue, timeToIncrement, timeToNewSegment, diffTolerance; // TODO:

	int availableDifficulty, difficultyTotal, cycleNumber = 0;
	float timeLeft;
	int lastSegment, activeSegment, newSegment;
	float time = 0;

	private void OnValidate()
	{
		Debug.Log("	!--> Level Manager validated");
		segments = GameObject.Find("Segments").GetComponentsInChildren<Segment>();
	}

	void Start()
	{
		NewChunk();

		lastSegment = 0;
		activeSegment = 1;

		segments[lastSegment].transform.position = new Vector3(0, -0.1f, 0);
		segments[lastSegment].SetupSegment(0);
		segments[activeSegment].SetupSegment(5);    //TODO: temp const 5
		segments[activeSegment].transform.position = new Vector3(20, -0.1f, 0);
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;
		time += Time.deltaTime;
		if (timeLeft <= 0)
			NewChunk();

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
		Debug.Log("Time to next segment: " + time);
		time = 0;
		segments[lastSegment].StopMoving();

		int diff = Mathf.RoundToInt(difficultyTotal / timeToNewSegment) + Random.Range(-diffTolerance, diffTolerance);
		Debug.Log("		Levelmanager diff: " + diff);
		int rand;

		List<Segment> availableSegments = new List<Segment>();
		for (int i = 0; i < segments.Length; i++)
		{
			if (segments[i].maxDifficultyLevel > diff)
				availableSegments.Add(segments[i]);
		}

		if (availableSegments.Count == 0)
		{
			availableSegments = new List<Segment>();
			Debug.LogError("No available segments");
			for (int i = 0; i < segments.Length; i++)
			{
				if (segments[i].maxDifficultyLevel > 0)
					availableSegments.Add(segments[i]);
			}
		}

		int check = 0;
		do
		{
			rand = Random.Range(0, availableSegments.Count);
			check++;
			if (check > 100)
			{
				Debug.LogError("Error in FindNextSegment in " + transform.name);
				return;
			}
		} while (rand == activeSegment || rand == newSegment);

		lastSegment = activeSegment;
		activeSegment = rand;
		segments[activeSegment].transform.position = new Vector3(20, -0.1f, 0);
		segments[activeSegment].SetupSegment(diff);
		//newSegment = rand;
	}

	void NewChunk()
	{
		timeLeft = timeToIncrement;
		availableDifficulty = startDifficulty + difficultyIncrementValue * cycleNumber;
		difficultyTotal = availableDifficulty;
		cycleNumber++;
	}
}
