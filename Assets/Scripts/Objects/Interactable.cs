using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
	//variables to create signals to the player when interacting with the objects
	public bool playerInRange;	
	public Signal context;
	
	// Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
	
	//method that triggers if player enters in area range
	private void OnTriggerEnter2D(Collider2D other) {
			if(other.CompareTag("Player") && !other.isTrigger) {
				//raise the signal to the Player that's in range
				context.Raise();	
				playerInRange = true;
			}	
	}	
	//method that triggers if player exits off area range
	private void OnTriggerExit2D(Collider2D other) {
			if(other.CompareTag("Player") && !other.isTrigger) {
				//raise the signal to the Player that's off range
				context.Raise();
				playerInRange = false;
			}	
	}
}
