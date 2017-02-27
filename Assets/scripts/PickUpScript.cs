using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

	PlayerScript player;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		Debug.Log("entered pick up");
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{ 
			Destroy(gameObject, 0);

			//add effect
		}
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
