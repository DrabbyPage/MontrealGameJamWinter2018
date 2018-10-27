using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    float boopForce = 180f;

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
        if(canMove)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * spearMoveSpeed);
            canMove = false;
        }
    }

    void ReturnToParent()
    {
        gameObject.transform.position = parObj.transform.position;
    }

    public void SetMove(bool newVal)
    {
        canMove = newVal;
    }

    public void SetParent(GameObject newPar)
    {
        parObj = newPar;
    }

    public void SetBooPVal(float newBoop)
    {
        boopForce = newBoop;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(col.gameObject != parObj)
            {
                Vector2 diff = parObj.transform.position - gameObject.transform.position;
                col.gameObject.GetComponent<BoopScript>().Booped(-diff, boopForce);
            }
            else
            {
                ReturnToParent();
            }
        }
    }
}
