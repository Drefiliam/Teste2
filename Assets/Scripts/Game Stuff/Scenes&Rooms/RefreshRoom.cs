using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshRoom : MonoBehaviour {

    public Enemy[] enemies;
    public Pot[] pots;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player") && !other.isTrigger) {
            //activates all Enemies
            for (int i = 0; i < enemies.Length; i++) {
                ChangeActivation(enemies[i], true);
            }
            //activates all Pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            virtualCamera.SetActive(true);
        }

    }

    public virtual void OnTriggerExit2D(Collider2D other) {

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
            virtualCamera.SetActive(false);
        }

    }
    public void ChangeActivation(Component comp, bool activ) {

        comp.gameObject.SetActive(activ);

    }
}
