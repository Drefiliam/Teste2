using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {
    //array of the heart images, total of 5 hearts
	public Image[] hearts;
	//the 3 types of heart that is gonna be available for the player to have in the Heart Container
	public Sprite fullHeart;
	public Sprite halfFullHeart;
	public Sprite emptyHeart;
	public FloatValue heartContainers;
	public FloatValue playerCurrentHealth;
	
	// Start is called before the first frame update
    void Start() {
        InitHearts();
    }
	//method of the pre set initial hearts the Player has
	public void InitHearts() {
		for(int i = 0; i < heartContainers.initialValue; i++) {
			hearts[i].gameObject.SetActive(true);
			//by updating this method solves the problem of resetting health to full when changing scenes
			UpdateHearts();
		}	
	}
	//this method goes through the Player's health to check it's value to decide what Heart Container to choose 
	public void UpdateHearts() {
		//divides by 2 because half of 1 heart counts as 1 health point, 3 hearts = 6 health points
		float tempHealth = playerCurrentHealth.runTimeValue / 2;
		for(int i = 0; i < heartContainers.runTimeValue; i++) {
			//its "tempHealth - 1" becasue the index start with value 0
			if(i <= tempHealth - 1) {
				//Full Heart
				hearts[i].sprite = fullHeart;
			}
			else if (i >= tempHealth) {
				//Empty Heart
				hearts[i].sprite = emptyHeart;
			}
			else {
				//Half Full Heart
				hearts[i].sprite = halfFullHeart;
			}
		}
	}

}
