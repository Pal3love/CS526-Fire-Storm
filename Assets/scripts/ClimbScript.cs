using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He

public class ClimbScript : MonoBehaviour {


	PlayerScript player;
	public float originalGravityScale;
	public float speed = 3;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{ 
			originalGravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
			player.GetComponent<Rigidbody2D> ().gravityScale = 0;
		}
	}
	void OnTriggerStay2D(Collider2D otherCollider)
	{
		if (player != null)
		{ 
			// if (Input.GetKey (KeyCode.UpArrow)) { // used a tag to ID collider as player
			if (CrossPlatformInputManager.GetAxis ("Vertical") > 0.4f || Input.GetKey (KeyCode.UpArrow)) {  // Replaced by Touchscreen control (Shiyu He)

				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);

			// } else if (Input.GetKey (KeyCode.DownArrow)) {
			} else if (CrossPlatformInputManager.GetAxis ("Vertical") < -0.4f || Input.GetKey (KeyCode.DownArrow)) {  // Replaced by Touchscreen control (Shiyu He)
				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed);

			} else {
				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (player != null)
		{ 
			player.GetComponent<Rigidbody2D> ().gravityScale = 5;
		}
	}

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

	}
}