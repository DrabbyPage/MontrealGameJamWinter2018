using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    bool canMove = false;

    float spearMoveSpeed = 90;

    GameObject parObj;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(canMove)
        {
            MoveSpear();
        }
	}

    void MoveSpear()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * spearMoveSpeed);
    }

    public void SetMove(bool newVal)
    {
        canMove = newVal;
    }

    public void SetParent(GameObject newPar)
    {
        parObj = newPar;
    }
}
