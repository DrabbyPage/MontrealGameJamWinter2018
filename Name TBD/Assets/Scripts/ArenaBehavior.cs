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

    private void Awake()
    {
        timer = 0;
        ringToDrop = arenaRings.Count - 1;
        SetArenaBounds();
    }

    void Update()
    {
        if (GameManager.getInstance().IsNewRound())
        {
            ResetArena();
        }

        if (ringToDrop > 0)
        {
            timer += Time.deltaTime;

            if (timer > timeToDrop && ringToDrop >= 0) //timer for rings falling
            {
                timer = 0;

                arenaRings[ringToDrop].GetComponent<ArenaFall>().Fall();
                StartCoroutine(Wait(ringToDrop));
                ringToDrop--;
                SetArenaBounds();
            }
        }

    }

    private IEnumerator Wait(int ringToDrop)
    {
        yield return new WaitForSeconds(ringFlashTime);
        GameManager.getInstance().SetArenaFell(true);
        arenaRings[ringToDrop].SetActive(false);
    }


    void ResetArena()
    {
        ringToDrop = arenaRings.Count - 1;

        for (int i = 0; i < arenaRings.Count; i++)
        {
            SetArenaBounds();
            timer = 0;
            arenaRings[i].GetComponent<ArenaFall>().Reset();
            arenaRings[i].SetActive(true);
        }

        GameManager.getInstance().ArenaNeedsUpdate(false);
    }

    public void SetArenaBounds()
    {
        Debug.Log(arenaRings[ringToDrop]);
       // Debug.Break();
        GameManager.getInstance().SetCurrentEdge(arenaRings[ringToDrop]);
    }
}
