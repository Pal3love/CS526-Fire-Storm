using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCircleAttack : MonoBehaviour {

    PlayerScript player;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        player = otherCollider.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            Destroy(gameObject, 0);
            player.isCircleAttack = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
