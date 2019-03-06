using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Running,
	Jumping,
	Falling,
	Stop
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState playerState;
	[SerializeField] float jumpHeight, jumpSpeed, maxTimeInAir, fallingSpeed;

	float timeInAir, jumpPressedTime;
	bool jumpHolding, doubleJumped;
	Vector3 playerPositionJumping, playerPositionFalling;


	private void Start()
	{
		playerState = PlayerState.Running;
		playerPositionJumping = transform.position;
	}
	
	private void Update()
	{
		JumpMovement();
	}

	void JumpMovement()
	{
		if (playerState == PlayerState.Jumping)
		{
			timeInAir += Time.deltaTime;
			if (transform.position.y > playerPositionJumping.y + jumpHeight)
			{
				playerState = PlayerState.Falling;
			}
			else if (timeInAir > jumpPressedTime)
			{
				playerState = PlayerState.Falling;
			}
			else
			{
				float jump = 1 - (timeInAir / jumpPressedTime);
				
				transform.position = new Vector3(transform.position.x, transform.position.y + jump * jumpSpeed * Time.deltaTime);
			}
		}
		else if (playerState == PlayerState.Falling)
		{
			if (transform.position.y < GameManager.Instance.groundLevel)
			{
				transform.position = new Vector3(transform.position.x, GameManager.Instance.groundLevel);
				playerState = PlayerState.Running;
			}
			else
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed * Time.deltaTime);
			}
		}
	}

	public void JumpButtonUp()
	{
		jumpHolding = false;
	}

	public void JumpHold()
	{
		if (playerState == PlayerState.Jumping && jumpHolding && jumpPressedTime < maxTimeInAir)
			jumpPressedTime += Time.deltaTime;
	}

	public void JumpPressed()
	{
		if (playerState == PlayerState.Running)
		{
			jumpPressedTime = 0.1f;
			timeInAir = 0;
			doubleJumped = false;
			playerPositionJumping = transform.position;
			playerState = PlayerState.Jumping;
			jumpHolding = true;
		}
		// double jump
		else if (!doubleJumped)
		{
			jumpPressedTime = 0.2f;
			timeInAir = 0;
			doubleJumped = true;
			playerPositionJumping = transform.position;
			playerState = PlayerState.Jumping;
			jumpHolding = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			playerState = PlayerState.Running;
		else if (collision.gameObject.tag == "DeathZone")
		{
			playerState = PlayerState.Stop;
			GameManager.Instance.gameState = GameState.GameOver;
		}
	}
}
