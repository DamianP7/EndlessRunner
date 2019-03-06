using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
