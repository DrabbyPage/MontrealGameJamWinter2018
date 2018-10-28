using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToScript : MonoBehaviour {

    [SerializeField]
    float boopForce = 200.0f;

    public void FreezeConstraints(Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
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
