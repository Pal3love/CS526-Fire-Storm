﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClimbScript : MonoBehaviour {


	PlayerScript player;
	float originalGravityScale;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		Debug.Log("entered ");
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{ 
			originalGravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
			player.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}

	void OnTriggerStay2D(Collider2D otherCollider)
	{
		Debug.Log("stay function");
		if (player != null)
		{ 
			if (Input.GetKey (KeyCode.W)) { // used a tag to ID collider as player
				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 1);

			} else if (Input.GetKey(KeyCode.S)) {
				player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -1);
			}
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{

		Debug.Log("tigger exit");
		// Is this player?
		PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{ 
			player.GetComponent<Rigidbody2D> ().gravityScale = originalGravityScale;

		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}