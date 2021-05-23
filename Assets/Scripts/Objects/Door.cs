using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType {
    key,
    enemy,
    button
}

public class Door : Interactable {

  //[Header ("Door Variables")]
    public DoorType thisDoorType;
    //by default the door is always close
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void Start() {
        doorSprite = GetComponentInParent<SpriteRenderer>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if (playerInRange && thisDoorType == DoorType.key ) {
                //does the Player have the key?
                if(playerInventory.numberOfKeys > 0) {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
  
    }

    public void Open ()  {
        //turns off the Door sprite 
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turns off the door's BoxCollider2D
        physicsCollider.enabled = false;
    }

    public void Close() {
       //if you want the door to close again after already defeating the enemies then go see txt vP47
    }
}

