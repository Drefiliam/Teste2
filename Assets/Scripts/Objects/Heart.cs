using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp {

    public FloatValue playerHealth;
    //1 would be half a heart, and 2 would be a full heart
    public float amountToIncrease;
    public FloatValue heartContainers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.runTimeValue += amountToIncrease;
            //checks if the health added is bigger than the number of max containers
            if (playerHealth.initialValue > heartContainers.runTimeValue * 2f) {
                //if so then the current health will be the number of max containers available
                playerHealth.initialValue = heartContainers.runTimeValue * 2f;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }

    }

}
