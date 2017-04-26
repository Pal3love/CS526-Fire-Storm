using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealthUp : MonoBehaviour {

    PlayerScript player;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this player?
        player = otherCollider.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            Destroy(gameObject, 0);
            player.currentHP += 2;
            player.currentHP = (player.currentHP > player.playerHP) ? player.playerHP : player.currentHP;
        }

        if(otherCollider.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
