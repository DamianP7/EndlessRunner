using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	Playing,
	GameOver,
	Paused
}

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<GameManager>();
			return instance;
		}
	}

	public GameState gameState;
	public float groundLevel;
	public float speed;
	public float time = 0;
	int coins = 0, distance = 0;
	public int timeToIncrementSpeed;
	int timeToIncrement;
	public int goldCoinValue, silverCoinValue;
	public float mSpeed;
	bool gameOver = false;

	[SerializeField] GameObject background, pausedWindow, gameOverWindow;
	[SerializeField] SettingsPanel settingsPanel;

	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			coins = value;
			coinsText.text = coins.ToString();
		}
	}

	[SerializeField] Text distanceText, coinsText;

	private void Awake()
	{
		gameState = GameState.Playing;
		timeToIncrement = timeToIncrementSpeed;
		Coins = 0;
	}

	private void Update()
	{
		if (gameState == GameState.Playing)
		{
			time += Time.deltaTime;
			distance = Mathf.RoundToInt(time * speed / mSpeed);
			distanceText.text = distance.ToString() + " m";
			if (time > timeToIncrement)
			{
				timeToIncrement += timeToIncrementSpeed;
				speed += 0.1f;
			}
			if (DeveloperOptions.Instance.gameManagerCanvas.timeTotal)
				DeveloperOptions.Instance.timeTotal.text = (time / 60).ToString("00") + (time % 60).ToString("00:00");
			if (DeveloperOptions.Instance.gameManagerCanvas.speed)
				DeveloperOptions.Instance.speed.text = speed.ToString();
		}
		else if (gameState == GameState.GameOver && !gameOver)
		{
			gameOver = true;
			background.SetActive(true);
			gameOverWindow.SetActive(true);
			settingsPanel.MovePanel();
			if (SaveManager.Instance != null)
				SaveManager.Instance.AddNewRecord(coins, distance);
		}
	}

	public void Pause()
	{
		if (gameState == GameState.Playing)
		{
			gameState = GameState.Paused;
			background.SetActive(true);
			pausedWindow.SetActive(true);
			settingsPanel.MovePanel();
		}
		else if (gameState == GameState.Paused)
		{
			gameState = GameState.Playing;
			background.SetActive(false);
			pausedWindow.SetActive(false);
			settingsPanel.MovePanel();
		}
	}
}
