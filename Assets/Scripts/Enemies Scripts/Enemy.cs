using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
		idle, walk, attack, stagger	
}

public class Enemy : MonoBehaviour {

	//this inherits from the FloatValue script inside the ScriptableObects folder
	//every enemy changes their health base on the amount of health they have
	public FloatValue maxHealth;

	public EnemyState currentState;
	public float health;
	public string enemyName;
	public int baseAttack;
	public float moveSpeed;
	public GameObject deathEffect;
	private float deathEffectDelay = 1f;
	//Enemy original position to go back when changing scene/room
	public Vector2 originalPosition;

	//when all Enemies die they send a signal to the room that they are deactivated
	public Signal roomSignal;

	private void Awake() {
		health = maxHealth.initialValue;
	}

    private void OnEnable() {
		transform.position = originalPosition;
	}

	private void TakeDamage(float damage) {
		health -= damage;
		//if health is equal or lower to 0 health then the enemy is destroyed
		if (health <= 0) {
			DeathEffect();
			//before setting the Enemy Object innactive a signal is raised 
			roomSignal.Raise();
			this.gameObject.SetActive(false);
		}	
	}

	private void DeathEffect() {

		if (deathEffect != null){
			GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(effect, deathEffectDelay);
		}

	}
	
	public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) {
		StartCoroutine(KnockCoroutine(myRigidbody, knockTime, damage));
		TakeDamage(damage);
	}	
	
	private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime, float damage) {
		if (myRigidbody != null) { 
		//	Vector2 forceDirection = myRigidbody.transform.position - transform.position;
		//	Vector2 force = forceDirection.normalized * thrust;
		//	myRigidbody.velocity = force;
			yield return new WaitForSeconds(knockTime);
			myRigidbody.velocity = Vector2.zero; //new Vector2();
			currentState = EnemyState.idle;
			myRigidbody.velocity = Vector2.zero; //new Vector2();
		}
	}	
}
