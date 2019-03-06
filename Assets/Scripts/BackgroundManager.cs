using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[Header("Clouds")]
	[SerializeField] List<GameObject> clouds;
	[SerializeField] float speedOfClouds, stopForClouds;
	[SerializeField] Vector3 cloudsSpawnPoint;
	[SerializeField] bool cloudRandomHeight;
	[SerializeField] float cloudHeightTollerance;

	[Header("Fogs")]
	[SerializeField] List<GameObject> fogs1;
	[SerializeField] List<GameObject> fogs2;
	[SerializeField] List<GameObject> fogs3;
	[SerializeField] float speedOfFog1, stopForFog1, speedOfFog2, stopForFog2, speedOfFog3, stopForFog3;
	[SerializeField] Vector3 fog1SpawnPoint, fog2SpawnPoint, fog3SpawnPoint;
	[SerializeField] bool fog1RandomHeight, fog2RandomHeight, fog3RandomHeight;
	[SerializeField] float fog1HeightTollerance, fog2HeightTollerance, fog3HeightTollerance;

	[Header("First Plan")]
	[SerializeField] List<GameObject> firstPlan;
	[SerializeField] float speedOfFirstPlan, stopForFirstPlan;
	[SerializeField] Vector3 firstPlanSpawnPoint;
	[SerializeField] bool firstPlanRandomHeight;
	[SerializeField] float firstPlanHeightTollerance;

	[Header("Second Plan")]
	[SerializeField] List<GameObject> secondPlan;
	[SerializeField] float speedOfSecondPlan, stopForSecondPlan;
	[SerializeField] Vector3 secondPlanSpawnPoint;
	[SerializeField] bool secondPlanRandomHeight;
	[SerializeField] float secondPlanHeightTollerance;

	[Header("Third Plan")]
	[SerializeField] List<GameObject> thirdPlan;
	[SerializeField] float speedOfThirdPlan, stopForThirdPlan;
	[SerializeField] Vector3 thirdPlanSpawnPoint;
	[SerializeField] bool thirdPlanRandomHeight;
	[SerializeField] float thirdPlanHeightTollerance;


	List<GameObject> activeClouds, activeFog1, activeFog2, activeFog3, activeFirstPlan, activeSecondPlan, activeThirdPlan;

	private void Start()
	{
		activeClouds = clouds;
		activeFog1 = fogs1;
		activeFog2 = fogs2;
		activeFog3 = fogs3;
		activeFirstPlan = firstPlan;
		activeSecondPlan = secondPlan;
		activeThirdPlan = thirdPlan;
	}

	void Update()
	{
		if (GameManager.Instance.gameState == GameState.Playing)
		{
			float speed = GameManager.Instance.speed;
			// clouds
			for (int i = 0; i < activeClouds.Count; i++)
			{
				activeClouds[i].transform.position = new Vector3(activeClouds[i].transform.position.x - speedOfClouds * speed, activeClouds[i].transform.position.y);
				Debug.Log("s: " + speed + "po: " + activeClouds[i].transform.position);
				if (activeClouds[i].transform.position.x < stopForClouds)
				{
					if (cloudRandomHeight)
					{
						activeClouds[i].transform.position = new Vector3(cloudsSpawnPoint.x, cloudsSpawnPoint.y + Random.Range(-cloudHeightTollerance, cloudHeightTollerance));
					}
					else
					{
						activeClouds[i].transform.position = cloudsSpawnPoint;
					}
				}
			}
			
			// fog 1
			for (int i = 0; i < activeFog1.Count; i++)
			{
				activeFog1[i].transform.position = new Vector3(activeFog1[i].transform.position.x - speedOfFog1 * speed, activeFog1[i].transform.position.y);
				if (activeFog1[i].transform.position.x < stopForFog1)
				{
					if (fog1RandomHeight)
					{
						activeFog1[i].transform.position = new Vector3(fog1SpawnPoint.x, fog1SpawnPoint.y + Random.Range(-fog1HeightTollerance, fog1HeightTollerance));
					}
					else
					{
						activeFog1[i].transform.position = fog1SpawnPoint;
					}
				}
			}

			// fog 2
			for (int i = 0; i < activeFog2.Count; i++)
			{
				activeFog2[i].transform.position = new Vector3(activeFog2[i].transform.position.x - speedOfFog2 * speed, activeFog2[i].transform.position.y);
				if (activeFog2[i].transform.position.x < stopForFog2)
				{
					if (fog2RandomHeight)
					{
						activeFog2[i].transform.position = new Vector3(fog2SpawnPoint.x, fog2SpawnPoint.y + Random.Range(-fog2HeightTollerance, fog2HeightTollerance));
					}
					else
					{
						activeFog2[i].transform.position = fog2SpawnPoint;
					}
				}
			}

			// fog 3
			for (int i = 0; i < activeFog3.Count; i++)
			{
				activeFog3[i].transform.position = new Vector3(activeFog3[i].transform.position.x - speedOfFog3 * speed, activeFog3[i].transform.position.y);
				if (activeFog3[i].transform.position.x < stopForFog3)
				{
					if (fog3RandomHeight)
					{
						activeFog3[i].transform.position = new Vector3(fog3SpawnPoint.x, fog3SpawnPoint.y + Random.Range(-fog3HeightTollerance, fog3HeightTollerance));
					}
					else
					{
						activeFog3[i].transform.position = fog3SpawnPoint;
					}
				}
			}

			// first plan
			for (int i = 0; i < activeFirstPlan.Count; i++)
			{
				activeFirstPlan[i].transform.position = new Vector3(activeFirstPlan[i].transform.position.x - speedOfFirstPlan * speed, activeFirstPlan[i].transform.position.y);
				if (activeFirstPlan[i].transform.position.x < stopForFirstPlan)
				{
					if (firstPlanRandomHeight)
					{
						activeFirstPlan[i].transform.position = new Vector3(firstPlanSpawnPoint.x, firstPlanSpawnPoint.y + Random.Range(-firstPlanHeightTollerance, firstPlanHeightTollerance));
					}
					else
					{
						activeFirstPlan[i].transform.position = firstPlanSpawnPoint;
					}
				}
			}

			// second plan
			for (int i = 0; i < activeSecondPlan.Count; i++)
			{
				activeSecondPlan[i].transform.position = new Vector3(activeSecondPlan[i].transform.position.x - speedOfSecondPlan * speed, activeSecondPlan[i].transform.position.y);
				if (activeSecondPlan[i].transform.position.x < stopForSecondPlan)
				{
					if (secondPlanRandomHeight)
					{
						activeSecondPlan[i].transform.position = new Vector3(secondPlanSpawnPoint.x, secondPlanSpawnPoint.y + Random.Range(-secondPlanHeightTollerance, secondPlanHeightTollerance));
					}
					else
					{
						activeSecondPlan[i].transform.position = secondPlanSpawnPoint;
					}
				}
			}

			// third plan
			for (int i = 0; i < activeThirdPlan.Count; i++)
			{
				activeThirdPlan[i].transform.position = new Vector3(activeThirdPlan[i].transform.position.x - speedOfThirdPlan * speed, activeThirdPlan[i].transform.position.y);
				if (activeThirdPlan[i].transform.position.x < stopForThirdPlan)
				{
					if (thirdPlanRandomHeight)
					{
						activeThirdPlan[i].transform.position = new Vector3(thirdPlanSpawnPoint.x, thirdPlanSpawnPoint.y + Random.Range(-thirdPlanHeightTollerance, thirdPlanHeightTollerance));
					}
					else
					{
						activeThirdPlan[i].transform.position = thirdPlanSpawnPoint;
					}
				}
			}
		}
	}




#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;

		Gizmos.DrawLine(transform.position, transform.position);
		UnityEditor.Handles.Label(transform.position, "Text");

		// clouds
		if (cloudRandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(cloudsSpawnPoint.x, cloudsSpawnPoint.y + cloudHeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(cloudsSpawnPoint.x, cloudsSpawnPoint.y - cloudHeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(cloudsSpawnPoint.x, cloudsSpawnPoint.y + cloudHeightTollerance),
				new Vector3(cloudsSpawnPoint.x, cloudsSpawnPoint.y - cloudHeightTollerance));
			UnityEditor.Handles.Label(new Vector3(cloudsSpawnPoint.x + 0.2f, cloudsSpawnPoint.y), "Clouds Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(cloudsSpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(cloudsSpawnPoint.x + 0.7f, cloudsSpawnPoint.y), "Clouds Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForClouds, -5), new Vector3(stopForClouds, 5));
		UnityEditor.Handles.Label(new Vector3(stopForClouds + 0.2f, 0), "Clouds Stop Line");


		// fog 1
		if (fog1RandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(fog1SpawnPoint.x, fog1SpawnPoint.y + fog1HeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(fog1SpawnPoint.x, fog1SpawnPoint.y - fog1HeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(fog1SpawnPoint.x, fog1SpawnPoint.y + fog1HeightTollerance),
				new Vector3(fog1SpawnPoint.x, fog1SpawnPoint.y - fog1HeightTollerance));
			UnityEditor.Handles.Label(new Vector3(fog1SpawnPoint.x + 0.2f, fog1SpawnPoint.y), "Fog 1 Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(fog1SpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(fog1SpawnPoint.x + 0.7f, fog1SpawnPoint.y), "Fog 1 Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForFog1, -5), new Vector3(stopForFog1, 5));
		UnityEditor.Handles.Label(new Vector3(stopForFog1 + 0.2f, 1f), "Fog 1 Stop Line");

		// fog 2
		if (fog2RandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(fog2SpawnPoint.x, fog2SpawnPoint.y + fog2HeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(fog2SpawnPoint.x, fog2SpawnPoint.y - fog2HeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(fog2SpawnPoint.x, fog2SpawnPoint.y + fog2HeightTollerance),
				new Vector3(fog2SpawnPoint.x, fog2SpawnPoint.y - fog2HeightTollerance));
			UnityEditor.Handles.Label(new Vector3(fog2SpawnPoint.x + 0.2f, fog2SpawnPoint.y), "Fog 2 Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(fog2SpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(fog2SpawnPoint.x + 0.7f, fog2SpawnPoint.y), "Fog 2 Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForFog2, -5), new Vector3(stopForFog2, 5));
		UnityEditor.Handles.Label(new Vector3(stopForFog2 + 0.2f, 2f), "Fog 2 Stop Line");

		// fog 3
		if (fog3RandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(fog3SpawnPoint.x, fog3SpawnPoint.y + fog3HeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(fog3SpawnPoint.x, fog3SpawnPoint.y - fog3HeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(fog3SpawnPoint.x, fog3SpawnPoint.y + fog3HeightTollerance),
				new Vector3(fog3SpawnPoint.x, fog3SpawnPoint.y - fog3HeightTollerance));
			UnityEditor.Handles.Label(new Vector3(fog3SpawnPoint.x + 0.2f, fog3SpawnPoint.y), "Fog 3 Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(fog3SpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(fog3SpawnPoint.x + 0.7f, fog3SpawnPoint.y), "Fog 3 Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForFog3, -5), new Vector3(stopForFog3, 5));
		UnityEditor.Handles.Label(new Vector3(stopForFog3 + 0.2f, 3f), "Fog 3 Stop Line");

		// first plan
		if (firstPlanRandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(firstPlanSpawnPoint.x, firstPlanSpawnPoint.y + firstPlanHeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(firstPlanSpawnPoint.x, firstPlanSpawnPoint.y - firstPlanHeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(firstPlanSpawnPoint.x, firstPlanSpawnPoint.y + firstPlanHeightTollerance),
				new Vector3(firstPlanSpawnPoint.x, firstPlanSpawnPoint.y - firstPlanHeightTollerance));
			UnityEditor.Handles.Label(new Vector3(firstPlanSpawnPoint.x + 0.2f, firstPlanSpawnPoint.y), "First Plan Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(firstPlanSpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(firstPlanSpawnPoint.x + 0.7f, firstPlanSpawnPoint.y), "First Plan Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForFirstPlan, -5), new Vector3(stopForFirstPlan, 5));
		UnityEditor.Handles.Label(new Vector3(stopForFirstPlan + 0.2f, -1f), "First Plan Stop Line");

		// second plan
		if (secondPlanRandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(secondPlanSpawnPoint.x, secondPlanSpawnPoint.y + secondPlanHeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(secondPlanSpawnPoint.x, secondPlanSpawnPoint.y - secondPlanHeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(secondPlanSpawnPoint.x, secondPlanSpawnPoint.y + secondPlanHeightTollerance),
				new Vector3(secondPlanSpawnPoint.x, secondPlanSpawnPoint.y - secondPlanHeightTollerance));
			UnityEditor.Handles.Label(new Vector3(secondPlanSpawnPoint.x + 0.2f, secondPlanSpawnPoint.y), "Second Plan Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(secondPlanSpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(secondPlanSpawnPoint.x + 0.7f, secondPlanSpawnPoint.y), "Second Plan Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForSecondPlan, -5), new Vector3(stopForSecondPlan, 5));
		UnityEditor.Handles.Label(new Vector3(stopForSecondPlan + 0.2f, -2f), "Second Plan Stop Line");

		// third plan
		if (thirdPlanRandomHeight)
		{
			Gizmos.DrawSphere(new Vector3(thirdPlanSpawnPoint.x, thirdPlanSpawnPoint.y + thirdPlanHeightTollerance), 0.25f);
			Gizmos.DrawSphere(new Vector3(thirdPlanSpawnPoint.x, thirdPlanSpawnPoint.y - thirdPlanHeightTollerance), 0.25f);
			Gizmos.DrawLine(new Vector3(thirdPlanSpawnPoint.x, thirdPlanSpawnPoint.y + thirdPlanHeightTollerance),
				new Vector3(thirdPlanSpawnPoint.x, thirdPlanSpawnPoint.y - thirdPlanHeightTollerance));
			UnityEditor.Handles.Label(new Vector3(thirdPlanSpawnPoint.x + 0.2f, thirdPlanSpawnPoint.y), "Third Plan Spawn Line");
		}
		else
		{
			Gizmos.DrawSphere(thirdPlanSpawnPoint, 0.5f);
			UnityEditor.Handles.Label(new Vector3(thirdPlanSpawnPoint.x + 0.7f, thirdPlanSpawnPoint.y), "Third Plan Spawn Point");
		}
		Gizmos.DrawLine(new Vector3(stopForThirdPlan, -5), new Vector3(stopForThirdPlan, 5));
		UnityEditor.Handles.Label(new Vector3(stopForThirdPlan + 0.2f, -3f), "Third Plan Stop Line");
	}
#endif
}
