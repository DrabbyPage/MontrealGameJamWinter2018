using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaScript : MonoBehaviour
{
    float moveSpeed = 10f;

    float deathTime = 2f;

    float boopForce = 100f;

    GameObject parObj;

	// Use this for initialization
	void Start ()
    {
        Physics2D.GetIgnoreLayerCollision(8, 9);
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name != parObj.name && col.gameObject.tag != "Arena" && col.gameObject.tag != "ArenaEdge")
        {
            // push the object
            //...................//
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);

            Destroy(gameObject);
        }
    }

    public void SetParentObject(GameObject newPar)
    {
        //Debug.Log(newPar.name);
        parObj = newPar;
    }

    public void SetBoopForce(float newBoop)
    {
        boopForce = newBoop;
    }

}
