using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable {
    
	public Item contents;
	public Inventory playerInventory;
	//this bool prevents getting an item from an already open chest
	public bool isOpen;
	//this variable saves if the chest is open or not when changing scenes
	public BoolValue isChestOpen; 
	public Signal raiseItem;
	//this 2 values are from Canvas but the description you put is in the scriptable object
	public GameObject dialogueBox;
	public Text dialogueText;
	//animation of the chest opening
	private Animator anim;
	
	// Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
		isOpen = isChestOpen.runTimeValue;
		if (isOpen) {
			anim.SetBool("opened", true);
		}
    }

    // Update is called once per frame
    void Update() {
        
		if(Input.GetKeyDown(KeyCode.Space) && playerInRange) {
			if(!isOpen) {
				//open the chest
				OpenChest();
			}
			else {
				//chest is already open
				ChestAlreadyOpen();
			}
		}	
		
    }
	
	public void OpenChest() {
		//dialogue box activates
		dialogueBox.SetActive(true);
		//dialogue text = context text
		dialogueText.text = contents.itemDescription;
		//adds contents to the inventory
		playerInventory.AddItem(contents);
		playerInventory.currentItem = contents;
		//raise the signal to the Player to animate
		raiseItem.Raise();
		//raise the context clue
		context.Raise();
		//set the chest to open
		isOpen = true;
		anim.SetBool("opened", true);
		isChestOpen.runTimeValue = isOpen;
	}
	
	public void ChestAlreadyOpen() {
		//dialgue off
		dialogueBox.SetActive(false);
		//raise the signal to the Player to stop animating
		raiseItem.Raise();
		playerInRange = false;
	}
	
	//method that triggers if player enters in area range
	private void OnTriggerEnter2D(Collider2D other) {
			if(other.CompareTag("Player") && !other.isTrigger && !isOpen) {
				//raise the signal to the Player that's in range
				context.Raise();	
				playerInRange = true;
			}	
	}	
	//method that triggers if player exits off area range
	private void OnTriggerExit2D(Collider2D other) {
			if(other.CompareTag("Player") && !other.isTrigger && !isOpen) {
				//raise the signal to the Player that's off range
				context.Raise();
				playerInRange = false;
			}	
	}
}
