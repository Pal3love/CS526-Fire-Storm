﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClimbScript : MonoBehaviour {


	PlayerScript player;
	public float originalGravityScale;
	public float speed = 5;

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
			if (Input.GetKey (KeyCode.UpArrow)) { // used a tag to ID collider as player

				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);


			} else if (Input.GetKey (KeyCode.DownArrow)) {
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
			player.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
	}

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

	}
}