using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour {
    public float life;
    public float curLife;
    // Use this for initialization
    public float enemyAtk = 1;
    public Slider playerSlider;
    // Use this for initialization
<<<<<<< HEAD
	//different Atk_value when pick up different equipment 
	//public int Atk_value_1 = 2;

	private PlayerScript PlayerS;

=======
    //different Atk_value when pick up different equipment 
    //public int Atk_value_1 = 2;
    void Start()
    {
        curLife = life;
        playerSlider.value = curLife / life;
    }
>>>>>>> origin/master
    void OnTriggerEnter2D(Collider2D other)

	{
        if (other.tag == "EnemyBullet")
        {
            curLife -= enemyAtk;
            playerSlider.value = curLife / life;
            if (curLife <= 0)
            {
				EndGameScript.endScore = PlayerS.playerScore;
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            curLife -= enemyAtk;
            playerSlider.value = curLife / life;
            if (curLife <= 0)
            {
				EndGameScript.endScore = PlayerS.playerScore;
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }

        }
    }

<<<<<<< HEAD
       void Start () {
		PlayerS = this.GetComponent<PlayerScript> ();
	}
=======
      
>>>>>>> origin/master
	
	// Update is called once per frame
	void Update () {
		
	}
}
