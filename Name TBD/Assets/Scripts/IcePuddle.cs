using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePuddle : MonoBehaviour
{
    GameObject owner;

    static GameObject otherPlayer;
    float meltTime;
    bool melting;

    void Start()
    {
        //meltTime = 5f;
    }

    void Update()
    {
      
        MeltTimer();
        Melt();
    }

  

    void MeltTimer()
    {
        if (meltTime != 0)
        {
            meltTime -= Time.deltaTime;

            if (meltTime < 0)
                melting = true;
        }
    }

    void Melt()
    {
        if (melting)
        {
            Debug.Log("melting");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != owner)
        {
            otherPlayer = collision.gameObject;
            owner.GetComponentInChildren<IcePlacerScirpt>().MakeSlippery(otherPlayer);
        }
    }

    public void SetOwner(GameObject obj)
    {
        owner = obj;
    }

    public void SetMeltTime(float melt)
    {
        meltTime = melt;
    }
}
