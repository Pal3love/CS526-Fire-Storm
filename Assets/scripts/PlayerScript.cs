﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He
using UnityEngine.UI;  // Added by Shiyu He for score support

public class PlayerScript : MonoBehaviour {

	public const float PLAYER_POSITION_LEFT_BOUNDARY = -24f;
	public const float PLAYER_POSITION_RIGHT_BOUNDARY = 25f;

	public Text scoreText;
	public long playerScore;

	public Rigidbody2D rigidbodyComponent;

	public float walkSpeed;
	public int Atk = 1;
	public float scale;
    public float shotSpeed = 8.0f;
	public float jumpSpeed;
	public Vector2 jumpVector = new Vector2(0, 230);
	public bool isGrounded;
	public float radiuss;
	public LayerMask ground;
    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D bulletPrefab2;

	private bool isClimbing = false;
	private bool isWalking = false;
	private float originalWalkingSpeed;
    
	// Use this for initialization
	void Start () {
		this.playerScore = 0;
		this.originalWalkingSpeed = walkSpeed;
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

		// if(Input.GetKey(KeyCode.RightArrow)){
		if (CrossPlatformInputManager.GetAxis ("Horizontal") > 0.4f || Input.GetKey(KeyCode.RightArrow)) {  // Replaced by touchscreen control (Shiyu He)
			this.isWalking = true;
			rigidbodyComponent.velocity = new Vector2 (walkSpeed,rigidbodyComponent.velocity.y);
			transform.localScale = new Vector3 (scale, scale, 1);

		// } else if (Input.GetKey(KeyCode.LeftArrow)) {
		} else if (CrossPlatformInputManager.GetAxis ("Horizontal") < -0.4f || Input.GetKey(KeyCode.LeftArrow)) {  // Replaced by touchscreen control (Shiyu He)
			this.isWalking = true;
			rigidbodyComponent.velocity = new Vector2 (-walkSpeed,rigidbodyComponent.velocity.y);
			transform.localScale = new Vector3 (-scale, scale, 1);
		} else {
			this.isWalking = false;
			rigidbodyComponent.velocity = new Vector2 (0,rigidbodyComponent.velocity.y);
		}

		// Blinking boost, added by Shiyu He
		if (CrossPlatformInputManager.GetButtonDown ("Blink") && isWalking) {
			this.walkSpeed = 35f;
		}
		if (this.walkSpeed > this.originalWalkingSpeed) {
			this.walkSpeed -= 1.5f;
		}

		// 5 - Jumping
<<<<<<< HEAD
		isGrounded = Physics2D.OverlapCircle(grounder.transform.position,radiuss,ground);
		if((CrossPlatformInputManager.GetAxis ("Vertical") > 0.4f || Input.GetKey(KeyCode.UpArrow)) && isGrounded){  // Replaced by touchscreen control (Shiyu He)
		// if(Input.GetKey(KeyCode.UpArrow) && isGrounded){
=======
		if(Input.GetKey(KeyCode.UpArrow) && isGrounded){
>>>>>>> origin/master
			rigidbodyComponent.AddForce (jumpVector, ForceMode2D.Force);
            isGrounded = false;
		}


		// 6 - Shooting
	    // bool shoot = Input.GetButtonDown("Fire1");
	    // shoot |= Input.GetButtonDown("Fire2");
		bool shoot = CrossPlatformInputManager.GetButtonDown("Shoot") || Input.GetButtonDown("Fire1");  // Replaced by touchscreen control (Shiyu He)
		shoot |= CrossPlatformInputManager.GetButtonDown("Shoot") || Input.GetButtonDown("Fire2");
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
	}

	void FixedUpdate()
	{
		// Get current score and print it
		this.scoreText.text = "SCORE: " + this.playerScore.ToString();

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
}
