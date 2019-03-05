using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Running,
	Jumping,
	Falling
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState playerState;
	[SerializeField] float jumpHeight;

	Vector3 playerPosition;


	private void Start()
	{
		playerState = PlayerState.Running;
		playerPosition = transform.position;
	}

	public void Jump()
	{

	}

	public void Slide()
	{

	}

	private void Update()
	{
		if (playerState == PlayerState.Jumping)
		{

		}
		else if (playerState == PlayerState.Falling)
		{
			if (transform.position.y < 0)
			{

			}
		}
	}
}
