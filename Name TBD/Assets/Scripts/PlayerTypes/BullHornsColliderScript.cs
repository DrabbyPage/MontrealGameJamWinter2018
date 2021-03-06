﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullHornsColliderScript : MonoBehaviour {

    private GameObject bullHornsObject;
    private float boopForce;
    
    public void Initialize(GameObject bullHorns, float force)
    {
        bullHornsObject = bullHorns;
        boopForce = force;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);

            bullHornsObject.GetComponent<BullHornsScript>().HaltCharge();

            //get the player that's being collided
            if(col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P2")
            {
                SoundManagerScript.instance.PlayHitSound(true);
            }
            if(col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P1")
            {
                SoundManagerScript.instance.PlayHitSound(false);
            }

            
        }
    }
}
