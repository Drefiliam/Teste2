using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {

	public Item currentItem;
	public List<Item> items = new List<Item>();
	public int numberOfKeys;
	public int coins;
	
	public void AddItem(Item itemToAdd) {
		//is the item a key?
		if(itemToAdd.isKey) {
			numberOfKeys++;
		}
		else {
			//if the item is not already in the inventory then it is added
			if(!items.Contains(itemToAdd)) {
				items.Add(itemToAdd);
			}	
		}
	}
   
}
