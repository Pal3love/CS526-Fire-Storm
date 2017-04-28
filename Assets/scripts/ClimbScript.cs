using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He

public class ClimbScript : MonoBehaviour {


	private PlayerScript player;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

	public float climpSpeed = 3;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponent<Animator>();
        }
	}
	void OnTriggerStay2D(Collider2D otherCollider)
	{
		if (player != null)
		{
            // float VerticalInput = Input.GetAxis("Vertical");  // Keyboard Support
			float VerticalInput = CrossPlatformInputManager.GetAxis("Vertical");  // Touchscreen Support

            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, VerticalInput * climpSpeed);

            //         if (CrossPlatformInputManager.GetAxis ("Vertical") > 0.4f || Input.GetKey (KeyCode.UpArrow)) {  // Replaced by Touchscreen control (Shiyu He)

            //	player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, climpSpeed);

            //} else if (CrossPlatformInputManager.GetAxis ("Vertical") < -0.4f || Input.GetKey (KeyCode.DownArrow)) {  // Replaced by Touchscreen control (Shiyu He)
            //	player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -climpSpeed);

            //} else {
            //	player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
            //}

            playerAnimator.SetFloat("VerticalSpeed", Mathf.Abs(VerticalInput));
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (player != null)
		{ 
		}
	}
}