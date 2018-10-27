using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer : MonoBehaviour
{
    GameObject mineObj;

    [SerializeField]
    float boopForce = 150f;

    public bool canPlace = true;

    float placeTimer;

    [SerializeField]
    float timeBetweenPlaces = 1.0f;

    // Use this for initialization
    void Start ()
    {
        placeTimer = timeBetweenPlaces;
        mineObj = Resources.Load("Prefabs/Mine") as GameObject;
    }

    // Update is called once per frame
    void Update ()
    {
		if(!canPlace)
        {
            ResetPlaceTimer();
        }
	}

    public void PlaceMine()
    {
        if(canPlace)
        {
            GameObject newMine;

            newMine = Instantiate(mineObj) as GameObject;

            newMine.transform.position = gameObject.transform.position;

            newMine.GetComponent<MinesScript>().SetParentObj(gameObject.transform.parent.gameObject);

            newMine.GetComponent<MinesScript>().SetBoopForce(boopForce);

            canPlace = false;
        }

    }

    void ResetPlaceTimer()
    {
        if(placeTimer > 0)
        {
            placeTimer = placeTimer - Time.deltaTime;
        }
        else
        {
            placeTimer = timeBetweenPlaces;
            canPlace = true;
        }

    }
}
