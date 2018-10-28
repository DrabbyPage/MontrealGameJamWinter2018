using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZLColliderScript : MonoBehaviour {
    
    private float boopForce;

    public void Initialize(float force)
    {
        boopForce = force;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);
        }
    }
}
