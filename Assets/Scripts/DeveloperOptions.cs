using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperOptions : MonoBehaviour
{
	private static DeveloperOptions instance;
	public static DeveloperOptions Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<DeveloperOptions>();
			return instance;
		}
	}

	[Header("Logs")]
	public Segment segmentLog;
	public LevelManager levelManagerLog;
	public GameManager gameManagerLog;

	[Header("Canvas")]
	public Segment segmentCanvas;
	public LevelManager levelManagerCanvas;
	public GameManager gameManagerCanvas;

	public Text difficulty, segmentDifficulty, speed, timeTotal;
	[SerializeField] GameObject diff, segDiff, spd, timTot;

	private void Awake()
	{
		if (levelManagerCanvas.difficulty)
			diff.SetActive(true);
		else
			diff.SetActive(false);
		if (segmentCanvas.segmentDifficulty)
			segDiff.SetActive(true);
		else
			segDiff.SetActive(false);
		if (gameManagerCanvas.speed)
			spd.SetActive(true);
		else
			spd.SetActive(false);
		if (gameManagerCanvas.timeTotal)
			timTot.SetActive(true);
		else
			timTot.SetActive(false);
	}

	[System.Serializable]
	public struct Segment
	{
		public bool environmentGeneration;
		public bool obstaclesGeneration;
		public bool coinGeneration;
		public bool segmentDifficulty;
	}

	[System.Serializable]
	public struct LevelManager
	{
		public bool time;
		public bool difficulty;
		public bool coinGeneration;
	}

	[System.Serializable]
	public struct GameManager
	{
		public bool timeTotal;
		public bool speed;
	}
}
