using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{

    public float speed = 5.0f;
    public Transform target;

    private float initialXScale;

    public Transform bulletPrefab;
    public Transform bulletPreb2;
    public float shootingSpeed = 600f;
    float shootingTime = 0.5f;
    float shootingNeedTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        initialXScale = transform.localScale.x;

    }

    // Update is called once per frame
    Transform bullet;
    Transform bullet2;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(initialXScale, transform.localScale.y);

        }
        else
        {
            transform.localScale = new Vector2(-initialXScale, transform.localScale.y);
        }

            if (Mathf.Abs(transform.position.x - target.position.x) < 5.0f)
            {
                AIShooting();
            }

        }

        
        
    
    
    void AIShooting()
    {
        if(shootingTime <= 0)
        {
            shootingTime = shootingNeedTime;
            if (transform.position.x <= target.position.x)
                bullet = Instantiate(bulletPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            else
            {
                bullet2 = Instantiate(bulletPreb2, transform.position, new Quaternion(0, 0, 0, 0));
                
            }
        }else
        {
            shootingTime -= Time.deltaTime;
        }

    }




}
