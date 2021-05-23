using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log {
  
	public Transform[] path;
	public int currentPoint;
	public Transform currentGoal;
	public float roundingDistance;
  
	//override void, overrides the class that is inheriting from, from the Log script CheckDistance
	public override void CheckDistance() {
		//if Player is not dead then do this
		if(target != null) {
			if(Vector3.Distance(target.position, transform.position) <= chaseRadius
			&& Vector3.Distance(target.position, transform.position) > attackRadius) {
				if(currentState == EnemyState.idle || currentState == EnemyState.walk
				&& currentState != EnemyState.stagger) {
					Vector3 temp = Vector3.MoveTowards(
					 transform.position, target.position, 
					  moveSpeed * Time.deltaTime);
					//this part makes the enemy change states depending on the position
					ChangeAnim(temp - transform.position);
					myRigidbody.MovePosition(temp);
					//ChangeState(EnemyState.walk);
					anim.SetBool("wakeUp", true);
				}
			}
		
			//if Player is out of the chaseRadius  then goes back to patroling
			else if(Vector3.Distance(target.position, transform.position) > chaseRadius){
				if(Vector3.Distance(transform.position, 
				 path[currentPoint].position) > roundingDistance) {
					Vector3 temp = Vector3.MoveTowards(
					 transform.position, path[currentPoint].position,
					  moveSpeed * Time.deltaTime);
					ChangeAnim(temp - transform.position);
					myRigidbody.MovePosition(temp);
				}
				else {
					ChangeGoal();
				}	
			}
		}
		//if Player is dead then it goes back patrolling
		else {
            Vector3 temp = Vector3.MoveTowards(
             transform.position, path[currentPoint].position,
              moveSpeed * Time.deltaTime);
			ChangeAnim(temp - transform.position);
			myRigidbody.MovePosition(temp);
			if(Vector3.Distance(transform.position,
             path[currentPoint].position) < roundingDistance) {
				ChangeGoal();
			}	
        }	
		
	}
	
	private void ChangeGoal() {
		if(currentPoint == path.Length -1) {
			currentPoint = 0;
			currentGoal = path[0];
		}
		else {
			currentPoint++;
			currentGoal = path[currentPoint];
		}
	}

}
