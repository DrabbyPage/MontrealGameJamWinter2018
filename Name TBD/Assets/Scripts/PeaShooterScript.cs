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

    bool canFire;

    const string p1_LSH_Name = "P1_LJS_H";
    const string p1_LSV_Name = "P1_LJS_V";
    const string p1_RT_Name = "P1_RT";
    const string p1_RSH_Name = "P1_RJS_H";
    const string p1_RSV_Name = "P1_RJS_V";

    // Use this for initialization
    void Start ()
    {
        bullet = Resources.Load("Prefabs/Pea") as GameObject;

        time = fireTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
	}
    
    void Shoot()
    {
        GameObject newBullet;

        if(canFire)
        {
            if (Input.GetAxis(p1_RT_Name) > 0 && canFire)
            {
                newBullet = Instantiate(bullet) as GameObject;

                newBullet.transform.eulerAngles = gameObject.transform.eulerAngles;

                newBullet.transform.position = gameObject.transform.position + gameObject.transform.up;

                canFire = false;
            }
        }
        else
        {
            CheckFire();
        }
        
    }

    void CheckFire()
    {
        if(time > 0)
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
