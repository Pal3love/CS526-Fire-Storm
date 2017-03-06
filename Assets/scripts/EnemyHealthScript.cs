using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour {

    public float enemyHP = 3;
    public float currentHP;
    public float Atk = 1;
    public Slider healthBar;
    
	void Start () {
        currentHP = enemyHP;
        healthBar.value = caculateHealth();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
        {
           currentHP -= Atk; //生命值-10

            healthBar.value = currentHP/enemyHP;
            if (currentHP <= 0)
            {
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
