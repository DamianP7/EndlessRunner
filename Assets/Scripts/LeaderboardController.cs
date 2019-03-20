using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
	[SerializeField] LocalRecord[] localRecords;
	[SerializeField] GlobalRecord[] globalRecords;
	public bool global;

	int index;
	List<SaveSystem.RecordOnLeaderboard> recordsInSave;

	private void OnEnable()
	{
		if (global)
			ShowGlobal();
		else
			ShowLocal();
	}

	public void ShowLeaderboard()
	{
		if (!global)
		{
			int max = index + localRecords.Length < recordsInSave.Count ? index + localRecords.Length : recordsInSave.Count - index;
			int recIndex = 0;
			for (int i = index; i < max; i++)
			{
				localRecords[recIndex].gameObject.SetActive(true);
				localRecords[recIndex].Number = i+1;
				localRecords[recIndex].Coins = recordsInSave[i].coins;
				localRecords[recIndex].Distance = recordsInSave[i].distance;
				recIndex++;
			}
			for (; recIndex < localRecords.Length; recIndex++)
			{
				localRecords[recIndex].gameObject.SetActive(false);
			}
		}
	}

	public void ShowLocal()
	{
		global = false;
		index = 0;

		recordsInSave = SaveManager.Instance.GetSave().leaderboard;

		ShowLeaderboard();
	}

	public void ShowGlobal()
	{
		global = true;
		index = 0;
	}

	public void GoDown()
	{
		int maxRecords;
		if (global)
			maxRecords = globalRecords.Length;
		else
			maxRecords = localRecords.Length;

		if (index + maxRecords < recordsInSave.Count)
		{
			index += maxRecords;
		}
		else
		{
			index = recordsInSave.Count - maxRecords;
		}
		Debug.Log("index: " + index);
	}

	public void GoUp()
	{
		int maxRecords;
		if (global)
			maxRecords = globalRecords.Length;
		else
			maxRecords = localRecords.Length;

		if (index - maxRecords > 0)
		{
			index -= maxRecords;
		}
		else
		{
			index = 0;
		}
		Debug.Log("index: " + index);
	}

	public void CloseWindow()
	{
		gameObject.SetActive(false);
	}
}
