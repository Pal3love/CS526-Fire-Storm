using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAddHP : MonoBehaviour {

	public int pillVaule = 5;
	int playerMaxHP;
	int playerCurrentHP;
	PlayerScript player;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();

		if (player != null)
		{ 
			Destroy(gameObject, 0);

			playerMaxHP = player.GetComponent<PlayerScript> ().playerHP;
			playerCurrentHP = player.GetComponent<PlayerScript> ().currentHP;

			playerCurrentHP + pillVaule <= playerMaxHP ? playerCurrentHP += pillVaule : playerCurrentHP = playerMaxHP;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
