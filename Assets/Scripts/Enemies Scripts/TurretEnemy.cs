using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log {

	public GameObject projectile;
	//timer for the projectiles thrown per second
	public float fireDelay;
	private float fireDelaySeconds;
	public bool canFire;
    private void Update()
    {
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }

    }
    public override void CheckDistance()
    {
		//if Player is not dead then do this
		if (target != null)
		{
			if (Vector3.Distance(target.position, transform.position) <= chaseRadius
			&& Vector3.Distance(target.position, transform.position) > attackRadius)
			{
				if (currentState == EnemyState.idle || currentState == EnemyState.walk
				 && currentState != EnemyState.stagger)
				{
					//only fires a projectile if the timer is over
					if (canFire) {
						//this temporary vector is to calculate the distance between the Player and the enemy
						Vector3 tmpVector = target.transform.position - transform.position;
						GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
						current.GetComponent<Projectile>().Launch(tmpVector);
						canFire = false;
						ChangeState(EnemyState.walk);
						anim.SetBool("wakeUp", true);
                        //if (Vector2.Distance(current.GetComponent<Projectile>().transform.position, this.transform.position) >= chaseRadius)
                        //{
                        //    Destroy(current.GetComponent<Projectile>());
                        //}
                    }
				}
				
			}
			else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
			{
				anim.SetBool("wakeUp", false);
			}
		}
		//if Player is dead then goes back to sleep animation
		else
		{
			anim.SetBool("wakeUp", false);
		}

	}

}
