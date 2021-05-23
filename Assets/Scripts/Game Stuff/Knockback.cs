using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This code was in comments
//it fixes the bug of thrusting too hard the enemy sometimes
public class Knockback : MonoBehaviour {
    
	/* [SerializeField] */ public float thrust;
	public float knockTime;
	//every variable that gives knockback also gives damage
	public float damage;
	

	private void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
			//instead of put put "breakable" in the future
			other.GetComponent<Pot>().OnSmash();
		}
		else {	
			if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player") || other.isTrigger) {
				//this if condition prevents other objects with the tag "enemy" to do damage and knockback to each other
				if(other.gameObject.CompareTag("enemy") && gameObject.CompareTag("enemy")) {
					return;
				}	
				Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
				if (hit != null) {
					Vector2 difference = hit.transform.position - transform.position;
					difference = difference.normalized * thrust;
					
					hit.AddForce(difference, ForceMode2D.Impulse);
					if(other.gameObject.CompareTag("enemy") && other.isTrigger) {
						hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
						other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
					}
					if(other.gameObject.CompareTag("Player")) {
						//Player gets knockback if it is not already in the knockback stage
						if(other.GetComponent<Player>().currentState != PlayerState.stagger) {
							hit.GetComponent<Player>().currentState = PlayerState.stagger;
							other.GetComponent<Player>().Knock(hit, knockTime, damage);
							//objects that knockback that dont do damage also exist, put code here
						}	
					}	
				}
			}	
		}
	}	
	
}
