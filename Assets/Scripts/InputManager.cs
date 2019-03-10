using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
	[SerializeField] PlayerMovement playerMovement;
	bool jumpClicked = false;

	void Update()
	{
		JumpKeyboardController();
		if (jumpClicked)
			playerMovement.JumpHold();
	}

	void JumpKeyboardController()
	{
		if (GameManager.Instance.gameState == GameState.Playing)
		{
			//jump
			if (Input.GetButtonDown("Jump"))
			{
				playerMovement.JumpPressed();
			}
			// button holding
			if (Input.GetButton("Jump"))
			{
				playerMovement.JumpHold();
			}
			// button up
			else if (Input.GetButtonUp("Jump"))
			{
				playerMovement.JumpButtonUp();
			}
		}
		if (Input.GetButtonDown("Cancel"))
		{
			GameManager.Instance.Pause();
		}
	}

	public void JumpPressed()
	{
		if (!jumpClicked)
		{
			jumpClicked = true;
			playerMovement.JumpPressed();
		}
	}

	public void JumpUp()
	{
		jumpClicked = false;
		playerMovement.JumpButtonUp();
	}

	public void Replay()
	{
		SceneManager.LoadScene(1); 
	}
}
