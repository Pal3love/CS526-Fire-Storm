﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour {

	PlayerScript player;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

        if (player != null)
		{ 
			Destroy(gameObject, 0);
            player.Atk++;
		}
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
