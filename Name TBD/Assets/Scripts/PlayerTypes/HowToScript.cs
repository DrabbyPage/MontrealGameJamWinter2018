using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToScript : MonoBehaviour {

    [SerializeField]
    float boopForce = 200.0f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);

            Vector2 difference = col.gameObject.transform.position - gameObject.transform.position;
            col.gameObject.GetComponent<BoopScript>().Booped(difference, boopForce);
        }
    }
}
