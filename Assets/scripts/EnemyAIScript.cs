using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float enemyHP = 3;
    public float currentHP;
    public float Atk = 1;
    public Slider healthBar;
    // Use this for initialization
    void Start()
    {
       
        rgbd = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];

        currentHP = enemyHP;
        healthBar.value = currentHP/enemyHP;
    }

    // Update is called once per frame

    void Update()
    {
        Follow(); 
        if(Mathf.Abs(target.transform.position.x - transform.position.x) <5.0f)
        {
            Shoot();
        }

        if (currentHP <= 0)
            Destroy(gameObject);
    }

    void Follow()
    {
        playerPos = target.transform.position;
        selfPos = transform.position;
        follow = Vector2.MoveTowards(selfPos, playerPos, moveSpeed);
        rgbd.MovePosition(follow);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
        {
            currentHP -= target.GetComponent<PlayerScript>().Atk;
            healthBar.value = currentHP / enemyHP;
            if (currentHP <= 0)
            {
                Destroy(gameObject);
            }
           
            Destroy(col.gameObject);
        }
    }

    void Shoot()
    {
        if (shootingTime <= 0)
        {
            shootingTime = shootingNeedTime;
            Rigidbody2D bullet = Instantiate(bulletPrefab) as Rigidbody2D;
            bullet.position = transform.position;
            bullet.GetComponent<EnemyBulletScript>().enemyAtk = Atk;
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
