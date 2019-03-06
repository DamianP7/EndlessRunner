using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
	[SerializeField] PlayerMovement playerMovement;

	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
    {
		if (playerMovement.playerState == PlayerMovementState.Stop)
		{
			animator.enabled = false;
		}

	}
}
