using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
	[Header("Segment Properties")]
	public Vector3 size;
	public int segmentDifficulty, minDifficultyLevel, maxDifficultyLevel;
	public int minCosmetics = 0, maxCosmetics = 2;
	[SerializeField] int coinObstacleChance, coinNearObstacleChance, coinChance;
	[SerializeField] bool waterSegment, doubleWaterSegment;

	[Header("Scripts and objects")]
	[SerializeField] List<Transform> cosmeticPositions;
	[SerializeField] List<Transform> obstaclePositions;
	[SerializeField] List<IntTransformPair> coinPositions;
	/// <summary>
	/// Lista użytych przedmiotów.
	/// </summary>
	List<GameObject> usedCosmetic, usedObstacles, usedCoins;
	/// <summary>
	/// Lista użytych indexów.
	/// </summary>
	List<int> usedCosmeticPositions, usedObstaclesPositions;

	[Header("Misc.")]
	[SerializeField] CosmeticsManager cosmeticsManager;
	[SerializeField] ObstaclesManager obstaclesManager;
	[SerializeField] CoinsManager coinsManager;
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

		if (minDifficultyLevel < segmentDifficulty)
			minDifficultyLevel = segmentDifficulty;

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
		usedCoins = new List<GameObject>();
		usedCosmeticPositions = new List<int>();
		usedObstaclesPositions = new List<int>();
	}

	public void SetupSegment(int expectedDiff)
	{
		int diffTotal = segmentDifficulty;
		int cosmeticPos, obstaclePos;
		int cosmeticsQuantity = Random.Range(minCosmetics, maxCosmetics);
		expectedDiff -= segmentDifficulty;

		int check = 0;
		for (int i = 0; i < cosmeticsQuantity; i++)
		{
			do
			{
				cosmeticPos = Random.Range(0, cosmeticPositions.Count);
				check++;
				if (check > 100)
				{
					Debug.LogError("Error in SetupSegment(first do-while) in " + transform.name);
					return;
				}
			} while (usedCosmeticPositions.Contains(cosmeticPos));

			usedCosmeticPositions.Add(cosmeticPos);
			usedCosmetic.Add(cosmeticsManager.UseRandomElement());
			usedCosmetic[i].transform.position = cosmeticPositions[cosmeticPos].position;

			if(DeveloperOptions.Instance.segmentLog.environmentGeneration)
				Debug.Log(usedCosmetic[i].name + " on pos " + cosmeticPositions[cosmeticPos].name + "(" + cosmeticPositions[cosmeticPos].parent.name + ")");
		}

		//int obstaclesQuantity = Random.Range(obstaclePositions.Count / 4, obstaclePositions.Count);
		while (expectedDiff > 0)
		{
			do
			{
				obstaclePos = Random.Range(0, obstaclePositions.Count);
				check++;
				if (check > 100)
				{
					Debug.LogError("Error in SetupSegment(second do-while) in " + transform.name);
					return;
				}
			} while (usedObstaclesPositions.Contains(obstaclePos));
			
			int diff = RoundToFive(Random.Range(minDifficultyLevel, expectedDiff));
			if (diff >= maxDifficultyLevel)
				diff = maxDifficultyLevel;
			if (diff == 0)
			{
				Debug.LogError("Can't find obstacle here: (" + transform.name + ')');
				break;
			}
			expectedDiff -= diff;
			diffTotal += diff;
			usedObstaclesPositions.Add(obstaclePos);
			usedObstacles.Add(obstaclesManager.UseRandomElement(diff));
			usedObstacles[usedObstacles.Count - 1].transform.position = obstaclePositions[obstaclePos].position;

			if (DeveloperOptions.Instance.segmentLog.obstaclesGeneration)
				Debug.Log(usedObstacles[usedObstacles.Count - 1].name + " on pos " + obstaclePositions[obstaclePos].name + "(" + obstaclePositions[obstaclePos].parent.name + ")");
		}
		if (DeveloperOptions.Instance.segmentCanvas.segmentDifficulty)
			DeveloperOptions.Instance.segmentDifficulty.text = diffTotal.ToString();

			List<IntTransformPair> temp;
		if (segmentDifficulty == 0)
		{
			if (usedObstaclesPositions.Contains(0))
			{
				MoveCoinsObstacle(0);
			}
			else
				MoveCoins(0);
			if (usedObstaclesPositions.Contains(1))
			{
				MoveCoinsObstacle(1);
			}
			else
				MoveCoins(1);
			if (usedObstaclesPositions.Contains(2))
			{
				MoveCoinsObstacle(2);
			}
			else
				MoveCoins(2);
		}
		else if (waterSegment)
		{
			if (usedObstaclesPositions.Contains(0))
			{
				if (ProcentChancesRandom(coinObstacleChance))
				{
					temp = coinPositions.FindAll(item => item.number == 0);
					usedCoins.Add(coinsManager.UseRandomCoin());
					usedCoins[usedCoins.Count - 1].transform.position = temp[1].transform.gameObject.transform.position;
				}
			}
			if (usedObstaclesPositions.Contains(1))
			{
				if (ProcentChancesRandom(coinObstacleChance))
				{
					temp = coinPositions.FindAll(item => item.number == 4);
					usedCoins.Add(coinsManager.UseRandomCoin());
					usedCoins[usedCoins.Count - 1].transform.position = temp[1].transform.gameObject.transform.position;
				}
			}
			if (ProcentChancesRandom(coinNearObstacleChance))
			{
				temp = coinPositions.FindAll(item => item.number == 1);
				usedCoins.Add(coinsManager.UseRandomCoin());
				usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
			}
			if (ProcentChancesRandom(coinNearObstacleChance))
			{
				temp = coinPositions.FindAll(item => item.number == 2);
				usedCoins.Add(coinsManager.UseRandomCoin());
				usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
			}
			if (ProcentChancesRandom(coinNearObstacleChance))
			{
				temp = coinPositions.FindAll(item => item.number == 3);
				usedCoins.Add(coinsManager.UseRandomCoin());
				usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
			}
		}
		else if (doubleWaterSegment)
		{
			for (int i = 0; i < 5; i++)
			{
				if (ProcentChancesRandom(coinNearObstacleChance))
				{
					temp = coinPositions.FindAll(item => item.number == i);
					usedCoins.Add(coinsManager.UseRandomCoin());
					usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
				}
			}
		}
		if (DeveloperOptions.Instance.segmentLog.coinGeneration)
		{
			int value = 0;
			foreach (GameObject item in usedCoins)
			{
				value += item.GetComponent<Coin>().value;
			}
			Debug.Log("Coins in segment " + transform.name + ": " + value);
		}
	}

	void MoveCoinsObstacle(int obstacleNumber)
	{
		List<IntTransformPair> temp;
		if (ProcentChancesRandom(coinNearObstacleChance))
		{
			temp = coinPositions.FindAll(item => item.number == obstacleNumber * 2);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
		}
		if (ProcentChancesRandom(coinObstacleChance))
		{
			temp = coinPositions.FindAll(item => item.number == obstacleNumber * 2 + 1);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[1].transform.gameObject.transform.position;
		}
		if (ProcentChancesRandom(coinNearObstacleChance) && obstacleNumber != 0)
		{
			temp = coinPositions.FindAll(item => item.number == obstacleNumber * 2 + 2);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
		}
	}

	void MoveCoins(int position)
	{
		List<IntTransformPair> temp;
		if (ProcentChancesRandom(coinChance))
		{
			temp = coinPositions.FindAll(item => item.number == position * 2);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
		}
		if (ProcentChancesRandom(coinChance))
		{
			temp = coinPositions.FindAll(item => item.number == position * 2 + 1);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
		}
		if (ProcentChancesRandom(coinChance) && position != 0)
		{
			temp = coinPositions.FindAll(item => item.number == position * 2 + 2);
			usedCoins.Add(coinsManager.UseRandomCoin());
			usedCoins[usedCoins.Count - 1].transform.position = temp[0].transform.gameObject.transform.position;
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
		for (int i = 0; i < usedCoins.Count; i++)
		{
			usedCoins[i].transform.position = new Vector3(usedCoins[i].transform.position.x - speed, usedCoins[i].transform.position.y, usedCoins[i].transform.position.z);
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
		for (int i = 0; i < usedCoins.Count; i++)
		{
			coinsManager.ReturnRandomCoin(usedCoins[i]);
		}
		usedCosmetic = new List<GameObject>();
		usedCosmeticPositions = new List<int>();
		usedObstacles = new List<GameObject>();
		usedObstaclesPositions = new List<int>();
		usedCoins = new List<GameObject>();
	}

	bool ProcentChancesRandom(int winProcent)
	{
		if (Random.Range(0, 100) < winProcent)
			return true;
		else
			return false;
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

[System.Serializable]
public struct IntTransformPair
{
	public int number;
	public Transform transform;
	public bool coinPlaced;
}