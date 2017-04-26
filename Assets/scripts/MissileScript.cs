using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

	public float speed = 5;
	public float rotatingSpeed= 200;
    public float Atk;
	public GameObject[] targets;
	public GameObject target;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		if (targets == null) {
			targets = GameObject.FindGameObjectsWithTag("Enemy");
		}

		rb = GetComponent<Rigidbody2D> ();
//		if (rb == null) {
//			Debug.Log ("can not load missile");
//		} else {
//			Debug.Log (rb.name + " loaded");
//		}

	}

	// Update is called once per frame
	void FixedUpdate () {

        targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length == 0)
        {
            Destroy(gameObject);
        }
        else {
            //find the nearest enemy
            float minDistance = float.MaxValue;
            for (int i = 0; i < targets.Length; i++)
            {
                Vector2 point2Target = (Vector2)transform.position - (Vector2)targets[i].transform.position;
                point2Target.Normalize();

                float value = Vector3.Cross(point2Target, transform.right).z;

                if (value < minDistance)
                    minDistance = value;
            }

            if (minDistance != float.MaxValue)
            {
                rb.angularVelocity = rotatingSpeed * minDistance;

                rb.velocity = transform.right * speed;
            }
        }
	}


	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Enemy") {

            other.GetComponent<EnemyAIScript>().currentHP -= Atk;
            Destroy(gameObject);
		}

	}
		
}
