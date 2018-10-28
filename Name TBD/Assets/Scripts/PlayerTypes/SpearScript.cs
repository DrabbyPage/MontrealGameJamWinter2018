using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    float boopForce = 60;

    float moveTime;
    float maxTime = 0.4f;

    bool canMove = false;
    bool isHeld = true;

    [SerializeField]
    float spearMoveSpeed = 60f;

    GameObject parObj;

    void Start()
    {
        moveTime = maxTime;
    }

    // Update is called once per frame
    void Update ()
    {
		if(canMove)
        {
            MoveSpear();
        }

        if(isHeld)
        {
            ReturnToParent();
        }
	}

    void MoveSpear()
    {
        // will push the obj if time hasnt ended
        if (moveTime > 0)
        {
            isHeld = false;
            moveTime = moveTime - Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * spearMoveSpeed);
        }
        else
        {
            moveTime = maxTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            canMove = false;
        }
    }

    void SetPosToPlayer()
    {
        gameObject.transform.position = parObj.transform.position;
    }

    void ReturnToParent()
    {
        gameObject.transform.position = parObj.transform.position;
        gameObject.transform.rotation = parObj.transform.rotation;
        parObj.transform.GetChild(3).GetComponent<SpearHolderScript>().SetHasWeapon(true);
    }

    public void SetMove(bool newVal)
    {
        canMove = newVal;
    }

    public void SetParent(GameObject newPar)
    {
        parObj = newPar;
    }

    public void SetBoopVal(float newBoop)
    {
        boopForce = newBoop;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            
            if (col.gameObject != parObj)
            {
                Vector2 diff = parObj.transform.position - gameObject.transform.position;
                col.gameObject.GetComponent<BoopScript>().Booped(-diff, boopForce);

                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                // this makes the hit sound when it hits an enemy
                if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P2")
                {
                    SoundManagerScript.instance.PlayHitSound(true);
                }
                if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P1")
                {
                    SoundManagerScript.instance.PlayHitSound(false);
                }


            }
            else
            {
                if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P2")
                {
                    SoundManagerScript.instance.EndCoolDownSound(false);
                }
                if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P1")
                {
                    SoundManagerScript.instance.EndCoolDownSound(true);
                }


                if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.2f)
                {
                    isHeld = true;
                }
            }
        }
    }
}
