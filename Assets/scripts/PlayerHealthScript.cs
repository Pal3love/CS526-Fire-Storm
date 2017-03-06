using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour {
    public int life = 5;
    // Use this for initialization
    public int enemyAtk = 1;
    // Use this for initialization
	//different Atk_value when pick up different equipment 
	//public int Atk_value_1 = 2;

    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "EnemyBullet")
        {
            life -= enemyAtk;
            if (life <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(1);
            }
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            life -= enemyAtk;

            if (life <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

       void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
