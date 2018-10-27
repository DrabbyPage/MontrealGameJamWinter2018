using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesScript : MonoBehaviour
{
    float deathTime = 2f;

    float boopForce = 150f;

    GameObject parObj;

    bool exploding = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        BlowUp();
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

    void BlowUp()
    {
        if(deathTime > 0)
        {
            deathTime = deathTime - Time.deltaTime;
        }
        else
        {
            GameObject explosion;

            if (!exploding)
            {
                exploding = true;
                explosion = Instantiate(Resources.Load("Prefabs/Explosion")) as GameObject;
                explosion.transform.position = gameObject.transform.position;
                explosion.GetComponent<ExplodeScript>().SetParent(gameObject);
                explosion.GetComponent<ExplodeScript>().SetBoopForce(boopForce);

            }

        }
    }

    public void SetBoopForce(float newBoopForce)
    {
        boopForce = newBoopForce;
    }

    public void SetParentObj(GameObject newParent)
    {
        parObj = newParent;
    }
}
