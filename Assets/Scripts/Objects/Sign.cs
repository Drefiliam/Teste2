using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable {
	//variables for dialogue in the Sign object
	public GameObject dialogueBox;
	public Text dialogueText;
	public string dialogue;
	
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange) {
			if(dialogueBox.activeInHierarchy) {
				dialogueBox.SetActive(false);	
			}
			else {
				dialogueBox.SetActive(true);
				dialogueText.text = dialogue;	
			}	
		}	
    }
	//method that triggers if player exits off area range
	private void OnTriggerExit2D(Collider2D other) {
			if(other.CompareTag("Player") && !other.isTrigger) {
					playerInRange = false;
					dialogueBox.SetActive(false);
					//raise the signal to the Player that's off range
					context.Raise();
			}	
	}
}
