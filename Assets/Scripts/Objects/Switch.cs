using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public bool active;
    //this variable saves if the switch is on or not when changing scenes 
    public BoolValue isSwitchOn;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start() {

        mySprite = GetComponent<SpriteRenderer>();
        active = isSwitchOn.runTimeValue;
        //checks to see if the door is already open
        if (active) {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch() {

        active = true;
        isSwitchOn.runTimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        //checks to see if the one triggering the switch is the Player
        if (other.CompareTag("Player")) {
            ActivateSwitch();
        }
    }

}
