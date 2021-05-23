using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	
	//camera smoothness variables
	public Transform target;
	public float smoothing;
	//tilemap camera variables
	public Vector2 maxPosition;
	public Vector2 minPosition;
	//new camera position when changing scenes
	public VectorValue cameraNewMax;
	public VectorValue cameraNewMin;
	//screen kick variables
	public Animator anim;


	
    // Start is called before the first frame update
    void Start() {
		maxPosition = cameraNewMax.initialValue;
		minPosition = cameraNewMin.initialValue;
		anim = GetComponent<Animator>();
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // LateUpdate is called once per frame and it's the last to happen
    void LateUpdate() {
        //Lerp = Linear interpolation uses 3 arguments (current position, wanted position, postion amount to cover)
		if (transform.position != target.position) {
			//fixes bug camera in Z position
			Vector3 targetPosition = new Vector3 (target.position.x, target.position.y, transform.position.z);
			//limits the tilemap camera to the x and y position of the character
			targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
			//Lerp function changes the current position to the new position
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
		}
			
    }
	//can be used later or not, but it causes a bug with the stagger effect of the Player when hitted by an Enemy
	//public void StartKick() {
	//	anim.SetBool("kickActive", true);
	//	StartCoroutine(KickCo());
	//}
	//public IEnumerator KickCo() {

	//	yield return null;
	//	anim.SetBool("kickActive", false);
	
	//}
}
