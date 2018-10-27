using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlacerScirpt : MonoBehaviour
{
    [SerializeField]
    float meltTime, slipTime, originalPlayerDrag, slipDrag, 
          puddleCooldown, puddleSize;

    float deathTime, mSlipTime;
    bool slipping = true;
    float boopForce = 150f;
    float timer;

    GameObject parObj, otherPlayer, icePuddle;
    List<GameObject> puddles;

    void Awake()
    {
        mSlipTime = 0;
        deathTime = 0;
        icePuddle = Resources.Load("Prefabs/IcePuddle") as GameObject;
        parObj = gameObject.transform.parent.gameObject;
        puddles = new List<GameObject>();
    }

    private void Update()
    {
        SlipTimer();
        Slip();

        PuddleCooldown();
    }


    void PuddleCooldown()
    {
        timer += Time.deltaTime;

        if (timer > puddleCooldown)
        {
            puddles.Clear();
            timer = 0;
        }
    }

    void Slip()
    {
        if (!slipping)
        {
            Debug.Log("end slippity");
            otherPlayer.GetComponent<Rigidbody2D>().drag = originalPlayerDrag;
            slipping = true;
        }
    }

    void SlipTimer()
    {
        if (mSlipTime != 0 && otherPlayer != null)
        {
            mSlipTime -= Time.deltaTime;

            if (mSlipTime < 0)
            {
                mSlipTime = 0;
                slipping = false;
            }
        }
    }

    public void PeformAction()
    {
        if (puddles.Count < puddleSize)
        {
            GameObject icePud = Instantiate(icePuddle) as GameObject;
            icePud.GetComponent<IcePuddle>().SetMeltTime(meltTime);
            icePud.GetComponent<IcePuddle>().SetOwner(parObj);
            icePud.transform.position = parObj.transform.position;
            puddles.Insert(0, icePud);
        }
    }

    public void MakeSlippery(GameObject obj)
    {
        otherPlayer = obj;
        otherPlayer.GetComponent<Rigidbody2D>().drag = slipDrag;
        mSlipTime = slipTime;
    }

}
