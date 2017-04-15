using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour {

    public float enemyHP = 10;
    public float currentHP;
    public Slider healthBar;
	private long enemyScore;  // Added by Shiyu He

	private PlayerScript PlayerS;
    
	void Start () {
        currentHP = enemyHP;
        healthBar.value = caculateHealth();
		PlayerS = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerScript> ();
		enemyScore = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
		{
           currentHP -= PlayerS.Atk; //生命值-10
			PlayerS.playerScore += PlayerS.Atk * 2;  //中一枪加小分

            healthBar.value = currentHP/enemyHP;
            if (currentHP <= 0)
            {
				PlayerS.playerScore += 50;  //敌人挂掉加大分
                Destroy(gameObject); //摧毀物件

            }
            Destroy(col.gameObject);
        }

    }

    float caculateHealth()
    {
        return currentHP / enemyHP;
    }

}
