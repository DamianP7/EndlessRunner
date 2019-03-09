using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
	[Header("Segment Properties")]
	public Vector3 size;
	public int segmentDifficulty, minDifficultyLevel, maxDifficultyLevel;
	public int minCosmetics = 0, maxCosmetics = 2;

	[Header("Scripts and objects")]
	[SerializeField] List<Transform> cosmeticPositions;
	[SerializeField] List<Transform> obstaclePositions;
	List<GameObject> usedCosmetic, usedObstacles;
	List<int> usedCosmeticPositions, usedObstaclesPositions;

	[Header("Misc.")]
	[SerializeField] CosmeticsManager cosmeticsManager;
	[SerializeField] ObstaclesManager obstaclesManager;
	private Vector3 startPosition;

	private void OnValidate()
	{
		if (maxCosmetics < minCosmetics)
			minCosmetics = maxCosmetics;
		if (minCosmetics < 0)
			minCosmetics = 0;
		if (maxCosmetics > cosmeticPositions.Count)
			maxCosmetics = cosmeticPositions.Count;
		if (minCosmetics > maxCosmetics)
			minCosmetics = maxCosmetics;
		if (maxCosmetics < 0)
			minCosmetics = maxCosmetics = 0;

		Debug.Log("	!--> " + gameObject.name + " validated");
		cosmeticPositions = new List<Transform>();
		obstaclePositions = new List<Transform>();

		List<GameObject> items = new List<GameObject>();
		for (int i = 0; i < transform.childCount; i++)
		{
			items.Add(transform.GetChild(i).gameObject);
		}

		foreach (GameObject item in items)
		{
			for (int i = 0; i < item.transform.childCount; i++)
			{
				if (item.transform.GetChild(i).name.Contains("Environment"))
					cosmeticPositions.Add(item.transform.GetChild(i).transform);
				else if (item.transform.GetChild(i).name.Contains("Obstacle"))
					obstaclePositions.Add(item.transform.GetChild(i).transform);
			}
		}
	}

	private void Awake()
	{
		startPosition = transform.position;
		usedCosmetic = new List<GameObject>();
		usedObstacles = new List<GameObject>();
		usedCosmeticPositions = new List<int>();
		usedObstaclesPositions = new List<int>();
	}

	public void SetupSegment(int expectedDiff)
	{
		int cosmeticPos, obstaclePos;
		int cosmeticsQuantity = Random.Range(minCosmetics, maxCosmetics);
		expectedDiff -= segmentDifficulty;

		for (int i = 0; i < cosmeticsQuantity; i++)
		{
			do
			{
				cosmeticPos = Random.Range(0, cosmeticPositions.Count);
			} while (usedCosmeticPositions.Contains(cosmeticPos));

			usedCosmeticPositions.Add(cosmeticPos);
			usedCosmetic.Add(cosmeticsManager.UseRandomElement());
			usedCosmetic[i].transform.position = cosmeticPositions[cosmeticPos].position;

			Debug.Log(usedCosmetic[i].name + " on pos " + cosmeticPositions[cosmeticPos].name + "(" + cosmeticPositions[cosmeticPos].parent.name + ")");
		}

		//int obstaclesQuantity = Random.Range(obstaclePositions.Count / 4, obstaclePositions.Count);
		while (expectedDiff > 0)
		{
			do
			{
				obstaclePos = Random.Range(0, obstaclePositions.Count);
			} while (usedObstaclesPositions.Contains(obstaclePos));

			//TODO: temp
			int diff = RoundToFive(Random.Range(minDifficultyLevel, expectedDiff));
			expectedDiff -= diff;
			if (diff == 0)
			{
				Debug.LogError("Can't find obstacle here: (" + transform.name + ')');
				break;
			}
			usedObstaclesPositions.Add(obstaclePos);
			usedObstacles.Add(obstaclesManager.UseRandomElement(diff));
			usedObstacles[usedObstacles.Count-1].transform.position = obstaclePositions[obstaclePos].position;

			Debug.Log(usedObstacles[usedObstacles.Count - 1].name + " on pos " + obstaclePositions[obstaclePos].name + "(" + obstaclePositions[obstaclePos].parent.name + ")");
		}
	}

	public void Move(float speed)
	{
		transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
		for (int i = 0; i < usedCosmetic.Count; i++)
		{
			usedCosmetic[i].transform.position = new Vector3(usedCosmetic[i].transform.position.x - speed, usedCosmetic[i].transform.position.y, usedCosmetic[i].transform.position.z);
		}
		for (int i = 0; i < usedObstacles.Count; i++)
		{
			usedObstacles[i].transform.position = new Vector3(usedObstacles[i].transform.position.x - speed, usedObstacles[i].transform.position.y, usedObstacles[i].transform.position.z);
		}
	}

	public void StopMoving()
	{
		transform.position = startPosition;
		for (int i = 0; i < usedCosmetic.Count; i++)
		{
			cosmeticsManager.ReturnElement(usedCosmetic[i]);
		}
		for (int i = 0; i < usedObstacles.Count; i++)
		{
			obstaclesManager.ReturnElement(usedObstacles[i]);
		}
		usedCosmetic = new List<GameObject>();
		usedCosmeticPositions = new List<int>();
		usedObstacles = new List<GameObject>();
		usedObstaclesPositions = new List<int>();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, size);
	}

	int RoundToFive(int x)
	{
		int y = x / 5;
		int z = x % 5;

		if (z < 3) y = y * 5;
		else y = y * 5 + 5;

		return y;
	}
}
