using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacerScirpt : MonoBehaviour
{
    float deathTime = 2f;

    float boopForce = 150f;

    GameObject parObj;

    bool melting = false;

    GameObject icePuddle;

    // Use this for initialization
    void Start()
    {
        icePuddle = Resources.Load("Prefabs/IcePuddle") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TimeTilMelt();
        MeltDown();
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
            deathTime = deathTime - Time.deltaTime;
        }
        else
        {
            melting = true;
        }
    }

    public void MeltDown()
    {
        if(melting)
        {

        }
    }

}
