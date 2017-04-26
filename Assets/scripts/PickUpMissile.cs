using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMissile : MonoBehaviour {

    PlayerScript player;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this player?
        player = otherCollider.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            Destroy(gameObject, 0);
            if (player.isMissile)
                player.MissileAtk++;
            else
                player.isMissile = true;
        }
    }

}
