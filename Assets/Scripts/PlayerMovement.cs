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
	[SerializeField] float jumpHeight, jumpSpeed, maxTimeInAir, fallingSpeed, timeFalling, jumpForce, jumpImpuls, fallingForce;

	float timeInAir, jumpPressedTime;
	bool jumpHolding, doubleJumped, paused;
	PlayerMovementState lastState;
	Vector3 playerPositionJumping, playerPositionFalling;
	Rigidbody2D rigidbody2d;

	private void Start()
	{
		playerState = PlayerMovementState.Running;
		playerPositionJumping = transform.position;
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (GameManager.Instance.gameState == GameState.Playing)
		{
			if (paused)
			{
				UnpauseMovement();
				paused = false;
			}
			JumpMovement();
		}
		else
		{
			paused = true;
			PauseMovement();
		}
	}

	public void PauseMovement()
	{
		lastState = playerState;
		playerState = PlayerMovementState.Stop;
		rigidbody2d.bodyType = RigidbodyType2D.Static;
	}

	public void UnpauseMovement()
	{
		playerState = lastState;
		rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
	}

	void JumpMovement()
	{
		if (playerState == PlayerMovementState.Jumping)
		{
			rigidbody2d.AddForce(new Vector2(0, -fallingSpeed));
			timeInAir += Time.deltaTime;
			if (transform.position.y > playerPositionJumping.y + jumpHeight)
			{
				playerState = PlayerMovementState.Falling;
				GameSoundManager.Instance.PlayWee();
			}
			else if (timeInAir > jumpPressedTime)
			{
				playerState = PlayerMovementState.Falling;
				GameSoundManager.Instance.PlayWee();
			}
			else
			{
				float jump = 1 - (timeInAir / jumpPressedTime);

				rigidbody2d.AddForce(new Vector2(0, jumpForce * jump));

				//transform.position = new Vector3(transform.position.x, transform.position.y + jump * jumpSpeed * Time.deltaTime);
			}
		}
		else if (playerState == PlayerMovementState.Falling)
		{
			rigidbody2d.AddForce(new Vector2(0, -fallingForce));
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
			rigidbody2d.AddForce(new Vector2(0, jumpImpuls), ForceMode2D.Impulse);
			GameSoundManager.Instance.PlayJump();
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
			rigidbody2d.velocity = new Vector2(0, 0);
			rigidbody2d.AddForce(new Vector2(0, jumpImpuls), ForceMode2D.Impulse);
			GameSoundManager.Instance.PlayJump();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			playerState = PlayerMovementState.Running;

			//GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "DeathZone")
		{
			Debug.Log("Death");
			playerState = PlayerMovementState.Stop;
			GameManager.Instance.gameState = GameState.GameOver;
			GameSoundManager.Instance.PlayDeath();
		}
		else if (collision.gameObject.tag == "Coin")
		{
			collision.GetComponent<Coin>().PickUp();
			GameSoundManager.Instance.PlayCoin();
		}
	}
}
