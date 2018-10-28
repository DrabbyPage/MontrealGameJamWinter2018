using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlaceScript : MonoBehaviour {

    [SerializeField]
    float deathTime, slowTime, originalPlayerDrag, slowDrag,
          slimeCooldown, slimeSize, boopForce;

    float mSlowTime;
    bool slowing = true;
    float timer;

    GameObject parObj, otherPlayer, icePuddle;
    List<GameObject> slimes;

    void Awake()
    {
        mSlowTime = 0;
        icePuddle = Resources.Load("Prefabs/Slime") as GameObject;
        parObj = gameObject.transform.parent.gameObject;
        slimes = new List<GameObject>();
    }

    private void Update()
    {
        SlowTimer();
        Slow();

        SlimeCooldown();
    }


    void SlimeCooldown()
    {
        timer += Time.deltaTime;

        if (timer > slimeCooldown)
        {
            slimes.Clear();
            timer = 0;
        }
    }

    void Slow()
    {
        if (!slowing)
        {
            Debug.Log("end slimey");
            otherPlayer.GetComponent<Rigidbody2D>().drag = originalPlayerDrag;
            slowing = true;
        }
    }

    void SlowTimer()
    {
        if (mSlowTime != 0 && otherPlayer != null)
        {
            mSlowTime -= Time.deltaTime;

            if (mSlowTime < 0)
            {
                mSlowTime = 0;
                slowing = false;
            }
        }
    }

    public void PeformAction()
    {
        if (slimes.Count < slimeSize)
        {
            GameObject slimePud = Instantiate(icePuddle) as GameObject;
            slimePud.GetComponent<SlimeTrail>().SetSlowTime(deathTime);
            slimePud.GetComponent<SlimeTrail>().SetOwner(parObj);
            slimePud.transform.position = parObj.transform.position;
            slimes.Insert(0, slimePud);
        }
    }

    public void MakeSlow(GameObject obj)
    {
        otherPlayer = obj;
        otherPlayer.GetComponent<Rigidbody2D>().drag = slowDrag;
        mSlowTime = slowTime;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, boopForce);
        }

    }

}
