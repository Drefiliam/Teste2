using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows to create this script as an object using RIGHT CLICK
[CreateAssetMenu]

//ScriptableObject makes this script impossible to attach to anything in Unity Scene 
//ISerializationCallbackReceiver 
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
	
	public float initialValue;
	//during run time this wont show in the Inspector only the initialValue
	[HideInInspector]
	public float runTimeValue;
	
	public void OnAfterDeserialize() {
		runTimeValue = initialValue;
	}
	
	public void OnBeforeSerialize() {
		
	}	
}
