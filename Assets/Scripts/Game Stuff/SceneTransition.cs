using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    
	public string sceneToLoad;
	public Vector2 playerPosition;
	public VectorValue playerStorage;
	//variable for faded scene transiction panel
	public GameObject fadeInPanel;
	public GameObject fadeOutPanel;
	public float fadeWait;
	public Player player;
	//camera min and max values are the ScritableObjects of the scene we want to go
	public VectorValue cameraMin;
	public VectorValue cameraMax;
	//the new camera values are the X and Y boundaries of the room's scene you are changing to
	public Vector2 cameraNewMax;
	public Vector2 cameraNewMin;
	
	
	//Awake method is called before Start method
	private void Awake() {
		if(fadeInPanel != null) {
			GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
			Destroy(panel, 1);
		}
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player") && !other.isTrigger) {
			//puts the Player in walk animation when entering the Collider for scene transition
			player.currentState = PlayerState.interact;
			playerStorage.initialValue = playerPosition;
			//resolves the bug of HealthHolder blinking when changing scenes
			GameObject.Find("HealthHolder").SetActive(false);
			GameObject.Find("CoinIndicator").SetActive(false);
			StartCoroutine(FadeCo());
		}	 
	}
	
	public IEnumerator FadeCo() {
		if(fadeOutPanel != null) {
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);
		ResetCameraBounds();
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		//while it is still loading it makes the program to wait untill it is finish loading
		while(!asyncOperation.isDone) {
			yield return null;
		}	
	}
	public void ResetCameraBounds() {

		cameraMax.initialValue = cameraNewMax;
		cameraMin.initialValue = cameraNewMin;

	}

}
