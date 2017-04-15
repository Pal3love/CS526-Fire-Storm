using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He

public class PlayerScript : MonoBehaviour {

	public const float PLAYER_POSITION_LEFT_BOUNDARY = -24f;
	public const float PLAYER_POSITION_RIGHT_BOUNDARY = 25f;

    public static PlayerScript playerScript;

	public Rigidbody2D rigidbodyComponent;

	public float walkSpeed;
	public float scale;
    public float shotSpeed = 8.0f;
	public float jumpSpeed;
	public Vector2 jumpVector = new Vector2(0, 230);
	public bool isGrounded;
	public float radiuss;
	public LayerMask ground;
    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D bulletPrefab2;

    public int playerHP = 10;
    public int currentHP;
    public int Atk = 1;
    public Slider playerSlider;

    private bool isClimbing = false;
    public bool isCircleAttack = false;
    
	// Use this for initialization
	void Start () {
        currentHP = playerHP;
        playerSlider.value = currentHP / playerHP;
    }
	
	// Update is called once per frame
	void Update () {
		// 3 - Retrieve axis information
//		float inputX = Input.GetAxis("Horizontal");
//		float inputY = Input.GetAxis("Vertical");
//		float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");  // Replaced by touchscreen control (Shiyu He)
//		float inputY = CrossPlatformInputManager.GetAxis ("Vertical");  // Replaced by touchscreen control (Shiyu He)

		// Shiyu He: Set boundry for the player
		Vector3 currPlayerPos = transform.position;
		if (currPlayerPos.x < PLAYER_POSITION_LEFT_BOUNDARY) {
			currPlayerPos.x = PLAYER_POSITION_LEFT_BOUNDARY;
		}
		if (currPlayerPos.x > PLAYER_POSITION_RIGHT_BOUNDARY) {
			currPlayerPos.x = PLAYER_POSITION_RIGHT_BOUNDARY;
		}
		transform.position = currPlayerPos;

		if(Input.GetKey(KeyCode.RightArrow)){
			
			rigidbodyComponent.velocity = new Vector2 (walkSpeed,rigidbodyComponent.velocity.y);
			transform.localScale = new Vector3 (scale, scale, 1);

		}else if(Input.GetKey(KeyCode.LeftArrow)){

			rigidbodyComponent.velocity = new Vector2 (-walkSpeed,rigidbodyComponent.velocity.y);
			transform.localScale = new Vector3 (-scale, scale, 1);
		}
		else{
			rigidbodyComponent.velocity = new Vector2 (0,rigidbodyComponent.velocity.y);
		}
			

		// 5 - Jumping
		if(Input.GetKey(KeyCode.UpArrow) && isGrounded){
			rigidbodyComponent.AddForce (jumpVector, ForceMode2D.Force);
            isGrounded = false;
		}


		// 6 - Shooting
//		bool shoot = CrossPlatformInputManager.GetButton("FIRE");  // Replaced by touchscreen control (Shiyu He)
		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");
		// Careful: For Mac users, ctrl + arrow is a bad idea

		if (!isClimbing && shoot)
		{
			
            if (transform.localScale.x > 0)
            {
                Rigidbody2D bullet = Instantiate(bulletPrefab1) as Rigidbody2D;
                bullet.position = transform.position;
                bullet.velocity = new Vector2(shotSpeed, 0);
                
            }
            else
            {
                Rigidbody2D bullet = Instantiate(bulletPrefab2) as Rigidbody2D;
                bullet.position = transform.position;
                bullet.velocity = new Vector2(-shotSpeed, 0);
            }
            WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				// false because the player is not an enemy
				weapon.Attack(false);
			}
		}


        // Circle Attack
        if(isCircleAttack)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemyList.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (dis < 3.1 && dis > 3.0)
                    enemyList[i].GetComponent<EnemyAIScript>().currentHP--;
            }
        }
	}

	void FixedUpdate()
	{
		// 5 - Get the component and store the reference
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		// 6 - Move the game object
//		rigidbodyComponent.velocity = movement;
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.tag == "Rope") {
			if (!isClimbing) {
				isClimbing = true;
			}
		}

            if (otherCollider.tag == "EnemyBullet")
            {
                currentHP -= otherCollider.GetComponent<EnemyBulletScript>().enemyAtk;
                playerSlider.value = currentHP / playerHP;
                if (currentHP <= 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("EndGame");
                }
                Destroy(otherCollider.gameObject);
            }

            if (otherCollider.tag == "Enemy")
            {
                currentHP -= otherCollider.GetComponent<EnemyBulletScript>().enemyAtk;
                playerSlider.value = currentHP / playerHP;
                if (currentHP <= 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("EndGame");
                }

            }
    }

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (otherCollider.tag == "Rope") {
			if (isClimbing) {
				isClimbing = false;
			}
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 3);
    }
}
