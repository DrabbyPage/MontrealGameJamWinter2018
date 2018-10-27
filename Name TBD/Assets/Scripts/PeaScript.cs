using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaScript : MonoBehaviour
{
    float moveSpeed = 10f;

    float deathTime = 2f;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        TimerToDestroy();
        MoveBullet();
	}

    void MoveBullet()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * moveSpeed;// AddForce(transform.up * moveSpeed);
    }

    void TimerToDestroy()
    {
        if(deathTime > 0)
        {
            deathTime = deathTime - Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // should be the weapon
        GameObject owner = gameObject.transform.parent.gameObject;

        // should be the weapon's holder
        GameObject ownersOwner = owner.transform.parent.gameObject;

        if (col.gameObject.name != ownersOwner.name)
        {
            // push the object
            Destroy(gameObject);
        }
    }
}
