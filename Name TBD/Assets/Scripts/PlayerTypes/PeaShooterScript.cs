using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterScript : MonoBehaviour
{
    GameObject bullet;

    float joySense = 0.4f;
    float time;

    [SerializeField]
    float fireTime = 0.1f;

    [SerializeField]
    float peaBoopForce = 100.0f;

    bool canFire;

    [SerializeField]
    int allowedShots;
    int shotCounter = 0;

    [SerializeField]
    int cooldownTime = 30;
    int cooldownCounter = 0;

    // Use this for initialization
    void Start ()
    {
        bullet = Resources.Load("Prefabs/Pea") as GameObject;

        time = fireTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckFire();
        CheckCooldown();
	}
    
    public void Shoot()
    {
        GameObject newBullet;

        if (canFire && shotCounter < allowedShots)
        {
            newBullet = Instantiate(bullet) as GameObject;

            newBullet.GetComponent<PeaScript>().SetParentObject(gameObject.transform.parent.gameObject);

            newBullet.transform.eulerAngles = gameObject.transform.eulerAngles;

            newBullet.transform.position = gameObject.transform.position + gameObject.transform.up;

            newBullet.GetComponent<PeaScript>().SetBoopForce(peaBoopForce);

            shotCounter++;
            canFire = false;
        }
        
    }

    void CheckFire()
    {
        if(!canFire)
        {
            if (time > 0)
            {
                time = time - Time.deltaTime;
            }
            else
            {
                time = fireTime;
                canFire = true;
            }
        }
    }

    // Determines whether or not the user can currently spin
    private void CheckCooldown()
    {
        if (shotCounter == allowedShots)
        {
            cooldownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                shotCounter = 0;
            }
        }
    }
}
