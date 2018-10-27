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
	}
    
    public void Shoot()
    {
        GameObject newBullet;

        if (canFire)
        {
            newBullet = Instantiate(bullet) as GameObject;

            newBullet.GetComponent<PeaScript>().SetParentObject(gameObject.transform.parent.gameObject);

            newBullet.transform.eulerAngles = gameObject.transform.eulerAngles;

            newBullet.transform.position = gameObject.transform.position + gameObject.transform.up;

            newBullet.GetComponent<PeaScript>().SetBoopForce(peaBoopForce);

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
}
