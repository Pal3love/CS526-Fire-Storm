using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He

public class PlayerScript : MonoBehaviour {

	public const float PLAYER_POSITION_LEFT_BOUNDARY = -24f;
	public const float PLAYER_POSITION_RIGHT_BOUNDARY = 25f;

	private Rigidbody2D rigidbodyComponent;

    public float walkSpeed = 5;
    public float shotSpeed = 8.0f;
	public float jumpSpeed;
	public Vector2 jumpVector = new Vector2(0, 230);

	public bool isGrounded;

    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D bulletPrefab2;

    public float playerHP = 15;
    public float currentHP;
    public float Atk = 1;
    public Slider playerSlider;

    public bool isClimbing = false;
    private float originalGravityScale;
    private Animator Animor;

    public bool isCircleAttack = false;
    public int CircleAtk = 1;
    public GameObject CircleGameObject;

    public GameObject MissileGameObject;
    public bool isMissile;
    public int MissileAtk = 1;

	public static int score;
	public Text scoreText;

    private float maxVerticalSpeed = 10;
	public float initialWalkingSpeed;

    private void Awake()
    {
        Animor = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		this.initialWalkingSpeed = this.walkSpeed;
		PlayerScript.score = 0;
		scoreText.text = "SCORE: " + PlayerScript.score.ToString();
        currentHP = playerHP;
        playerSlider.value = currentHP / playerHP;
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        originalGravityScale = rigidbodyComponent.gravityScale;
        CircleGameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        // float horizontalInput = Input.GetAxis("Horizontal");  // Keyboard Support
		float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");  // Touchscreen Support

        // Shiyu He: Set boundry for the player
        Vector3 currPlayerPos = transform.position;
		if (currPlayerPos.x < PLAYER_POSITION_LEFT_BOUNDARY) {
			currPlayerPos.x = PLAYER_POSITION_LEFT_BOUNDARY;
		}
		if (currPlayerPos.x > PLAYER_POSITION_RIGHT_BOUNDARY) {
			currPlayerPos.x = PLAYER_POSITION_RIGHT_BOUNDARY;
		}
		transform.position = currPlayerPos;

        rigidbodyComponent.velocity = new Vector2(horizontalInput * walkSpeed, rigidbodyComponent.velocity.y);

        if(horizontalInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if(horizontalInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        if(!isClimbing)
            Animor.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalInput));

        // 5 - Jumping
		// Use Input.GetKey(KeyCode.UpArrow) for keyboard support
		if (CrossPlatformInputManager.GetAxis("Vertical") > 0.4f && isGrounded) {
			rigidbodyComponent.AddForce (jumpVector, ForceMode2D.Force);
            isGrounded = false;
		}

		// 6 - Shooting
		bool shoot = CrossPlatformInputManager.GetButtonDown("Shoot");  // Replaced by touchscreen control (Shiyu He)
		// bool shoot = Input.GetButtonDown("Fire1");
		// shoot |= Input.GetButtonDown("Fire2");
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

            if (isMissile)
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber <= 20)
                {
                    GameObject missile = Instantiate(MissileGameObject, transform.position, Quaternion.identity);
                    if (missile)
                        missile.GetComponent<MissileScript>().Atk = MissileAtk;
                }
            }

            Animor.SetTrigger("Shooting");
        }

        Animor.SetBool("isClimbing", isClimbing);

        // Circle Attack
        if(isCircleAttack)
        {
            CircleGameObject.SetActive(true);
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemyList.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (dis < 3.4 && dis > 3.3)
                    enemyList[i].GetComponent<EnemyAIScript>().currentHP -= CircleAtk;
            }
        }

        playerSlider.value = currentHP / playerHP;
    }

	void FixedUpdate()
	{
		// Shiyu He: Update score
		scoreText.text = "SCORE: " + PlayerScript.score.ToString();

		// Shiyu He: Get blink!
		// Use Input.GetAxis("Horizontal") for keyboard support
		if (!this.isClimbing && CrossPlatformInputManager.GetButtonDown ("Blink") && CrossPlatformInputManager.GetAxis ("Horizontal") != 0) {
			this.walkSpeed = 30f;
		}

		// Shiyu He: Updata blinking speed
		if (walkSpeed > this.initialWalkingSpeed) {
			if (CrossPlatformInputManager.GetAxis("Horizontal") == 0) {
				this.walkSpeed = this.initialWalkingSpeed;
			} else {
				this.walkSpeed -= 1f;
			}
		}

		// 5 - Get the component and store the reference
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // limit vertical speed
        if(rigidbodyComponent.velocity.y > maxVerticalSpeed)
            rigidbodyComponent.velocity = new Vector2(rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.normalized.y * maxVerticalSpeed);
    }

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.tag == "Rope") {
			if (!isClimbing) {
				isClimbing = true;
                rigidbodyComponent.gravityScale = 0;
			}
		}

        if (otherCollider.tag == "EnemyBullet")
        {
            currentHP -= otherCollider.GetComponent<EnemyBulletScript>().enemyAtk;
            if (currentHP <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }
            Destroy(otherCollider.gameObject);
        }

        if (otherCollider.tag == "Enemy")
        {
            currentHP -= otherCollider.GetComponent<EnemyAIScript>().Atk;
            if (currentHP <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("EndGame");
            }
            otherCollider.GetComponent<EnemyAIScript>().currentHP = 0;
        }
    }

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (otherCollider.tag == "Rope") {
			if (isClimbing) {
				isClimbing = false;
                rigidbodyComponent.gravityScale = originalGravityScale;
			}
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3.4f);
    }
}
