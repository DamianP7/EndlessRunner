using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	Playing,
	GameOver
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
	int coins = 0;
	public int goldCoinValue, silverCoinValue;

	public float mSpeed;

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
	}

	private void Update()
	{
		time += Time.deltaTime * speed / mSpeed;
		distanceText.text = Mathf.RoundToInt(time).ToString() + " m";
	}

}
