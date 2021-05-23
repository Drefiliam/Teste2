using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //movement variables
    public float moveSpeed;
    //duration variables
    public float duration;
    //this one runs in the background
    //private float durationSeconds;
    public Rigidbody2D myRigidbody;
    // how long in time the projectile is active in the GameObject
    [SerializeField] private float lifeTimeSeconds; 

    // Start is called before the first frame update
    void Start() {

        myRigidbody = GetComponent<Rigidbody2D>();
       // durationSeconds = duration;

    }

    // Update is called once per frame
    void FixedUpdate() {
        //durationSeconds -= Time.deltaTime;
        //if (durationSeconds <= 0 ) {
        //    Destroy(this.gameObject);
        //}
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo() {
        
        yield return new WaitForSeconds(lifeTimeSeconds);
        {
            Destroy(gameObject);
        }

    }

    public void Launch( Vector2 initialVeloc) {
        //normalizing makesthe projectile not change speed depending on the distance between the Player and the enemy
        myRigidbody.velocity = initialVeloc.normalized * moveSpeed;
    
    }
    //if it enters in contact with something then it is destroied
    public void OnTriggerEnter2D(Collider2D other) {

        //destroys on impact any object in the world or when it hits the Player
        if (!other.isTrigger || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }

}
