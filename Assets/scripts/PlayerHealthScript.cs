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

	private PlayerScript PlayerS;

    void OnTriggerEnter2D(Collider2D other)

	{
        if (other.tag == "EnemyBullet")
        {
            life -= enemyAtk;
            if (life <= 0)
            {
				EndGameScript.endScore = PlayerS.playerScore;
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            life -= enemyAtk;

            if (life <= 0)
            {
				EndGameScript.endScore = PlayerS.playerScore;
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }

        }
    }

       void Start () {
		PlayerS = this.GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
