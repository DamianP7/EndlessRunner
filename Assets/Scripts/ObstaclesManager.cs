using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
	[SerializeField] List<Obstacle> obstacles;
	List<GameObject> usedObstacles;

	private void OnValidate()
	{
		Debug.Log("	!--> Obstacles Manager validated");
		obstacles = new List<Obstacle>();
		for (int i = 0; i < transform.childCount; i++)
		{
			obstacles.Add(transform.GetChild(i).GetComponent<Obstacle>());
		}
	}

	private void Awake()
	{
		usedObstacles = new List<GameObject>();
	}

	public GameObject UseRandomElement(int diff)
	{
		Debug.Log("diff: " + diff);
		List<Obstacle> availableObstacles = new List<Obstacle>();

		int check = 0;
		do
		{
			foreach (var item in obstacles)
			{
				if (item.difficultyLevel == diff)
					availableObstacles.Add(item);
			}

			diff -= 5;
			check++;
			if (check > 10)
				break;

		} while (availableObstacles.Count == 0);


		int index = Random.Range(0, availableObstacles.Count);

		obstacles.Remove(availableObstacles[index]);

		usedObstacles.Add(availableObstacles[index].gameObject);

		return usedObstacles[usedObstacles.Count - 1];
	}

	public void ReturnElement(GameObject obstacle)
	{
		obstacles.Add(obstacle.GetComponent<Obstacle>());
		usedObstacles.Remove(obstacle);
	}
}
