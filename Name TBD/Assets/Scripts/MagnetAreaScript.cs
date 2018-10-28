using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAreaScript : MonoBehaviour
{
    GameObject thisParent;
    float boopForce = 0.0f;

    // Sets the power of each magnetic area
    public void SetMagnetAreaData(GameObject parent, float boop)
    {
        thisParent = parent;
        boopForce = boop;
    }

    // Reverses the magnetic polarity
    public void ReversePolarity()
    {
        boopForce *= -1;
        Debug.Log(boopForce);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.name != thisParent.name && col.gameObject.tag != "Arena" && col.gameObject.tag != "ArenaEdge")
        {
            // Push the object
            col.gameObject.GetComponent<BoopScript>().Booped(thisParent.transform.up, boopForce);
        }
    }
}
