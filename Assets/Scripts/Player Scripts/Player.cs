using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState {
	walk, attack, interact, stagger, idle
}

public enum PlayerDirection {
    up, down, left, right
}


public class Player : MonoBehaviour {
	
	//Player variables to move when an animations is triggered
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidBody;
	private Vector3 change;
	public Animator animator;
	//Player variables for health
	public FloatValue currentHealth;
	public Signal playerHealthSignal;
	//Player variables for room transictions
	public VectorValue startingPosition;
	//Player variables for their inventory
	public Inventory playerInventory;
	public SpriteRenderer receivedItemSprite;
	//screen kick variables for when player is hit
	public Signal playerHit;
	
    // Start is called before the first frame update
    void Start() {
        currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
		//fixes bug of all Player hit boxes activate at once when not moving
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		//initial position of the Player when a scene transiction occurs
		transform.position = startingPosition.initialValue;
	
		if(SceneManager.GetActiveScene().name == "Overworld"  ) {
			animator.SetFloat("moveY", -1);
		}
		else {
		//position of the Player relative to the way he is facing when entering a new scene
		switch (startingPosition.Facing) {
				case PlayerDirection.down:
					animator.SetFloat("moveY", -1);
					break;
				case PlayerDirection.up:
					animator.SetFloat("moveY", 1);
					break;
				case PlayerDirection.right:
					animator.SetFloat("moveX", 1);
					break;
				case PlayerDirection.left:
					animator.SetFloat("moveX", -1);
					break;
			}
		}	
	}

    // FixedUpdate makes the player movements more smooth but the attack is lagged
    void FixedUpdate() {
		//is the player in an interaction?
		if (currentState == PlayerState.interact) {
			return;
		}
        change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		if(currentState == PlayerState.walk || currentState == PlayerState.idle) {
			UpdateAnimationAndMove();
		}
    }
	
	//Update has trouble with player movements but compensates with attack movements
	void Update()
    {
		if (Input.GetButtonDown("attack") && currentState != PlayerState.attack 
		 && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
        }
		
    }
	
	//co-routine is a method that passes in values for it to wait
	//runs in parallel to the main process
	//allows to build in specific delays
	private IEnumerator AttackCo() {
		animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		//waits 1 frame to make a small delay
		yield return null; 
		//doesnt go automatically to attack animation
		animator.SetBool("attacking", false); 
		//waits 1/3 of a second to reset to walk animation
		yield return new WaitForSeconds(0.33f); 
		//this condition protects the animation from attacking while interacting
		if(currentState != PlayerState.interact) {
			currentState = PlayerState.walk;
		}	
	}
	
	//this method sends a signal to the game
	public void RaiseItem() {
		if(playerInventory.currentItem != null) {
			if(currentState != PlayerState.interact) {
				animator.SetBool("reveiveItem", true);
				currentState = PlayerState.interact;
				receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
			}
			else {
				animator.SetBool("reveiveItem", false);
				currentState = PlayerState.idle;
				receivedItemSprite.sprite = null;
				playerInventory.currentItem = null;
			}
		}
	}
	
	void UpdateAnimationAndMove() {
		if(change != Vector3.zero) {
			MoveCharacter();
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
			//position of the Player relative to the way he is facing when entering another scene
			if(change.x > 0) {
				startingPosition.Facing = PlayerDirection.right;
			}
			else if(change.x < 0) {
				startingPosition.Facing = PlayerDirection.left;
			}
			if(change.y > 0) {
				startingPosition.Facing = PlayerDirection.up;
			}
			else if (change.y < 0) {
				startingPosition.Facing = PlayerDirection.down;
			}
		}	
		else {
			animator.SetBool("moving", false);
		}
	}	
	
	void MoveCharacter() {
//next line solves the diagonal bug of going faster than vertical and horizontal walk
		change.Normalize();
		myRigidBody.MovePosition (
			transform.position + change * speed * Time.deltaTime
		);
	}
	
	public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) {
		//if damage occurs then it is gonna raise a signal of the Player's health
		currentHealth.runTimeValue -= damage;
		playerHealthSignal.Raise();
		if(currentHealth.runTimeValue > 0) {			
			StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
		}
		else {
			this.gameObject.SetActive(false);
		}
	}	
	
	private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime) {
		//for when player is hit a signal is raised
		playerHit.Raise();
		
		if (myRigidbody != null ) { 
		//	Vector2 forceDirection = myRigidbody.transform.position - transform.position;
		//	Vector2 force = forceDirection.normalized * thrust;
		//	myRigidbody.velocity = force;
			yield return new WaitForSeconds(knockTime);
			myRigidbody.velocity = Vector2.zero; //new Vector2();
			currentState = PlayerState.idle;
			myRigidbody.velocity = Vector2.zero; //new Vector2();

		}
	}
}