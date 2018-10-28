using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour {

    [SerializeField]
    float foxCooldown;//, boopForce;

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
            if (timer < 0)
            {
                Debug.Log("down");
                timer = 0;
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
        }
    }

    public void SetOwner(GameObject ob)
    {
        owner = ob;
    }

}
