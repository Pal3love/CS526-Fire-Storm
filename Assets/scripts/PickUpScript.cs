using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

	PlayerScript player;
	PlayerHealthScript playerAttribute;

	int pillVaule = 5;

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		Debug.Log("entered pick up");
		// Is this player?
		player = otherCollider.gameObject.GetComponent<PlayerScript>();
		playerAttribute = otherCollider.gameObject.GetComponent<PlayerHealthScript>();
//		recover = 

        if (player != null)
		{ 
			Destroy(gameObject, 0);

			//if picking up orange
			if (playerAttribute != null)
            {
				playerAttribute.enemyAtk++;  //increase attack
            }//add effect

			//if picking up pill
			if (this.gameObject.CompareTag ("Pill"))
			{
				Debug.Log("before: " + playerAttribute.life);
				playerAttribute.life += pillVaule;
				Debug.Log("after: " + playerAttribute.life);
			}

		}
	}
		
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
