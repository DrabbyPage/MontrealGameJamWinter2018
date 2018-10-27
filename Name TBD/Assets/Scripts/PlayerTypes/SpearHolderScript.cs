using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHolderScript : MonoBehaviour
{
    [SerializeField]
    float boopForce = 90.0f;

    bool hasWeapon = true;

    GameObject spearChild;

	// Use this for initialization
	void Start ()
    {
        spearChild = Instantiate(Resources.Load("Prefabs/Spear")) as GameObject;

        spearChild.GetComponent<SpearScript>().SetParent(gameObject.transform.parent.gameObject);
        spearChild.GetComponent<SpearScript>().SetBoopVal(boopForce);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ThrowWeapon()
    {
        if(hasWeapon)
        {            
            spearChild.GetComponent<SpearScript>().SetMove(true);

            hasWeapon = false;
        }
    }

    public void SetHasWeapon(bool newBool)
    {
        hasWeapon = newBool;
    }
}
