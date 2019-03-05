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
	[SerializeField] float jumpHeight, jumpSpeed, maxTimeInAir;

	float timeInAir, jumpPressedTime;
	bool jumpHolding, doubleJumped;
	Vector3 playerPositionJumping, playerPositionFalling;


	private void Start()
	{
		playerState = PlayerState.Running;
		playerPositionJumping = transform.position;
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
			timeInAir += Time.deltaTime;
			if (transform.position.y > playerPositionJumping.y + jumpHeight)
			{
				playerState = PlayerState.Falling;
			}
			else if (timeInAir > jumpPressedTime)
			{
				playerState = PlayerState.Falling;
			}
			else if (transform.position.y > playerPositionJumping.y + jumpHeight)
			{

			}
			else
			{
				//float jump = jumpHeight - (transform.position.y - playerPositionJumping.y) / (jumpHeight);



				float jump = 1 - (timeInAir / jumpPressedTime);

				Debug.Log("jmp speed: " + jump);
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
				transform.position = new Vector3(transform.position.x, transform.position.y - jumpSpeed * Time.deltaTime);
			}
		}

		JumpController();

		//Debug.Log(jumpPressedTime + "	" + timeInAir);
	}

	void JumpController()
	{
		//jump
		if (Input.GetButtonDown("Jump"))
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
		// button holding
		if (Input.GetButton("Jump") && playerState == PlayerState.Jumping && jumpHolding && jumpPressedTime < maxTimeInAir)
		{
			jumpPressedTime += Time.deltaTime;
		}
		// button up
		else if (Input.GetButtonUp("Jump"))
		{
			jumpHolding = false;
		}
	}
}
