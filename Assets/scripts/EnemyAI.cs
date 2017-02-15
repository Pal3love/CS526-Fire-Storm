using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI: MonoBehaviour {
    public Transform target;
    public float speed = 0.75f;
    private float initialXScale;
    public float ShootingTime = 0.5f;
    public float ShootingNeedTime = 0.5f;
    public Transform BulletPrefab1;
    public Transform BulletPrefab2;

    void Start()
    {
        initialXScale = transform.localScale.x;
    }

    void Update()
    {
        move();
        if(Mathf.Abs(transform.position.x - target.position.x) < 5.0f)
        {
            shooting();
        }
    }

    //The enemy follows the player
    void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(initialXScale, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-initialXScale, transform.localScale.y);
        }
    }

    void shooting()
    {
        if (ShootingTime <= 0)  
        {
            ShootingTime = ShootingNeedTime;  
            if(transform.position.x > target.position.x)
            {
                Transform bulletL = Instantiate(BulletPrefab1,
                transform.position, new Quaternion(0, 0, 0, 0));
            }else
            {
                Transform bulletR = Instantiate(BulletPrefab2,
                transform.position, new Quaternion(0, 0, 0, 0));
                bulletR.Rotate(0, 180, 0);
            }
            
        }
        else 
        {
            ShootingTime -= Time.deltaTime;
        }
    }





}
