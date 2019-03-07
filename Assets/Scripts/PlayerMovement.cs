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
	[SerializeField] float jumpHeight, jumpSpeed, maxTimeInAir, fallingSpeed, timeFalling, jumpForce, jumpImpuls;

	float timeInAir, jumpPressedTime;
	bool jumpHolding, doubleJumped;
	Vector3 playerPositionJumping, playerPositionFalling;

	public float mas;

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
				//GetComponent<Rigidbody2D>().gravityScale = mas;

			}
			else if (timeInAir > jumpPressedTime)
			{
				playerState = PlayerMovementState.Falling;
				//GetComponent<Rigidbody2D>().gravityScale = mas;
			}
			else
			{
				float jump = 1 - (timeInAir / jumpPressedTime);

				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * jump));

				//transform.position = new Vector3(transform.position.x, transform.position.y + jump * jumpSpeed * Time.deltaTime);
			}
		}
		else if (playerState == PlayerMovementState.Falling)
		{
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
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpImpuls), ForceMode2D.Impulse);
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
			GetComponent<Rigidbody2D>().gravityScale = 1;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpImpuls), ForceMode2D.Impulse);
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
		}
	}
}
