using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom {

    public Door[] doors;
    public GameObject triggerArea;
    public GameObject trapDoor;
    public void CheckEnemies() {
        //goes through all Enemies in the room
        for (int i = 0; i < enemies.Length; i++) {
            //if the Enemies are active then  it doesn't do anything
            if (enemies[i].gameObject.activeInHierarchy 
                 && i < enemies.Length - 1) {
                return; 
            }
        }
        //if all Enemies are dead/deactivated then the doors open
        OpenDoors();

    }

    public override void OnTriggerEnter2D(Collider2D triggerArea) {

        if (triggerArea.CompareTag("Player") && !triggerArea.isTrigger) {
            //activates all Enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            //activates all Pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            trapDoor.SetActive(true);
            CloseDoors();
            virtualCamera.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //deactivates all Enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            //deactivates all Pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            trapDoor.SetActive(false);
            virtualCamera.SetActive(false);
        }

    }

    public void CloseDoors() {
        for (int i = 0; i < doors.Length; i++) {
            doors[i].Close();
        }
    }

    public void OpenDoors() {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }


}
