using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;  // Added by Shiyu He

public class PlayerScript : MonoBehaviour {

	public Rigidbody2D rigidbodyComponent;

	public float walkSpeed;
	public float scale;

	public Vector2 jumpVector;
	public bool isGrounded;
	public Transform grounder;
	public float radiuss;
	public LayerMask ground;
    



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 3 - Retrieve axis information
//		float inputX = Input.GetAxis("Horizontal");
//		float inputY = Input.GetAxis("Vertical");
//		float inputX = CrossPlatformInputManager.GetAxis ("Horizontal");  // Replaced by touchscreen control (Shiyu He)
//		float inputY = CrossPlatformInputManager.GetAxis ("Vertical");  // Replaced by touchscreen control (Shiyu He)

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
		isGrounded = Physics2D.OverlapCircle(grounder.transform.position,radiuss,ground);
		if(Input.GetKey(KeyCode.UpArrow) && isGrounded){
			rigidbodyComponent.AddForce (jumpVector, ForceMode2D.Force);
		}



        // 6 - Shooting
        //		bool shoot = CrossPlatformInputManager.GetButton("FIRE");  // Replaced by touchscreen control (Shiyu He)
        bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");
		// Careful: For Mac users, ctrl + arrow is a bad idea

		if (shoot)
		{
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
		// 5 - Get the component and store the reference
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		// 6 - Move the game object
//		rigidbodyComponent.velocity = movement;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (grounder.transform.position, radiuss);
	}
				
}
