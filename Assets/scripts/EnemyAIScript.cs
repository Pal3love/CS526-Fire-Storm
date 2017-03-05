using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{

    public float moveSpeed = 0.01f;
    public GameObject target;

    private float initialXScale;
    public float shootingSpeed = 600f;
    float shootingTime = 0.5f;
    float shootingNeedTime = 1.0f;
    
    public Rigidbody2D rgbd;
    public Vector2 playerPos;
    public Vector2 selfPos;
    public Vector2 follow;
    // Use this for initialization
    void Start()
    {
        initialXScale = -transform.localScale.x;
        rgbd = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame

    void Update()
    {
        Follow(); 
        if(Mathf.Abs(target.transform.position.x - transform.position.x) < 1.0f)
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
        }
        //void AIShooting()
        //{

        //        if (transform.position.x <= target.position.x)
        //            bullet = Instantiate(bulletPrefab, transform.position, new Quaternion(0, 0, 0, 0));
        //        else
        //        {
        //            bullet2 = Instantiate(bulletPreb2, transform.position, new Quaternion(0, 0, 0, 0));

        //        }
        //    }else
        //    {
        //        shootingTime -= Time.deltaTime;
        //    }

        //}



    }
}
