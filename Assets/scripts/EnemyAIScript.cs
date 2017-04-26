using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIScript : MonoBehaviour
{

    public float moveSpeed = 0.01f;
    public GameObject target;
    float shootingTime = 0.5f;
    public float shootingNeedTime = 1.0f;
   
    public Rigidbody2D rgbd;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 3.0f;
    public Vector2 playerPos;
    public Vector2 selfPos;
    public Vector2 follow;
  
    private Vector2 enemyToPlayer;

    public float enemyHP = 3;
    public float currentHP;
    public float Atk = 1;
    public Slider healthBar;


	public GameObject Pill; 

    private Animator enemyBeeAnimator;
    // Use this for initialization
    void Start()
    {
       
        rgbd = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];

        currentHP = enemyHP;
        healthBar.value = currentHP/enemyHP;

        enemyBeeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

        if(Vector2.Distance(target.transform.position, transform.transform.position) < 5.0f)
       // if(Mathf.Abs(target.transform.position.x - transform.position.x) <5.0f)

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
        enemyToPlayer = playerPos - selfPos;
        if (enemyToPlayer.x > 0)
            enemyBeeAnimator.SetBool("isFaceRight", true);
        else
            enemyBeeAnimator.SetBool("isFaceRight", false);

        follow = Vector2.MoveTowards(selfPos, playerPos, moveSpeed);
        rgbd.MovePosition(follow);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
        {
            currentHP -= target.GetComponent<PlayerScript>().Atk;
            //healthBar.value = currentHP / enemyHP;
            healthBar.value = currentHP / enemyHP;
            if (currentHP <= 0)
            {
                Destroy(gameObject);

//				int randomNumber = Random.Range(0, 100);
//				if (randomNumber >= 50) {
					Instantiate (Pill, new Vector2 (rgbd.transform.position.x, rgbd.transform.position.y), Quaternion.identity);
//				}
            }
           
            Destroy(col.gameObject);
        }
    }

    void Shoot()
    {
        playerPos = target.transform.position;
        if (shootingTime <= 0)
        {
            shootingTime = shootingNeedTime;
            Rigidbody2D bullet = Instantiate(bulletPrefab) as Rigidbody2D;
            bullet.position = transform.position;

            bullet.GetComponent<EnemyBulletScript>().enemyAtk = Atk;

            Vector2 transPos = new Vector2(transform.position.x, transform.position.y);
               Vector2 moveFollow  = playerPos - transPos;
                bullet.velocity = moveFollow * 0.8f;

            
        }
        else
        {
            shootingTime -= Time.deltaTime;
        }
    }
}
