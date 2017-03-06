﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour {

    /// <summary>
    /// Total hitpoints
    ///// </summary>
    //public int hp = 15;

    ///// <summary>
    ///// Enemy or player?
    ///// </summary>
    //public bool isEnemy = true;

    ///// <summary>
    ///// Inflicts damage and check if the object should be destroyed
    ///// </summary>
    ///// <param name="damageCount"></param>
    //public void Damage(int damageCount)
    //{
    //	hp -= damageCount;

    //	if (hp <= 0)
    //	{
    //		// Dead!
    //		Destroy(gameObject);
    //	}
    //}

    //void OnTriggerEnter2D(Collider2D otherCollider)
    //{
    //	// Is this a shot?
    //	ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();

    //	if (shot != null)
    //	{
    //		// Avoid friendly fire
    //		if (shot.isEnemyShot != isEnemy)
    //		{
    //			Damage(shot.damage);

    //			// Destroy the shot
    //			Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
    //		}
    //	}
    //}

    public int enemyHP = 3;
    public int Atk = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
        {
            enemyHP -= Atk; //生命值-10
            if (enemyHP <= 0)
            {
                Destroy(gameObject); //摧毀物件

            }
            Destroy(col.gameObject);
        }

    }

    // Use this for initialization
    
}