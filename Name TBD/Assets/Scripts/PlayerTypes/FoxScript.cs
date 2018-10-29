using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour {

    [SerializeField]
    float foxCooldown, boopForce;

    GameObject owner, tracer, tracerObj;

    bool hasTracerSpawned = false;

    float timer;

	// Use this for initialization
	void Awake () {
        timer = 0;
        tracer = Resources.Load("Prefabs/Trace") as GameObject;
    }

    // Update is called once per frame
    void Update () {
       FoxCooldown();
	}

    void FoxCooldown()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            Debug.Log("cool");
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;
            if (timer < 0)
            {
                Debug.Log("down");
                timer = 0;
                GetComponentInParent<SpriteRenderer>().color = Color.white;
                //plays the ready clip from the sound manager
                if (SoundManagerScript.instance != null)
                {
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(true);
                    }
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(false);
                    }
                }
            }
        }
    }

    public void Trace()
    {
        if(!hasTracerSpawned && timer <= 0)
        {
            //instatiate
            Debug.Log("droppit");
            hasTracerSpawned = true;
            tracerObj = Instantiate(tracer) as GameObject;
            tracerObj.transform.position = owner.transform.position;
            timer = foxCooldown;
            
        }
        else if(hasTracerSpawned && timer <= 0)
        {
            Debug.Log("come back");
            owner.transform.position = tracerObj.transform.position;
            hasTracerSpawned = false;
            Debug.Log(tracerObj.name);
            timer = foxCooldown;
            Destroy(tracerObj);
            //tel sound
            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
            {
                SoundManagerScript.instance.PlayerTeliportSound(true);
            }

            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
            {
                SoundManagerScript.instance.PlayerTeliportSound(false);
            }

        }
    }

    public void SetOwner(GameObject obj)
    {
        owner = obj;
    }
	
	 void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);

            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
            {
                SoundManagerScript.instance.PlayHitSound(true);
            }

            if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
            {
                SoundManagerScript.instance.PlayHitSound(false);
            }


        }


    }

}
