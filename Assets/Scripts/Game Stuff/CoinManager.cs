using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour {

    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinDisplay() {

        coinDisplay.text = "" + playerInventory.coins;

    }
 
}
