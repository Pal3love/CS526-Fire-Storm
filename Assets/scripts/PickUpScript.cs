using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

	PlayerScript player;
    PlayerHealthScript damage;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		Debug.Log("entered pick up");
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();
        damage = otherCollider.gameObject.GetComponent<PlayerHealthScript>();

        if (player != null)
		{ 
			Destroy(gameObject, 0);
            if (damage != null)
            {
                damage.enemyAtk++;
            }//add effect
		}
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
