﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpeedUp : MonoBehaviour {

    PlayerScript player;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this player?
        player = otherCollider.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            Destroy(gameObject, 0);
			player.initialWalkingSpeed += 0.5f;
			player.walkSpeed += 0.5f;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
