using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {
    public int life = 15;
    public int Atk = 1;
    // Use this for initialization





    void OnTriggerEnter2D(Collider2D other)
        
    {
        if(other.tag == "player")
        {
            life -= Atk;

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
