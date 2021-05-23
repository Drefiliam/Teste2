using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//goes on objects and listens and does what the Signal Script tells it to do
public class SignalListener : MonoBehaviour {
    
	public Signal signal;
	public UnityEvent signalEvent;
	
	public void OnSignalRaised() {
		signalEvent.Invoke();
	}
	//enabled signals get registered
	private void OnEnable() {
		signal.RegisterListener(this);
	}	
	//disabled signals get deleted
	private void OnDisable() {
		signal.DeRegisterListener(this);
	}	
	
}
