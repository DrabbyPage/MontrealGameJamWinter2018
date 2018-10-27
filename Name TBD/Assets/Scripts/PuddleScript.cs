using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleScript : MonoBehaviour
{
    // i dont know what im doin walter you take over...

    float deathTime = 2f;

    float boopForce = 150f;

    GameObject parObj;

    bool melting = false;

    float sizeX;
    float sizeY;
    float minSize = 0;

    float shrinkRate = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeTilMelt();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name != parObj.name && col.gameObject.tag != "Arena" && col.gameObject.tag != "ArenaEdge")
        {
            // push the object
            //...................//
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);
        }
    }

    void TimeTilMelt()
    {
        if (deathTime > 0)
        {
            sizeX = sizeX - shrinkRate;
            sizeY = sizeY - shrinkRate;
            deathTime = deathTime - Time.deltaTime;
        }
        else
        {
            melting = true;
        }
    }

}
