using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Right Move
public class BulletRightMove : MonoBehaviour {
    public float BulletSpeed;
    Transform target;

    
	// Use this for initialization
	void Start () {
        BulletSpeed = 8.0f;
	}
    Vector2 position;
    // Update is called once per frame
    void Update () {

        position = transform.position;
  
            position = new Vector2(position.x + BulletSpeed * Time.deltaTime, position.y);
 
            transform.position = position;

    }
}
