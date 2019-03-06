using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementState
{
	Running,
	Jumping,
	Falling,
	Stop
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerMovementState playerState;
	[SerializeField] float jumpHeight, jumpSpeed, maxTimeInAir, fallingSpeed, timeFalling;

	float timeInAir, jumpPressedTime;
	bool jumpHolding, doubleJumped;
	Vector3 playerPositionJumping, playerPositionFalling;


	private void Start()
	{
		playerState = PlayerMovementState.Running;
		playerPositionJumping = transform.position;
	}

	private void Update()
	{
		JumpMovement();
	}

	void JumpMovement()
	{
		if (playerState == PlayerMovementState.Jumping)
		{
			timeInAir += Time.deltaTime;
			if (transform.position.y > playerPositionJumping.y + jumpHeight)
			{
				playerState = PlayerMovementState.Falling;
			}
			else if (timeInAir > jumpPressedTime)
			{
				playerState = PlayerMovementState.Falling;
			}
			else
			{
				float jump = 1 - (timeInAir / jumpPressedTime);

				transform.position = new Vector3(transform.position.x, transform.position.y + jump * jumpSpeed * Time.deltaTime);
			}
		}
		else if (playerState == PlayerMovementState.Falling)
		{
			timeFalling += Time.deltaTime;
			if (transform.position.y < GameManager.Instance.groundLevel)
			{
				transform.position = new Vector3(transform.position.x, GameManager.Instance.groundLevel);
				playerState = PlayerMovementState.Running;
			}
			else
			{
				float fall = timeFalling / jumpPressedTime;
				transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed * fall * Time.deltaTime);
			}
		}
	}

	public void JumpButtonUp()
	{
		jumpHolding = false;
	}

	public void JumpHold()
	{
		if (playerState == PlayerMovementState.Jumping && jumpHolding && jumpPressedTime < maxTimeInAir)
			jumpPressedTime += Time.deltaTime;
	}

	public void JumpPressed()
	{
		if (playerState == PlayerMovementState.Running)
		{
			jumpPressedTime = 0.1f;
			timeInAir = 0;
			doubleJumped = false;
			playerPositionJumping = transform.position;
			playerState = PlayerMovementState.Jumping;
			jumpHolding = true;
			timeFalling = 0;
		}
		// double jump
		else if (!doubleJumped)
		{
			jumpPressedTime = 0.2f;
			timeInAir = 0;
			doubleJumped = true;
			playerPositionJumping = transform.position;
			playerState = PlayerMovementState.Jumping;
			jumpHolding = true;
			timeFalling = 0;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			playerState = PlayerMovementState.Running;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "DeathZone")
		{
			Debug.Log("Death");
			playerState = PlayerMovementState.Stop;
			GameManager.Instance.gameState = GameState.GameOver;
		}
	}
}
