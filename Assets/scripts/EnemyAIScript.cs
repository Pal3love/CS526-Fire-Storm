using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    public GameObject target;
    float shootingTime = 0.5f;
    float shootingNeedTime = 1.0f;
   
    public Rigidbody2D rgbd;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 3.0f;
    public Vector2 playerPos;
    public Vector2 selfPos;
    public Vector2 follow;
    // Use this for initialization
    void Start()
    {
       
        rgbd = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame

    void Update()
    {
        Follow(); 
        if(Mathf.Abs(target.transform.position.x - transform.position.x) < 3.0f)
        {
            Shoot();
        }
    }

    void Follow()
    {
        playerPos = target.transform.position;
        selfPos = transform.position;
        follow = Vector2.MoveTowards(selfPos, playerPos, moveSpeed);
        rgbd.MovePosition(follow);
    }

    
        void Shoot()
    {
            if (shootingTime <= 0)
            {
                shootingTime = shootingNeedTime;
                Rigidbody2D bullet = Instantiate(bulletPrefab) as Rigidbody2D;
                bullet.position = transform.position;
                if (transform.position.x < target.transform.position.x)
                {
                    bullet.velocity = new Vector2(bulletSpeed, 0);
                }
                else
                {
                    bullet.velocity = new Vector2(-bulletSpeed, 0);
                }

            }
            else
            {
                shootingTime -= Time.deltaTime;
            }



        }
}
