using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHolderScript : MonoBehaviour
{
    bool hasWeapon = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ThrowWeapon()
    {
        if(hasWeapon)
        {
            gameObject.transform.GetChild(0).GetComponent<SpearScript>().SetMove(true);
        }
    }
}
