using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {
	
	//camera movements variables
	public Vector2 cameraChange;
	public Vector3 playerChange;
	private CameraMovement cam;
	public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
	//text screen variables
	public bool needText;
	public string placeName;
	public GameObject text;
	public Text placeText;
	
    // Start is called before the first frame update
    void Start() {
		cam = Camera.main.GetComponent<CameraMovement>();

    }

    // Update is called once per frame
    void Update() {
        
    }
	
	private void OnTriggerEnter2D(Collider2D other) {
		//the second condition makes the room transition occur 
		//with the BoxCollider2D that is NOT a trigger
			if(other.CompareTag("Player") && !other.isTrigger) {
				
			 	cam.minPosition += cameraChange;
				cam.maxPosition += cameraChange;
				cam.maxPosition.x = cameraChangeMax.x;
				cam.maxPosition.y = cameraChangeMax.y;
				other.transform.position += playerChange; 
				if(needText) {
					StartCoroutine(placeNameCo());
				}		
			}
	}
	
	//method that runs in parallel to other processes and allows a specefied wait time
	private IEnumerator placeNameCo() {
		text.SetActive(true);
		placeText.text = placeName;
		// Adds fade to text when entering a new world and not map
		//placeText.GetComponent<Text>().CrossFadeAlpha(0, 2.5f, false);
		yield return new WaitForSeconds(2f);
		text.SetActive(false);
		
	} 	
	
}
