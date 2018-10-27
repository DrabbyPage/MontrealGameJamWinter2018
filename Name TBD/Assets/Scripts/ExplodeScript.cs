using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    float boopForce = 150.0f;

    GameObject parObj;

    public bool isDoneWithAnim;

    float sizeX;
    float sizeY;
    float maxSize = 2.5f;

    float explodeRate = 0.25f;

	// Use this for initialization
	void Start ()
    {
        sizeX = gameObject.transform.localScale.x;
        sizeY = gameObject.transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Explode();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name != parObj.name && col.gameObject.tag != "Arena" && col.gameObject.tag != "ArenaEdge")
        {
            // push the object
            //...................//

            Vector2 diff = gameObject.transform.position - col.gameObject.transform.position;

            col.gameObject.GetComponent<BoopScript>().Booped(-diff, boopForce);
            // using a positive diff can be used for the fishing hook;

            Debug.Log("booped");
        }
    }

    void Explode()
    {
        if(sizeX < maxSize && sizeY < maxSize)
        {
            sizeX = sizeX + explodeRate;
            sizeY = sizeY + explodeRate;
        }
        else
        {
            isDoneWithAnim = true;
            Destroy(parObj);
            Destroy(gameObject);
        }
        gameObject.transform.localScale = new Vector3(sizeX, sizeY, gameObject.transform.localScale.z);
    }

    public void SetParent(GameObject newPar)
    {
        parObj = newPar;
    }

    public void SetBoopForce(float newBoop)
    {
        boopForce = newBoop;
    }
}
