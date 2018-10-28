using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrail : MonoBehaviour {

    GameObject owner;

    static GameObject otherPlayer;
    float deathTime;
    bool dying;

    void Start()
    {

    }

    void Update()
    {

        MeltTimer();
        Melt();
    }


    void MeltTimer()
    {
        if (deathTime != 0)
        {
            deathTime -= Time.deltaTime;

            if (deathTime < 0)
                dying = true;
        }
    }

    void Melt()
    {
        if (dying)
        {
            Debug.Log("dying");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != owner)
        {
            otherPlayer = collision.gameObject;
            owner.GetComponentInChildren<SlimePlaceScript>().MakeSlow(otherPlayer);
        }
    }

    public void SetOwner(GameObject obj)
    {
        owner = obj;
    }

    public void SetSlowTime(float melt)
    {
        deathTime = melt;
    }
}
