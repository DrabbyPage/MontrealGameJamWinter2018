using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehavior : MonoBehaviour {

    [SerializeField]
    float ringFlashTime, timeToDrop;

    [SerializeField]
    List<GameObject> arenaRings;

    int ringToDrop;
    float timer;
    bool canCheckRingDrop = false;

    float dropTimer;

    bool roundStarted = false;

    private void Awake()
    {
        timer = 0;
        dropTimer = 0;
        ringToDrop = arenaRings.Count - 1;
        SetArenaBounds();
    }

    void Update()
    {
        if (GameManager.getInstance().IsNewRound())
        {
            ResetArena();
        }

        roundStarted = GameManager.getInstance().newRoundStarted;

        if(roundStarted && !canCheckRingDrop)
        {
            if (ringToDrop > 0)
            {
                timer += Time.deltaTime;

                if (timer > timeToDrop && ringToDrop >= 0) //timer for rings falling
                {
                    timer = 0;

                    arenaRings[ringToDrop].GetComponent<ArenaFall>().Fall();
                    canCheckRingDrop = true;
                    //StartCoroutine(Wait(ringToDrop));
                    //ringToDrop = ringToDrop - 1; ;
                }
            }
        }

        
        if(canCheckRingDrop)
        {
            DropTimer();
        }
        
    }

    
    private IEnumerator Wait(int ringToDrop)
    {
        yield return new WaitForSeconds(ringFlashTime);
        //Debug.Log("arena fell");
        GameManager.getInstance().SetArenaFell(true);
        arenaRings[ringToDrop].SetActive(false);

        SetArenaBounds();

    }
    
    void DropTimer()
    {
        if (ringToDrop > 0)
        {
            dropTimer += Time.deltaTime;

            if (dropTimer > 1.0f && ringToDrop >= 0) //timer for rings falling
            {
                dropTimer = 0;
                canCheckRingDrop = false;

                GameManager.getInstance().SetArenaFell(true);
                arenaRings[ringToDrop].SetActive(false);
                ringToDrop = ringToDrop - 1;
                SetArenaBounds();
            }
        }
    }


    void ResetArena()
    {
        timer = 0;
        ringToDrop = arenaRings.Count - 1;
        dropTimer = 0;
        canCheckRingDrop = false;

        for (int i = 0; i < arenaRings.Count; i++)
        {
            arenaRings[i].GetComponent<ArenaFall>().Reset();
            arenaRings[i].SetActive(true);
            SetArenaBounds();
        }

        GameManager.getInstance().ArenaNeedsUpdate(false);
    }

    public void SetArenaBounds()
    {
        //Debug.Log(arenaRings[ringToDrop]);
       // Debug.Break();
        GameManager.getInstance().SetCurrentEdge(arenaRings[ringToDrop]);
    }

    public void SetRoundStart(bool newBool)
    {

    }
}
