using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows to create this script as an object using RIGHT CLICK
[CreateAssetMenu]
//sits in memory and tell other objects what they need to do
public class Signal : ScriptableObject {
    
	public List<SignalListener> listeners = new List<SignalListener>();
	//loops through all the signal listeners that I currently have
	//for each signal it is gonna raise a method that I gonna create on it
	public void Raise() {
		//the list is goona be analised backwards, starting at the end, this way prevents errors
		for(int i = listeners.Count - 1; i >= 0; i--) {
			listeners[i].OnSignalRaised();
		}	
	}	
	//this method registers the signals
	public void RegisterListener(SignalListener listener) {
		listeners.Add(listener);
	}	
	//this method deletes registered signals
	public void DeRegisterListener(SignalListener listener) {
		listeners.Remove(listener);
	}
}
