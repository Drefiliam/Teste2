using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inherits directly from the Enemy Script and transfers automatically from there
public class Log : Enemy {
	//makes the enemy wake up and move all ways animation
	public Rigidbody2D myRigidbody;
    //Transform makes it located into a specific space and not the entire game
	public Transform target;
	public float chaseRadius;
	public float attackRadius;
	//when player is out of the chaseRadius he will come back to his homePosition and in sleep mode
	//public Transform homePosition;
	public Animator anim;
	
	// Start is called before the first frame update
    void Start() {
		currentState = EnemyState.idle;
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
		anim.SetBool("wakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate() {
		//if the Player dies then it sets is value to null
        if(GameObject.FindWithTag("Player")) {
			target = GameObject.FindWithTag("Player").transform;
		}
		else {
			target = null;
		}
		CheckDistance();
    }
	
	//virtual void overrides the class that it inherits from except if the class is override void
	public virtual void CheckDistance() {
		//if Player is not dead then do this
		if(target != null) {
			if (Vector3.Distance(target.position, transform.position) <= chaseRadius
			&& Vector3.Distance(target.position, transform.position) > attackRadius) {
				if (currentState == EnemyState.idle || currentState == EnemyState.walk
				 && currentState != EnemyState.stagger) {
					Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
					//this part makes the enemy change states depending on the position
					ChangeAnim(temp - transform.position);
					myRigidbody.MovePosition(temp);
					ChangeState(EnemyState.walk);
					anim.SetBool("wakeUp", true);
				}
			}
			else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
				anim.SetBool("wakeUp", false);
			}
		}
		//if Player is dead then goes back to sleep animation
		else {
			anim.SetBool("wakeUp", false);
		}
	}	
	//this method is called when the enemy direction changes
	public void ChangeAnim(Vector2 direction) {
		direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
	}	
	
	public void ChangeState(EnemyState newState) {
		if(currentState != newState) {
			currentState = newState;
		}
	}
	
	public IEnumerator DelayMethod()
    {
		yield return new WaitForSeconds(3f);
    }
}
