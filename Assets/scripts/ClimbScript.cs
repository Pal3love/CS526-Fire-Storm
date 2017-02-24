using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbScript : MonoBehaviour {

	GameObject player; 
	bool canClimb = false; 
	float climbSpeed = 1;

	void Awake () {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other) { // send in the other collider (should always work)

		if (other.gameObject.tag == "Player") { // used a tag to ID collider as player
			canClimb = true;
			Rigidbody body = player.GetComponent<Rigidbody>();
			body.useGravity = false;

		}
	}

	void OnTriggerExit2D(Collider2D other) { // send in the other collider (should always work)

		Rigidbody body = other.GetComponent<Rigidbody>();
		if(body.tag == "Player"){
			canClimb = false;
			body.useGravity = true;

		}
	}

	// Use this for initialization
	void Start () {

		if(canClimb == true){


			if(Input.GetKey(KeyCode.Z)){
				
			}
			if(Input.GetKey(KeyCode.S)){

			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
