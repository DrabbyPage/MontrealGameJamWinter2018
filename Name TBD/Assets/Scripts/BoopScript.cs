using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoopScript : MonoBehaviour {

    bool isBooped;
    float boopForce = 100f;
    float time;
    float maxTime = 0.2f;

    Vector2 boopDir;

	// Use this for initialization
	void Start ()
    {
        isBooped = false;
        time = maxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isBooped)
        {
            ForceAway();
        }
	}

    public void Booped(Vector2 dir, float boopEnergy)
    {
        Debug.Log("BLOOP");

        boopForce = boopEnergy;
        boopDir = dir;
        isBooped = true;
    }

    void ForceAway()
    {
        if(time > 0)
        {
            time = time - Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(boopDir * boopForce);
        }
        else
        {
            time = maxTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isBooped = false;
        }
    }
}
