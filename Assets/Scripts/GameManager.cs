using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Awake()
	{
		gameState = GameState.Playing;
	}

}
