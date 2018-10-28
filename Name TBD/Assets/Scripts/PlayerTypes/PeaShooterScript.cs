using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterScript : MonoBehaviour
{
    GameObject bullet;

    float time;

    [SerializeField]
    float fireTime = 0.1f;

    [SerializeField]
    bool peashooter = true;

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
        if (peashooter)
        {
            bullet = Resources.Load("Prefabs/Pea") as GameObject;
        }

        else
        {
            bullet = Resources.Load("Prefabs/Star") as GameObject;
        }

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
            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
            {
                SoundManagerScript.instance.PeaShootSoundPlay(true);
            }
            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
            {
                SoundManagerScript.instance.PeaShootSoundPlay(false);
            }

            if (peashooter)
            {
                newBullet.GetComponent<PeaScript>().SetParentObject(gameObject.transform.parent.gameObject);

                newBullet.transform.eulerAngles = gameObject.transform.eulerAngles;

                newBullet.transform.position = gameObject.transform.position + gameObject.transform.up;

                newBullet.GetComponent<PeaScript>().SetBoopForce(peaBoopForce);
            }

            else
            {
                newBullet.transform.eulerAngles = gameObject.transform.eulerAngles;

                newBullet.transform.position = gameObject.transform.position + gameObject.transform.up;
            }

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
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                shotCounter = 0;

                //set the skin color back to normal
                GetComponentInParent<SpriteRenderer>().color = Color.white;
                //plays the ready clip from the sound manager
                if (SoundManagerScript.instance != null)
                {
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(true);
                    }
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(false);
                    }
                }
            }
        }
    }
}
