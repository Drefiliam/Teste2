using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/VectorValue")]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver {
    
	
	public Vector2 initialValue;
	public Vector2 defaultValue;
	//position of the Player relative to the way he is facing when entering another scene
	private PlayerDirection facing;
	public Vector2 InitialValue { 
		get { 
			return initialValue; 
		} 
		set { 
			initialValue = value; 
		} 
	}
    public PlayerDirection Facing { 
		get { 
			return facing; 
		}
		set { 
			facing = value; 
		} 
	}
	
	public void OnAfterDeserialize() {
		initialValue = defaultValue;
	}
	
	public void OnBeforeSerialize() {
		
	}
}
